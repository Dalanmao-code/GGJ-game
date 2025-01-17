using System;
using DG.Tweening;
using UnityEngine;

namespace UI
{
    public class UI_CutscenesPanel : BasePanel
    {
        public override EPanelAttr GetPanelAttr => EPanelAttr.None;
        public override EPanelLayer GetPanelLayer => EPanelLayer.FlowTip + 1;
        
        private CanvasGroup m_canvasGroup;
        internal override void Init()
        {
            m_canvasGroup = transform.GetComponent<CanvasGroup>();
        }

        public void FadeInOut(float inTime, Action inCallback, 
            float outTime, Action outCallback, float outDelay)
        {
            var sequence = DOTween.Sequence();
            sequence.Append(m_canvasGroup.DOFade(1, inTime).From(0f).SetEase(Ease.OutSine));
            sequence.AppendCallback(() =>
            {
                inCallback?.Invoke();
            });
            sequence.AppendInterval(outDelay);
            sequence.Append(m_canvasGroup.DOFade(0, outTime).SetEase(Ease.InCubic));
            sequence.AppendCallback(() =>
            {
                outCallback?.Invoke();
                Hide();
            });
        }
        public void FadeIn(float inTime, Action inCallback)
        {
            var sequence = DOTween.Sequence();
            sequence.Append(m_canvasGroup.DOFade(1, inTime).From(0f).SetEase(Ease.OutSine));
            sequence.AppendCallback(() =>
            {
                inCallback?.Invoke();
            });
        }
        public void FadeOut(float outTime, Action outCallback)
        {
            var sequence = DOTween.Sequence();
            sequence.Append(m_canvasGroup.DOFade(0, outTime).From(1f).SetEase(Ease.InCubic));
            sequence.AppendCallback(() =>
            {
                outCallback?.Invoke();
                Hide();
            });
        }
    }
}