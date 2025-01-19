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
        // 返回一条从相机到鼠标位置的射线（2D）
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        // 检测射线是否碰到物体
        if (hit.collider != null)
        {
            if (Input.GetMouseButtonDown(0) && GameMgr.Instance.ObjectCanInteract())
            {
                // 处理点击事件
                if(hit.collider.gameObject.GetComponent<Iinteract>()!=null)
                {
                    Iinteract _t = hit.collider.gameObject.GetComponent<Iinteract>();
                    _t.InteractEvent();
                }
               
            }
        }
    }
}
