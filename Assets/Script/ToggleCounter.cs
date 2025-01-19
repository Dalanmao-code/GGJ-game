using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UI.ExamplePanel;

public class DynamicToggleManager : Singleton<DynamicToggleManager>
{
    private Dictionary<int, Follow_> Clues=new Dictionary<int, Follow_>();
    private int checkedCount = 0;  // ��ѡ�� Toggle ����

    private void OnEnable()
    {
        // ������е� Toggle �б�
        //toggles.Clear();
    }

    /// <summary>
    /// �������е���������
    /// </summary>
    public void AddMyself(int id,Follow_ clue)
    {
        Clues.Add(id, clue);
    }

    public void RemoveMyself(int id)
    {
        Clues.Remove(id);
    }

    public void Update()
    {
        if(Clues.Count==6)
        {
            SixToOnePanel.Instance.ShowMeNew(Clues);
        }
    }
}
