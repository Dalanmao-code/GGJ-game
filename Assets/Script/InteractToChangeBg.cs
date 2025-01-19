using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class InteractToChangeBg : MonoBehaviour,Iinteract
{
    public BackGroundType Bt;

    public void InteractEvent()
    {
        if (Bt == BackGroundType.exhibt)
        {
            if (!GameMgr.Instance.CanEnterExhibit())
            {
                //TODO ��öԻ�����ʾһ��
                Debug.Log("����ʧ��");
                return;
            }
        }
        BackGroundMgr.Instance.ChangeBackGround(Bt);
        gameObject.SetActive(false);
    }
}
