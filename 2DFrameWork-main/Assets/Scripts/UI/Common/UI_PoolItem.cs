using UnityEngine;

namespace UI
{
    public class UI_PoolItem : IPoolItem
    {
        public RectTransform transform;
        public virtual void Init(Transform root)
        {
            transform = root as RectTransform;
        }
        public void SetParent(Transform parent)
        {
            transform.SetParent(parent, false);
            transform.anchoredPosition3D = Vector3.zero;
        }
    }
}