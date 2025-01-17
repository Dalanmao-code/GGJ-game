using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UI_MainTitlePanel : BasePanel
    {
        public override EPanelAttr GetPanelAttr => EPanelAttr.None;
        public override EPanelLayer GetPanelLayer => EPanelLayer.Background;

        internal override void Init()
        {
            transform.Find("StartBtn").GetComponent<Button>().onClick.AddListener(() =>
            {
                GameManager.Instance.LoadScene("scene1.1");
                UIController.Instance.Show<UI_MainPanel>();
                Hide();
            });
            transform.Find("ExitBtn").GetComponent<Button>().onClick.AddListener(() =>
            {
                Application.Quit();
            });
        }
    }
}
