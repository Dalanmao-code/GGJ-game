using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UI_MainPanel : BasePanel
    {
        public override EPanelAttr GetPanelAttr => EPanelAttr.None;
        public override EPanelLayer GetPanelLayer => EPanelLayer.Battle;

        internal override void Init()
        {
            
        }
        public override void OnPanelShow()
        {
            UIController.Instance.Show<UI_FlowTipPanel>();
        }
        public override void OnPanelHide()
        {
            
        }
        internal override void OnUpdate(float deltaTime)
        {
            var gameManager = GameManager.Instance;
            
        }
    }
}
