using Core;
using System.IO;
using System.Net.NetworkInformation;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace UI.ExamplePanel
{
    public class FindCluePanel : BasePanel<FindCluePanel>
    {
        public Image targetImage;
        public bool isOpen;
        public Animator animator;
        private bool This_Is_Open = false;
        private bool IsInput_E = false;
        private bool IsInput_F = false;
        public static int enlarge_id = -1;
        public static int Tracing_id = -1;
        public static int main_Tracing_id = -1;
        public static OpenCluePanel openCluePanel;
        public GameObject jing;

        [Header("气泡")]
        [SerializeField] public GameObject[] bubbles;
        public override void Init()
        {
            base.Init();
            GetControl<Button>("Exit").onClick.AddListener(() =>
            {
                AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
                if (IsInput_E) {

                    animator.SetBool("Water_Up", false);
                    emerg();
                    if (enlarge_id != -1)
                    {
                        bubbles[0].SetActive(true);
                    }
                    IsInput_E = false;
                    This_Is_Open = false;
                }
                else if(stateInfo.IsName("null"))
                {
                    HideMe();
                    This_Is_Open = false;
                }
                
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
            emerg();
            if (enlarge_id != -1)
            {
                bubbles[0].SetActive(true);
            }
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
            This_Is_Open = true;
        }

        private void Update()
        {
            if (This_Is_Open&&Input.GetKeyDown(KeyCode.E))
            {
                if (IsInput_E)
                {
                    animator.SetBool("Water_Up", false);
                    emerg();
                    if (enlarge_id != -1)
                    {
                        bubbles[0].SetActive(true);
                    }
                    IsInput_E = false;
                }
                else
                {
                    animator.SetBool("Water_Up", true);
                    emerg();
                    if (Tracing_id != -1)
                    {
                        bubbles[1].SetActive(true);
                    }
                    IsInput_E = true;
                }
            }

            if (This_Is_Open && Input.GetKeyDown(KeyCode.F))
            {
                if (IsInput_F)
                {
                    jing.SetActive(false);
                    emerg();
                    if (enlarge_id != -1)
                    {
                        bubbles[0].SetActive(true);
                    }
                    IsInput_F = false;
                }
                else
                {
                    jing.SetActive(true);
                    emerg();
                    if (main_Tracing_id != -1)
                    {
                        bubbles[2].SetActive(true);
                    }
                    IsInput_F = true;
                }
            }
        }
        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        public void enlarge_clues()
        {
            Backpack.AddItem(new Item(enlarge_id, DataCenter.GetItemDataByID(enlarge_id), 1));
            BackpackUIController.notifyBackpackUpdated();
            bubbles[0].SetActive(false);
            enlarge_id = -1;
            if (openCluePanel != null)
                openCluePanel.enlarge_id = -1;
        }
        public void Tracing_clues()
        {
            Backpack.AddItem(new Item(Tracing_id, DataCenter.GetItemDataByID(Tracing_id), 1));
            BackpackUIController.notifyBackpackUpdated();
            bubbles[1].SetActive(false);
            Tracing_id = -1;
            if (openCluePanel != null)
                openCluePanel.Tracing_id = -1;
        }
        public void main_Tracing_id_clues()
        {
            Backpack.AddItem(new Item(main_Tracing_id, DataCenter.GetItemDataByID(main_Tracing_id), 1));
            BackpackUIController.notifyBackpackUpdated();
            bubbles[2].SetActive(false);
            main_Tracing_id = -1;
            if (openCluePanel != null)
                openCluePanel.main_Tracing_id = -1;
        }
        public void emerg()
        {
            foreach (var item in bubbles)
            {
                item.SetActive(false);
            }
            
        }
    }
}