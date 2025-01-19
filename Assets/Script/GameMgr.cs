using System.Collections;
using System.Collections.Generic;
using UI.ExamplePanel;
using UnityEngine;

public class GameMgr : Singleton<GameMgr>
{
    private bool _canEnterExhibit;

    private void Start()
    {
        GameStartPanel.Instance.ShowMe();
        _canEnterExhibit = false;
    }

    /// <summary>
    /// ��Ϸ�������߼�
    /// </summary>
    public void GameOver()
    {
        GameOverPanel.Instance.ShowMe();
    }

    public bool ObjectCanInteract()
    {
        bool t= FindCluePanel.Instance.isActiveAndEnabled || DynamicToggleManager.Instance.isActiveAndEnabled ||
            SixToOnePanel.Instance.isActiveAndEnabled;
        return !t;
    }

    /// <summary>
    /// ������48�ϳɺ� ����
    /// </summary>
    /// <param name="b"></param>
    public void SetEnterExhibit(bool b)
    {
        _canEnterExhibit = b;
    }

    public bool CanEnterExhibit()
    {
        return _canEnterExhibit;
    }
}
