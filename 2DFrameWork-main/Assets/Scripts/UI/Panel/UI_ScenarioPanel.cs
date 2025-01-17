using System;
using System.Collections.Generic;
using DG.Tweening;
using Scenario;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public class UI_ScenarioPanel : BasePanel, IForegroundPanel
    {
        public override EPanelAttr GetPanelAttr => EPanelAttr.PauseGame;
        public override EPanelLayer GetPanelLayer => EPanelLayer.Foreground;
        
        private readonly object m_contentTweenTarget = new object();
        private readonly object m_tweenTarget = new object();
        
        public static event Action<ScenarioData> OnScenarioStart;
        public static event Action<ScenarioData, int> OnScenarioStep;
        public static event Action<ScenarioData> OnScenarioFinish;
        /// <summary>
        /// 回复回调
        /// string: 对话名
        /// int: 对话索引
        /// int: 回复索引
        /// </summary>
        public static event Action<ScenarioData, int, int> OnScenarioReply;
        
        //立绘
        [SerializeField]
        private Transform m_drawingRoot;
        private readonly Dictionary<EDrawingDirection, Image> m_drawingImgMap = new Dictionary<EDrawingDirection, Image>();
        //对话
        [SerializeField]
        private Transform m_dialogRoot;
        [SerializeField]
        private TextMeshProUGUI m_titleText;
        [SerializeField]
        private TextMeshProUGUI m_contentText;
        private readonly HashSet<EDrawingDirection> m_drawingDirSet = new HashSet<EDrawingDirection>();
        //回复
        private class UI_ReplyBtnItem : UI_PoolItem
        {
            private TextMeshProUGUI m_contentText;
            private Button m_btn;
            public override void Init(Transform root)
            {
                base.Init(root);
                m_contentText = root.Find("Text").GetComponent<TextMeshProUGUI>();
                m_btn = root.GetComponent<Button>();
            }
            public void SetData(string msg, UnityAction callback)
            {
                m_contentText.text = msg;
                m_btn.onClick.RemoveAllListeners();
                m_btn.onClick.AddListener(callback);
            }
        }
        private SimpleItemPool<UI_ReplyBtnItem> m_ReplyBtnPool;
        private List<UI_ReplyBtnItem> m_replyBtnItemList;
        [SerializeField]
        private GameObject m_replyBtnPrefab;
        [SerializeField]
        private Transform m_replyRoot;
        [SerializeField]
        private CanvasGroup m_replyCanvasGroup;

        private bool m_hasReply;
        private bool m_contentEnd;
        private bool m_scenarioFinish;

        private CanvasGroup m_canvasGroup;

        //当前剧本
        private int m_currentIndex;
        private ScenarioData m_currentScenarioData;
        internal override void Init()
        {
            m_canvasGroup = transform.GetComponent<CanvasGroup>();
            
            //获取所有立绘站位
            var enumType = typeof(EDrawingDirection);
            var stationNames = Enum.GetNames(enumType);
            for (var i = 0; i < stationNames.Length; i++)
            {
                var img = m_drawingRoot.Find(stationNames[i]).GetComponent<Image>();
                img.enabled = false;
                m_drawingImgMap.Add((EDrawingDirection) Enum.Parse(enumType, stationNames[i]), img);
            }
            //回复
            m_ReplyBtnPool = new SimpleItemPool<UI_ReplyBtnItem>(m_replyBtnPrefab, null, item =>
            {
                item.SetParent(m_replyRoot);
                m_replyBtnItemList.Add(item);
            }, item => item.SetParent(null));
            m_replyBtnItemList = new List<UI_ReplyBtnItem>();

            //点击切换
            transform.Find("Next").GetComponent<Button>().onClick.AddListener(() =>
            {
                if (m_scenarioFinish || m_currentScenarioData == null) return;
                var dialogueData = m_currentScenarioData[m_currentIndex];
                if (!m_contentEnd)
                {
                    if (dialogueData.canSkip)
                    {
                        m_contentEnd = true;
                        DOTween.Kill(m_contentTweenTarget, true);
                    }
                }
                else if (!m_hasReply)
                {
                    //跳转
                    if (!string.IsNullOrEmpty(dialogueData.jumpScenario))
                    {
                        BeginScenario(dialogueData.jumpScenario, dialogueData.jumpTo);
                    }
                    else
                    {
                        if (dialogueData.jumpTo == 0)
                        {
                            Next();
                        }
                        else
                        {
                            SetStep(m_currentScenarioData.GetIndexById(dialogueData.jumpTo));
                        }
                    }
                }
            });
        }
        protected override void OnPanelRelease()
        {
            OnScenarioStart = null;
            OnScenarioStep = null;
            OnScenarioFinish = null;
        }
        public override void OnPanelShow()
        {
            m_canvasGroup.alpha = 0;
            foreach (var pair in m_drawingImgMap)
            {
                pair.Value.enabled = false;
            }
        }

        /// <summary>
        /// 开始一个剧本
        /// </summary>
        public void BeginScenario(ScenarioTarget target)
        {
            BeginScenario(target.scenarioName, target.scenarioId);
        }
        /// <summary>
        /// 开始一个剧本
        /// </summary>
        public void BeginScenario(string scenarioName, int beginId)
        {
            if (string.IsNullOrEmpty(scenarioName))
            {
                Hide();
                return;
            }
            var scenarioData = ScenarioManager.Find(scenarioName);
            m_scenarioFinish = false;
            m_canvasGroup.DOFade(1, 1f);
            //开始对话
            m_currentIndex = beginId != 0 ? scenarioData.GetIndexById(beginId) : 0;
            m_currentScenarioData = scenarioData;
            RefreshDialogue();
            
            OnScenarioStart?.Invoke(scenarioData);
        }

        private const float SWITCH_TIME = 0.5f;
        private void RefreshDialogue()
        {
            m_contentEnd = false;
            
            //停止之前的
            DOTween.Kill(m_tweenTarget, false);
            DOTween.Kill(m_contentTweenTarget, false);
            
            var dialogueData = m_currentScenarioData[m_currentIndex];
            var length = dialogueData.replyDataArr.Length;
            var hasReply = length > 0;
            //回复
            m_replyRoot.gameObject.SetActive(hasReply);
            if (hasReply)
            {
                m_replyCanvasGroup.alpha = 0;
                m_ReplyBtnPool.Push(m_replyBtnItemList);
                for (int i = 0; i < length; i++)
                {
                    var replyData = dialogueData.replyDataArr[i];
                    var btnItem = m_ReplyBtnPool.Pop();
                    var m = i;
                    btnItem.SetData(replyData.message, () =>
                    {
                        OnScenarioReply?.Invoke(m_currentScenarioData, m_currentIndex, m);
                        //跳转
                        if (!string.IsNullOrEmpty(replyData.jumpScenario))
                        {
                            BeginScenario(replyData.jumpScenario, replyData.jumpTo);
                        }
                        else
                        {
                            if (replyData.jumpTo == 0)
                            {
                                Next();
                            }
                            else
                            {
                                SetStep(m_currentScenarioData.GetIndexById(replyData.jumpTo));
                            }
                        }
                    });
                }
            }
            //对话
            m_titleText.text = dialogueData.name;
            m_contentText.text = "";
            //抖动
            if (dialogueData.shake) Shake();
            //文字/回复动画
            var contentSequence = DOTween.Sequence();
            //文字动画
            contentSequence.Append(m_contentText.DOText(dialogueData.content, dialogueData.showTime));
            //回调
            contentSequence.AppendCallback(() =>
            {
                m_contentEnd = true;
            });
            //回复动画
            if (hasReply)
            {
                contentSequence.Append(m_replyCanvasGroup.DOFade(1, SWITCH_TIME).From(0));
            }
            contentSequence.SetId(m_contentTweenTarget);
            m_hasReply = hasReply;
            
            //立绘
            m_drawingDirSet.Clear();
            for (var i = 0; i < dialogueData.drawingDataArr.Length; i++)
            {
                var drawingData = dialogueData.drawingDataArr[i];
                var img = m_drawingImgMap[drawingData.direction];
                if (!img.enabled) //刚显示
                {
                    img.enabled = true;
                    img.DOFade(1f, 0.5f).From(0f).SetId(m_tweenTarget);
                    img.sprite = drawingData.icon;
                    //改变颜色
                    img.color = drawingData.black ? new Color(0.2f, 0.2f, 0.2f, 1f) : Color.white;
                }
                else //之前还在显示
                {
                    var newDrawing = drawingData.icon;
                    if (img.sprite == newDrawing)
                    {
                        //改变颜色
                        img.DOColor(drawingData.black ? new Color(0.2f, 0.2f, 0.2f, 1f) : Color.white, SWITCH_TIME).SetId(m_tweenTarget);
                    }
                    else
                    {
                        //切换角色
                        var switchRoleSequence = DOTween.Sequence();
                        switchRoleSequence.Append(img.DOFade(0f, SWITCH_TIME * 0.5f));
                        switchRoleSequence.AppendCallback(() =>
                        {
                            img.sprite = newDrawing;
                            img.color = drawingData.black ? new Color(0.2f, 0.2f, 0.2f, 0f) : new Color(1, 1, 1, 0f);
                        });
                        //改变颜色
                        switchRoleSequence.Append(img.DOColor(drawingData.black ? new Color(0.2f, 0.2f, 0.2f, 1f) : Color.white, SWITCH_TIME * 0.5f));
                        switchRoleSequence.SetId(m_tweenTarget);
                    }
                }
                m_drawingDirSet.Add(drawingData.direction);
            }
            //隐藏未显示的
            foreach (var pair in m_drawingImgMap)
            {
                if (!m_drawingDirSet.Contains(pair.Key))
                {
                    pair.Value.DOFade(0, SWITCH_TIME).SetId(m_tweenTarget).OnComplete(() =>
                    {
                        pair.Value.enabled = false;
                    });
                }
            }
        }
        private void Next()
        {
            if (m_currentScenarioData[m_currentIndex].end)
            {
                Finish();
            }
            else
            {
                SetStep(m_currentIndex + 1);
            }
        }
        private void SetStep(int index)
        {
            m_currentIndex = index;
            if (index >= m_currentScenarioData.Length)
            {
                Finish();
            }
            else
            {
                OnScenarioStep?.Invoke(m_currentScenarioData, m_currentScenarioData[index].id);
                RefreshDialogue();
            }
        }

        private void Shake()
        {
            m_drawingRoot.DOShakeRotation(0.8f, new Vector3(0, 0, 3), 30, 90f, false, ShakeRandomnessMode.Harmonic);
            m_dialogRoot.DOShakeRotation(0.8f, new Vector3(0, 0, 3), 30, 90f, false, ShakeRandomnessMode.Harmonic);
        }
        private void Finish()
        {
            m_scenarioFinish = true;
            m_canvasGroup.DOFade(0, 1f).OnComplete(() =>
            {
                Hide();
                OnScenarioFinish?.Invoke(m_currentScenarioData);
            });
        }
    }
}
