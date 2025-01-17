using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BackGround : MonoBehaviour
{
    [Header("储存类")]
    [SerializeField]public GameObject[] scenarios;
    [SerializeField] public Animator[] animators;
    private int local_scene = 1;
    // Start is called before the first frame update
    void Start()
    {
        //Backpack.AddItem(new Item(0, DataCenter.GetItemDataByID(0), 1));
        //Backpack.AddItem(new Item(1, DataCenter.GetItemDataByID(1), 1));
        //Backpack.AddItem(new Item(3, DataCenter.GetItemDataByID(3), 1));
        //BackpackUIController.notifyBackpackUpdated();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Left_Button()
    {
        local_scene--;
        Update_scene();
    }
    public void Right_Button()
    {
        local_scene++;
        Update_scene();
    }

    public void Update_scene()
    {
        if (local_scene == 0)
            local_scene = 4;
        if (local_scene == 5)
            local_scene = 1;
        foreach (var scenario in scenarios)
        {
            scenario.SetActive(false);
        }
        scenarios[local_scene-1].SetActive(true);
    }
    // 鼠标进入按钮时的响应
    public void OnPointerEnter_Left()
    {
        animators[0].SetBool("button", true);
    }

    // 鼠标离开按钮时的响应
    public void OnPointerExit_Left()
    {
        animators[0].SetBool("button", false);
    }

    // 鼠标进入按钮时的响应
    public void OnPointerEnter_Right()
    {
        animators[1].SetBool("button", true);
    }

    // 鼠标离开按钮时的响应
    public void OnPointerExit_Right()
    {
        animators[1].SetBool("button", false);
    }
}
