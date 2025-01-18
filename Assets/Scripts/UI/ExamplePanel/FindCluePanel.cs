using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace UI.ExamplePanel
{
    public class FindCluePanel : BasePanel<FindCluePanel>
    {
        public Image targetImage;
        public bool isOpen;

        public override void Init()
        {
            base.Init();
            GetControl<Button>("Exit").onClick.AddListener(() =>
            {
                HideMe();
            });
            /*GetControl<Button>("level1").onClick.AddListener(() =>
            {
                GamePanel.Instance.ShowMe();
                Debug.Log("进入了第1关");
            });
            GetControl<Button>("level2").onClick.AddListener(() =>
            {
                GamePanel.Instance.ShowMe();
                Debug.Log("进入了第2关");
            });
            GetControl<Button>("level3").onClick.AddListener(() =>
            {
                GamePanel.Instance.ShowMe();
                Debug.Log("进入了第3关");
            });*/
        }

        public void ShowMeNew(string path)
        {
            //加载不同物体的img
            string imagePath = Path.Combine("Sprites", path);

            Texture2D texture = Resources.Load<Texture2D>(imagePath);
            if (texture != null)
            {
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                targetImage.sprite = sprite;
            }
            else
            {
                Debug.LogError("Image not found at path: " + imagePath);
            }

            ShowMe();
        }
    }
}