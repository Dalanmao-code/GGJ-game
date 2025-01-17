using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Little
{
    public class UI_ProgressItem
    {
        public readonly RectTransform rectTransform;
        
        private readonly GameObject m_obj;
        private readonly Image m_bar;
        private readonly TextMeshProUGUI m_text;
        public UI_ProgressItem(Transform root)
        {
            rectTransform = root as RectTransform;
            m_bar = root.Find("Bar").GetComponent<Image>();
            m_text = root.Find("Text")?.GetComponent<TextMeshProUGUI>();
            m_obj = root.gameObject;
        }
        public void SetActive(bool active)
        {
            m_obj.SetActive(active);
        }
        public void SetFill(float fill)
        {
            m_bar.fillAmount = fill;
        }
        public void SetFillAndTextWithFormat(float fill, string format)
        {
            m_bar.fillAmount = fill;
            m_text.text = fill.ToString(format);
        }
        public void SetText(string msg)
        {
            m_text.text = msg;
        }
    }
}