using UnityEngine;
using UnityEngine.UI;

namespace UI.ExamplePanel
{
    /// <summary>
    /// 游戏开始时的面板，可以在UIManager的Start方法里添加GameStartPanel.Instance.ShowMe()来打开
    /// </summary>
    public class GameStartPanel :BasePanel<GameStartPanel>
    {
        public override void Init()
        {
            base.Init();

            GetControl<Button>("selectLevel").onClick.AddListener(() =>
            {
                HideMe();
                GamePanel.Instance.ShowMe();
            });
            GetControl<Button>("setting").onClick.AddListener(() =>
            {
                SixToOnePanel.Instance.ShowMe();
            });
        }

        public override void OnPressedEsc()
        {
            Debug.Log("确认要退出游戏吗?");
        }
    }
}