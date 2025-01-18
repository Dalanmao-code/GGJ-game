using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UI.ExamplePanel;

public class DynamicToggleManager : MonoBehaviour
{
    private List<Toggle> toggles = new List<Toggle>();  // 存储所有的 Toggle
    private List<Transform> checkedToggles = new List<Transform>();  // 存储勾选的 Toggle
    private int checkedCount = 0;  // 勾选的 Toggle 个数

    void OnEnable()
    {
        // 清空已有的 Toggle 列表
        toggles.Clear();
        checkedToggles.Clear();

        // 检测所有子物体并添加 Toggle
        foreach (Transform child in transform)
        {
            Toggle toggle = child.Find("Canvas/Toggle").GetComponent<Toggle>();
            if (toggle != null)
            {
                toggles.Add(toggle);
                toggle.onValueChanged.AddListener((isChecked) => OnToggleChanged(isChecked,child));
            }
        }
    }

    // 每当 Toggle 状态变化时调用
    void OnToggleChanged(bool isChecked,Transform clue)
    {
        // 更新已勾选的数量
        if (isChecked)
        {
            if(!checkedToggles.Contains(clue))
            {
                checkedToggles.Add(clue);
            }
        }
        else
        {
            checkedToggles.Remove(clue);
        }

        // 检查是否达到 6 个勾选
        if (checkedCount >= 6)
        {
            TriggerEvent();
        }
    }

    // 达到勾选数量后触发的事件
    void TriggerEvent()
    {
        List<int> numbers=new List<int>();
        for(int i=0;i<6;i++)
        {
            var t = checkedToggles[i].GetComponent<Follow_>().local_id;
            numbers.Add(t);
        }

        SixToOnePanel.Instance.ShowMeNew(numbers);
    }

    // 可选：在禁用时移除监听器
    void OnDisable()
    {
        foreach (var toggle in toggles)
        {
            //toggle.onValueChanged.RemoveListener((isChecked) => OnToggleChanged(isChecked,null));
        }
    }
}
