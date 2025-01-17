using UI;
using UnityEngine;

public class ScenarioTrigger : MonoBehaviour
{
    public string scenarioName;
    public int scenarioId;
    [Space]
    public bool force;
    public bool inactiveAfterTrigger;

    public virtual void Trigger(Player player)
    {
        UIController.Instance.Show<UI_ScenarioPanel>().BeginScenario(scenarioName, scenarioId);
        if (inactiveAfterTrigger)
        {
            gameObject.SetActive(false);
        }
    }
}