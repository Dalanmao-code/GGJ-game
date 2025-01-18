using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum BackGroundType
{
    living,
    sick,
    exhibt,
    work,
}


public class BackGroundMgr:Singleton<BackGroundMgr>
{
    private BackGround _backGround;
    
    private void Start()
    {
        _backGround = GetComponent<BackGround>();
    }

    /// <summary>
    /// �л�����ʱ����
    /// </summary>
    public void ChangeBackGround(BackGroundType bt)
    {
        _backGround.ChangeBackGround(bt);
    }
}
