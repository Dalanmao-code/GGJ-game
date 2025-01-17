using System;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.Events;

public class ScenarioAndActionWhenContact : ScenarioWhenContact
{
    public UnityEvent callback;

    public override void OnCollisionEnter2D(Collision2D other)
    {
        base.OnCollisionEnter2D(other);
        if (counterList == null || counterList.Count == 0) return;
        if (other.gameObject.layer == Layers.Player)
        {
            callback?.Invoke();
        }
    }
}