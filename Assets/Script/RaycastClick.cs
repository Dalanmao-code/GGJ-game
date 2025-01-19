using System.Collections;
using System.Collections.Generic;
using UI.ExamplePanel;
using UnityEngine;

public class RaycastClick : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        // ����һ������������λ�õ����ߣ�2D��
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        // ��������Ƿ���������
        if (hit.collider != null)
        {
            if (Input.GetMouseButtonDown(0) && GameMgr.Instance.ObjectCanInteract())
            {
                // �������¼�
                if(hit.collider.gameObject.GetComponent<Iinteract>()!=null)
                {
                    Iinteract _t = hit.collider.gameObject.GetComponent<Iinteract>();
                    _t.InteractEvent();
                }
               
            }
        }
    }
}
