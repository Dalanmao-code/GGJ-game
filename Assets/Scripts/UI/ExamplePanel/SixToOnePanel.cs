using System.Collections;
using System.Collections.Generic;
using Core;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

namespace UI.ExamplePanel
{
    /// <summary>
    /// 示例面板设置
    /// </summary>
    public class SixToOnePanel : BasePanel<SixToOnePanel>
    {
        public List<GameObject> ClueBtns;
        private List<int> rightList;
        private List<int> ids=new List<int>();
        public override void Init()
        {
            rightList = new List<int> { 2, 34, 39, 42, 53, 54 };
            base.Init();
            GetControl<Button>("return").onClick.AddListener(HideMe);         
            GetControl<Button>("SixToOneBtn").onClick.AddListener(() =>
            {
                SixToOneMethod();
            });
        }

        private void SixToOneMethod()
        {
            bool areEqual = new HashSet<int>(ids).SetEquals(rightList);
            if(areEqual)
            {
                Debug.Log("合成成功");
            }
            else
            {
                Debug.Log("合成失败");
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