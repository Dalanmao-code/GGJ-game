using System;
using System.Collections.Generic;
using System.Reflection;
using UI;
using UnityEngine;

public interface IForegroundPanel { }
public class UIController : Singleton<UIController>
{
    /// <summary>
    /// 获取UI相机
    /// </summary>
    public Camera GetUICamera()
    {
        return m_uiCamera;
    }
    
    private readonly Camera m_uiCamera;
    private readonly List<BasePanel> m_list = new List<BasePanel>();
    private readonly Dictionary<Type, BasePanel> m_panelMap = new Dictionary<Type, BasePanel>();
    
    private static readonly Collider2D[] s_cols = new Collider2D[10];
    
    private enum EFlowTriggerType
    {
        Scenario = 1,
    }

    private readonly Transform m_transform;
    public UIController()
    {
        m_transform = GameObject.Find("UI Root").transform;
        m_uiCamera = m_transform.Find("UI Camera").GetComponent<Camera>();
        m_showForePanelList = new List<BasePanel>();
    }
    public void ReleaseAll()
    {
        for (var i = m_list.Count - 1; i >= 0; i--)
        {
            m_list[i].Release();
        }
        for (var i = m_list.Count - 1; i >= 0; i--)
        {
            GameObject.Destroy(m_list[i].gameObject);
        }
        m_list.Clear();
        m_panelMap.Clear();
    }
    public void OnUpdate(float deltaTime)
    {
        for (var i = m_list.Count - 1; i >= 0; i--)
        {
            var panel = m_list[i];
            if (panel.IsVisible())
            {
                panel.OnUpdate(deltaTime);
            }
        }
        
        var controlPlayer = GameManager.Player;
        if (controlPlayer == null) return;
        var flowTipPanel = Get<UI_FlowTipPanel>();
        if (AnyFore())
        {
            flowTipPanel.HideFlowTip();
            return;
        }
        var filter2D = new ContactFilter2D
        {
            useLayerMask = true,
            layerMask = Layers.Interactable_Mask,
            useTriggers = true,
        };
        var count = controlPlayer.m_controller.Collider.OverlapCollider(filter2D, s_cols);
        if (count > 0)
        {
            var show = false;
            var triggerDict = new Dictionary<EFlowTriggerType, MonoBehaviour>();
            for (int i = 0; i < count; i++)
            {
                var col = s_cols[i];
                if (col.TryGetComponent(out ScenarioTrigger scenarioTrigger))
                {
                    triggerDict[EFlowTriggerType.Scenario] = scenarioTrigger;
                    show = true;
                }
            }
            if (show)
            {
                if (triggerDict.TryGetValue(EFlowTriggerType.Scenario, out var trigger) &&
                    trigger is ScenarioTrigger scenarioTrigger)
                {
                    if (!scenarioTrigger.force)
                    {
                        //显示
                        flowTipPanel.ShowFlowTip("对话 (F)");
                        //触发
                        if (Input.GetKeyDown(KeyCode.F))
                        {
                            scenarioTrigger.Trigger(controlPlayer);
                        }
                    }
                    else
                    {
                        scenarioTrigger.Trigger(controlPlayer);
                    }
                }
            }
            else
            {
                flowTipPanel.HideFlowTip();
            }
        }
        else
        {
            flowTipPanel.HideFlowTip();
        }
    }

    public T Get<T>() where T : BasePanel
    {
        if (m_panelMap.TryGetValue(typeof(T), out var panel))
        {
            return (T) panel;
        }
        return NewPanel<T>();
    }
    public T Show<T>() where T : BasePanel
    {
        var panel = Get<T>();
        panel.Show();
        return panel;
    }
    public void Hide<T>() where T : BasePanel
    {
        var panel = Get<T>();
        panel.Hide();
    }

    private List<BasePanel> m_showForePanelList;
    /// <summary>
    /// 通知面板展示
    /// </summary>
    public void OnShow(BasePanel panel)
    {
        if (panel is IForegroundPanel)
        {
            m_showForePanelList.Add(panel);
        }
        panel.OnPanelShow();
    }
    /// <summary>
    /// 通知面板隐藏
    /// </summary>
    public void OnHide(BasePanel panel)
    {
        if (m_showForePanelList.Count > 0)
        {
            m_showForePanelList.Remove(panel);
        }
        panel.OnPanelHide();
    }
    /// <summary>
    /// 隐藏最后的
    /// </summary>
    public void HideLast()
    {
        var count = m_showForePanelList.Count;
        if (count > 0)
        {
            m_showForePanelList[count - 1].Hide();
        }
    }
    /// <summary>
    /// 有拍脸
    /// </summary>
    public bool AnyFore()
    {
        return m_showForePanelList.Count > 0;
    }
    
    private T NewPanel<T>() where T : BasePanel
    {
        var type = typeof(T);
        var typeName = type.Name;
        var panelPrefab = Resources.Load<GameObject>($"UIPanel/{typeName.Substring(3, typeName.Length - 3)}");
        var panelRoot = GameObject.Instantiate(panelPrefab, m_transform).transform;
        
        var basePanel = panelRoot.GetComponent<T>();
        basePanel.canvas = panelRoot.GetComponent<Canvas>();
        basePanel.canvas.worldCamera = m_uiCamera;
        basePanel.canvas.sortingOrder = (int) basePanel.GetPanelLayer;
        basePanel.Init();
        basePanel.gameObject.SetActive(false);
        
        m_list.Add(basePanel);
        m_panelMap.Add(type, basePanel);
        return basePanel;
    }
}