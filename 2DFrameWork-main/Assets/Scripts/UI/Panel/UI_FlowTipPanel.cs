using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UI;
using Unity.Mathematics;
using UnityEngine;

namespace UI
{
    public class UI_FlowTipPanel : BasePanel
    {
        public override EPanelAttr GetPanelAttr => EPanelAttr.None;
        public override EPanelLayer GetPanelLayer => EPanelLayer.FlowTip;

        [SerializeField]
        private GameObject m_flowTipObj;
        [SerializeField]
        private TextMeshProUGUI m_flowText;

        [SerializeField]
        private CanvasGroup m_tipGroup;
        [SerializeField]
        private TextMeshProUGUI m_tipText;
        
        internal override void Init() { }
        public override void OnPanelShow()
        {
            m_tipGroup.alpha = 0f;
            m_flowTipObj.SetActive(false);
        }
        public void ShowTip(string msg, float time = 1.5f)
        {
            m_tipText.text = msg;
            m_tipGroup.alpha = 1f;
            m_tipGroup.DOFade(0f, time * 0.2f).SetDelay(time * 0.8f);
        }

        public void ShowFlowTip(string msg)
        {
            m_flowTipObj.SetActive(true);
            m_flowText.text = msg;
        }
        public void HideFlowTip()
        {
            m_flowTipObj.SetActive(false);
        }
    }
}
