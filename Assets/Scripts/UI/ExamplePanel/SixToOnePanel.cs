using System.Collections.Generic;
using Core;
using TMPro;
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
        public override void Init()
        {
            base.Init();
            GetControl<Button>("return").onClick.AddListener(HideMe);         
            GetControl<Button>("SixToOneBtn").onClick.AddListener(() =>
            {

            });
        }

        /// <summary>
        /// 传过来选中线索的id
        /// </summary>
        /// <param name="numbers"></param>
        public void ShowMeNew(List<int> numbers)
        {
            AddListeners(numbers);
            ShowMe();
        }

        public void AddListeners(List<int> numbers)
        {
            for(int i=0;i<ClueBtns.Count;i++)
            {
                var text=ClueBtns[i].transform.Find("Text(TMP)").gameObject.GetComponent<TextMeshProUGUI>();
                text.text = DataCenter.GetItemDataByID(numbers[i]).text;
            }
        }
    }
}