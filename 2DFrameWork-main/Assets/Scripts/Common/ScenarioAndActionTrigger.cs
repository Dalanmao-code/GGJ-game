using UI;
using UnityEngine;
using UnityEngine.Events;

public class ScenarioAndActionTrigger : ScenarioTrigger
{
    public UnityEvent callback;

    public override void Trigger(Player player)
    {
        base.Trigger(player);
        callback?.Invoke();
    }
}