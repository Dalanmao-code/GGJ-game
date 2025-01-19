using System.Collections;
using System.Collections.Generic;
using UI.ExamplePanel;
using UnityEngine;

public class GameMgr : Singleton<GameMgr>
{
    private bool _canEnterExhibit;
    public GameObject Display;
    private void Start()
    {
        GameStartPanel.Instance.ShowMe();
        _canEnterExhibit = false;
    }

    /// <summary>
    /// 游戏结束的逻辑
    /// </summary>
    public void GameOver()
    {
        GameOverPanel.Instance.ShowMe();
    }

    public bool ObjectCanInteract()
    {
        bool t= FindCluePanel.Instance.isActiveAndEnabled || Display.activeSelf ||
            SixToOnePanel.Instance.isActiveAndEnabled;
        return !t;
    }

    /// <summary>
    /// 当线索48合成后 调用
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
