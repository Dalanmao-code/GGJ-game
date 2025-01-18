using System.Collections;
using System.Collections.Generic;
using UI.ExamplePanel;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameStartPanel.Instance.ShowMe();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
