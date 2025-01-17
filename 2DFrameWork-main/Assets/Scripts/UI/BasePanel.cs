using System;
using UnityEngine;

namespace UI
{
    public enum EPanelLayer : short
    {
        //战斗界面
        Battle = -4500,
        Battle_Middle = -3000,
        Battle_Foreground = -1500,
        //主界面
        Background = 0,
        Middle = 1500,
        Foreground = 3000,
        //提示层
        FlowTip = 4500,
        
        _MAX_
    }
    [Flags]
    public enum EPanelAttr
    {
        None = 0,
        PauseGame = 1 << 0,
    }

    public abstract class BasePanel : MonoBehaviour
    {
        /// <summary>
        /// 面板属性
        /// </summary>
        public abstract EPanelAttr GetPanelAttr { get; }
        /// <summary>
        /// 面板层级
        /// </summary>
        public abstract EPanelLayer GetPanelLayer { get; }
        
        [HideInInspector]
        public Canvas canvas;

        internal abstract void Init();
        internal void Release()
        {
            if (m_show) Show();
            OnPanelRelease();
        }
        public virtual void OnPanelShow() { }
        public virtual void OnPanelHide() { }
        protected virtual void OnPanelRelease() { }

        protected bool m_show;

        public bool Show()
        {
            var sortingOrder = (int) GetPanelLayer;
            if (!CanShow())
            {
                canvas.sortingOrder = sortingOrder;
                return false;
            }
            canvas.sortingOrder = sortingOrder;
            gameObject.SetActive(true);
            m_show = true;
            
            UIController.Instance.OnShow(this);
            
            //暂停游戏
            if ((GetPanelAttr & EPanelAttr.PauseGame) != 0)
            {
                GameManager.SetPause(true);
            }
            
            return true;
        }
        public bool Hide()
        {
            if (!m_show) return false;
            gameObject.SetActive(false);
            m_show = false;
            
            UIController.Instance.OnHide(this);
            
            //恢复游戏
            if ((GetPanelAttr & EPanelAttr.PauseGame) != 0)
            {
                GameManager.SetPause(false);
            }
            return true;
        }
        internal virtual void OnUpdate(float deltaTime) { }

        internal virtual bool CanShow()
        {
            if (IsVisible())
            {
                return false;
            }
            return true;
        }
        internal virtual bool IsVisible() => m_show;
    }
}
