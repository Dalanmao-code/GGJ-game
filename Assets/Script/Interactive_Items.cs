using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveItems : MonoBehaviour
{
    [Header("¥¢¥Ê¿‡")]
    [SerializeField] public GameObject picture;
    private bool IsOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsOpen && Input.GetKeyUp(KeyCode.Escape))
        {
            picture.gameObject.SetActive(false);
            IsOpen = false;
        }
    }
    public void Open_Close_Obj()
    {
        picture.gameObject.SetActive(true);
        IsOpen = true;
    }
}
