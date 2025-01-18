using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UI.ExamplePanel;

public class DynamicToggleManager : MonoBehaviour
{
    private List<Toggle> toggles = new List<Toggle>();  // 存储所有的 Toggle
    private int checkedCount = 0;  // 勾选的 Toggle 个数

    void OnEnable()
    {
        // 清空已有的 Toggle 列表
        toggles.Clear();

        // 检测所有子物体并添加 Toggle
        foreach (Transform child in transform)
        {
            Toggle toggle = child.Find("Canvas/Toggle").GetComponent<Toggle>();
            if (toggle != null)
            {
                toggles.Add(toggle);
                toggle.onValueChanged.AddListener((isChecked) => OnToggleChanged(isChecked));
            }
        }
    }

    // 每当 Toggle 状态变化时调用
    void OnToggleChanged(bool isChecked)
    {
        // 更新已勾选的数量
        if (isChecked)
        {
            checkedCount++;
        }
        else
        {
            checkedCount--;
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
        Debug.Log("已勾选 6 个 Toggle，事件已触发！");
        SixToOnePanel.Instance.ShowMe();
    }

    // 可选：在禁用时移除监听器
    void OnDisable()
    {
        foreach (var toggle in toggles)
        {
            toggle.onValueChanged.RemoveListener((isChecked) => OnToggleChanged(isChecked));
        }
    }
}
