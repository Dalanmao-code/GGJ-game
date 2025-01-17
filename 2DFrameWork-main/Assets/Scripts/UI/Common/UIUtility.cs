using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public static class UIUtility
    {
        /// <summary>
        /// 设置到最佳宽度
        /// </summary>
        public static float SetPreferredWidth(this TextMeshProUGUI text, float offset = 0f)
        {
            var sizeDelta = text.rectTransform.sizeDelta;
            sizeDelta.x = text.preferredWidth + offset;
            text.rectTransform.sizeDelta = sizeDelta;
            return sizeDelta.x;
        }
        /// <summary>
        /// 设置到最佳高度
        /// </summary>
        public static float SetPreferredHeight(this TextMeshProUGUI text, float offset = 0f)
        {
            var sizeDelta = text.rectTransform.sizeDelta;
            sizeDelta.y = text.preferredHeight + offset;
            text.rectTransform.sizeDelta = sizeDelta;
            return sizeDelta.y;
        }
        /// <summary>
        /// 设置到固定大小
        /// </summary>
        public static void SetFixedWidth(this TextMeshProUGUI text, float width)
        {
            var sizeDelta = text.rectTransform.sizeDelta;
            sizeDelta.x = width;
            text.rectTransform.sizeDelta = sizeDelta;
        }
        /// <summary>
        /// 设置到固定大小
        /// </summary>
        public static void SetFixedHeight(this TextMeshProUGUI text, float height)
        {
            var sizeDelta = text.rectTransform.sizeDelta;
            sizeDelta.y = height;
            text.rectTransform.sizeDelta = sizeDelta;
        }

        /// <summary>
        /// 刷新布局到最适合的大小
        /// </summary>
        public static float RefreshSizeToPreferred(this LayoutGroup layout, RectTransform.Axis axis, float extraSize = 0f, float minSize = 0f)
        {
            var layoutRoot = layout.transform as RectTransform;
            
            layout.enabled = true;
            LayoutRebuilder.ForceRebuildLayoutImmediate(layoutRoot);
            float size;
            if (axis == RectTransform.Axis.Horizontal)
            {
                layout.SetLayoutHorizontal();
                size = layout.preferredWidth;
            }
            else
            {
                layout.SetLayoutVertical();
                size = layout.preferredHeight;
            }
            size = Mathf.Max(size, minSize);
            layoutRoot.SetSizeWithCurrentAnchors(axis, size + extraSize);
            if (axis == RectTransform.Axis.Horizontal)
            {
                layout.SetLayoutHorizontal();
            }
            else
            {
                layout.SetLayoutVertical();
            }
            layout.enabled = false;
            return size;
        }
        /// <summary>
        /// 刷新布局
        /// </summary>
        public static void RefreshLayout(this LayoutGroup layout)
        {
            var layoutRoot = layout.transform as RectTransform;
            layout.enabled = true;
            LayoutRebuilder.ForceRebuildLayoutImmediate(layoutRoot);
            layout.enabled = false;
        }
    }
}