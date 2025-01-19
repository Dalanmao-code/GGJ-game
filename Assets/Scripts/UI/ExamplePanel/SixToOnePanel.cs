using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace UI.ExamplePanel
{
    /// <summary>
    /// 示例面板设置
    /// </summary>
    public class SixToOnePanel : BasePanel<SixToOnePanel>
    {
        public List<GameObject> ClueBtns;
        private List<int> rightList1;
        private List<int> rightList2;
        private List<int> ids=new List<int>();
        public override void Init()
        {
            rightList1 = new List<int> { 0, 5, 22, 24, 26, 45 };
            rightList2 = new List<int> { 2, 34, 39, 42, 53, 54 };
            base.Init();
            GetControl<Button>("return").onClick.AddListener(HideMe);         
            GetControl<Button>("SixToOneBtn").onClick.AddListener(() =>
            {
                SixToOneMethod();
            });
        }

        private void SixToOneMethod()
        {
            bool areEqual1 = new HashSet<int>(ids).SetEquals(rightList1);
            bool areEqual2 = new HashSet<int>(ids).SetEquals(rightList2);
            if(areEqual1)
            {
                Backpack.AddItem(new Item(45, DataCenter.GetItemDataByID(46), 1));
                BackpackUIController.notifyBackpackUpdated();
                Debug.Log("合成成功46");
            }
            else if (areEqual2)
            {
                Backpack.AddItem(new Item(45, DataCenter.GetItemDataByID(56), 1));
                BackpackUIController.notifyBackpackUpdated();
                Debug.Log("合成成功56");
            }
            else
            {
                var common1 = new HashSet<int>(ids).Intersect(rightList1).Count();
                var common2 = new HashSet<int>(ids).Intersect(rightList2).Count();
                //匹配个数
                var res = common1 > common2 ? common1 : common2;
                //TODO 对话框说明一下
                Debug.Log(res);
            }
        }

        /// <summary>
        /// 传过来选中线索的id
        /// </summary>
        /// <param name="numbers"></param>
        public void ShowMeNew(Dictionary<int, Follow_> clues)
        { 
            ShowMe();
            AddListeners(clues);
        }

        public void AddListeners(Dictionary<int, Follow_> clues)
        {
            ids.Clear();
            foreach(var clue in clues)
            {
                ids.Add(clue.Key);
            }

            for(int i=0;i<ClueBtns.Count;i++)
            {
                var text = ClueBtns[i].transform.Find("Text (TMP)").gameObject.GetComponent<TextMeshProUGUI>();
                var t = string.Format("线索{0}", ids[i]);
                text.text = t;
            }
        }
    }
}