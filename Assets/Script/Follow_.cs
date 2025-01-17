using Core;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Follow_: MonoBehaviour
{
    [Header("数值类")]
    [SerializeField]public float Box_size_long;//设置移动框大小
    [SerializeField]public float Box_size_wight;//设置移动框大小
    [SerializeField] public int pairing_id = -1;
    [SerializeField] public int target_id = -1;
    [SerializeField] public int local_id = -1;
    private bool isMouseDown = false;
    private Vector3 lastMousePosition = Vector3.zero;

    private Vector3 StartPosition;
    private bool Have_First = false;
    private bool In_WeaponBox = false;//是否处于武器框内
    private bool IsSleep = false;
    private float Timer = 0;
    private bool IsEnter = false;
    private GameObject linshi;

    // Start is called before the first frame update
    void Start()
    {
        StartPosition = this.transform.localPosition;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsSleep)//防止多次操作进行的休眠措施
        {
            isMouseDown = false;
            Timer += Time.deltaTime;
            if (Timer > 0.1)
            {
                Have_First = false;//防止进框瞬间休眠引起的朝鼠标位置移动
                IsSleep = false;
                Timer = 0;
            }
            return;
        }
        if (In_WeaponBox)
        {
            StartPosition = this.transform.localPosition;
            In_WeaponBox = false;
        }

        Vector3 position = this.gameObject.transform.position;
        float mouseY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
        float mouseX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;

        if (Input.GetMouseButtonDown(0) && mouseY >= position.y - Box_size_wight / 2 && mouseY <= position.y + Box_size_wight / 2 && mouseX >= position.x - Box_size_long / 2 && mouseX <= position.x + Box_size_long / 2)
        {
            isMouseDown = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (isMouseDown)
            {
                this.transform.localPosition = StartPosition;//判断必须移动了物体后才会复位

            }
            isMouseDown = false;
            lastMousePosition = Vector3.zero;
            Have_First = false;
            this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }
        if (isMouseDown)
        {
            if (mouseY >= position.y - Box_size_wight / 2 && mouseY <= position.y + Box_size_wight / 2 && mouseX >= position.x - Box_size_long / 2 && mouseX <= position.x + Box_size_long / 2 || Have_First)
            {
                Have_First = true;
                if (lastMousePosition != Vector3.zero)
                {
                    Vector3 offset = Camera.main.ScreenToWorldPoint(Input.mousePosition) - lastMousePosition;
                    this.transform.position += offset;
                   
                }
                lastMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }
        if(IsEnter)
        On_collider();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IsEnter = true;
        if (collision.tag == "bianqian")
        {
            collision.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 0.65f, 1);
        }
        linshi = collision.gameObject;
        
        

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        IsEnter = false;
        if (collision.tag == "bianqian")
        {
            collision.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1f, 1);
        }
        linshi = null;
    }

    public void On_collider()
    {
        if (linshi.GetComponent<Follow_>().local_id == pairing_id && Input.GetMouseButtonUp(0))
        {
            Homing();
            linshi.GetComponent<Follow_>().Homing();
            Backpack.AddItem(new Item(local_id, DataCenter.GetItemDataByID(local_id), -1));
            Backpack.AddItem(new Item(pairing_id, DataCenter.GetItemDataByID(pairing_id), -1));
            Backpack.AddItem(new Item(target_id, DataCenter.GetItemDataByID(target_id), 1));
            BackpackUIController.notifyBackpackUpdated();
        }
    }

    public void Homing()
    {
        //yield return new WaitForSeconds(0.02f);
        this.transform.localPosition = StartPosition;
        isMouseDown = false;
        lastMousePosition = Vector3.zero;
        Have_First = false;
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
    }


}
