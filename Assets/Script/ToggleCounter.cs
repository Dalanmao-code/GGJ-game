using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UI.ExamplePanel;

public class DynamicToggleManager : MonoBehaviour
{
    private List<Toggle> toggles = new List<Toggle>();  // �洢���е� Toggle
    private List<Transform> checkedToggles = new List<Transform>();  // �洢��ѡ�� Toggle
    private int checkedCount = 0;  // ��ѡ�� Toggle ����

    void OnEnable()
    {
        // ������е� Toggle �б�
        toggles.Clear();
        checkedToggles.Clear();

        // ������������岢��� Toggle
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

    // ÿ�� Toggle ״̬�仯ʱ����
    void OnToggleChanged(bool isChecked,Transform clue)
    {
        // �����ѹ�ѡ������
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

        // ����Ƿ�ﵽ 6 ����ѡ
        if (checkedCount >= 6)
        {
            TriggerEvent();
        }
    }

    // �ﵽ��ѡ�����󴥷����¼�
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

    // ��ѡ���ڽ���ʱ�Ƴ�������
    void OnDisable()
    {
        foreach (var toggle in toggles)
        {
            //toggle.onValueChanged.RemoveListener((isChecked) => OnToggleChanged(isChecked,null));
        }
    }
}
