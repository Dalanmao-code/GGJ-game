using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasRenderer))]
public class EmptyImage : MaskableGraphic
{
    protected override void Awake()
    {
        useLegacyMeshGeneration = false;
    }
    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();
    }
}
