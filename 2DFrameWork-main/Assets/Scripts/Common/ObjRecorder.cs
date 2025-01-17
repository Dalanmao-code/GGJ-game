using System;
using UnityEngine;

public class ObjRecorder : MonoBehaviour
{
    private void Start()
    {
        if (TryGetComponent(out Player player))
        {
            GameManager.Instance.InitPlayer(player);
        }
        else
        {
            GameManager.Instance.InitSceneObj(transform);
        }
    }
}