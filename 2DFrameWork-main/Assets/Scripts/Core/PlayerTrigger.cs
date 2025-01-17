using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UI;
using UnityEngine;

public class PlayerTrigger : UnitComponent, INotLogicComponent
{
    public override int scriptOrder => 0;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        
    }
}
