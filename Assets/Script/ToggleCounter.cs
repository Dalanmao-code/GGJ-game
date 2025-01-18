using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UI.ExamplePanel;

public class DynamicToggleManager : MonoBehaviour
{
    private List<Toggle> toggles = new List<Toggle>();  // �洢���е� Toggle
    private int checkedCount = 0;  // ��ѡ�� Toggle ����

    void OnEnable()
    {
        // ������е� Toggle �б�
        toggles.Clear();

        // ������������岢��� Toggle
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

    // ÿ�� Toggle ״̬�仯ʱ����
    void OnToggleChanged(bool isChecked)
    {
        // �����ѹ�ѡ������
        if (isChecked)
        {
            checkedCount++;
        }
        else
        {
            checkedCount--;
        }

        // ����Ƿ�ﵽ 6 ����ѡ
        if (checkedCount >= 6)
        {
            TriggerEvent();
        }
    }

    // �ﵽ��ѡ�����󴥷����¼�
    void TriggerEvent()
    {
        Debug.Log("�ѹ�ѡ 6 �� Toggle���¼��Ѵ�����");
        SixToOnePanel.Instance.ShowMe();
    }

    // ��ѡ���ڽ���ʱ�Ƴ�������
    void OnDisable()
    {
        foreach (var toggle in toggles)
        {
            toggle.onValueChanged.RemoveListener((isChecked) => OnToggleChanged(isChecked));
        }
    }
}
