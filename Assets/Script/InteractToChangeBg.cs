using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractToChangeBg : MonoBehaviour,Iinteract
{
    public BackGroundType Bt;

    public void InteractEvent()
    {
        BackGroundMgr.Instance.ChangeBackGround(Bt);
        gameObject.SetActive(false);
    }
}
