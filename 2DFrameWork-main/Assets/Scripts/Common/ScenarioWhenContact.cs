using System;
using System.Collections.Generic;
using UI;
using UnityEngine;

[Serializable]
public class ScenarioTarget
{
    public string scenarioName;
    public int scenarioId;
}
public class ScenarioWhenContact : MonoBehaviour
{
    public List<ScenarioTarget> counterList;
    [Space]
    public bool inactiveAfterTrigger;
    [Space]
    public bool pushAfterTrigger;

    public virtual void OnCollisionEnter2D(Collision2D other)
    {
        if (counterList == null || counterList.Count == 0) return;
        if (other.gameObject.layer == Layers.Player)
        {
            var scenarioTarget = counterList[0];
            UIController.Instance.Show<UI_ScenarioPanel>().BeginScenario(scenarioTarget);
            if (inactiveAfterTrigger)
            {
                gameObject.SetActive(false);
            }
            if (pushAfterTrigger)
            {
                var deltaX = transform.position.x - other.rigidbody.position.x;
                other.rigidbody.position -= new Vector2((other.collider as BoxCollider2D).size.x / 2 * Mathf.Sign(deltaX), 0f);
            }
            if (counterList.Count > 1) counterList.RemoveAt(0);
        }
    }
}