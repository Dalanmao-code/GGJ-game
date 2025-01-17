using UnityEngine;

public interface INotLogicComponent { }
public abstract class UnitComponent : MonoBehaviour
{
    [HideInInspector]
    public Unit unit;
    public abstract int scriptOrder { get; }

    public virtual void OnInit()
    {
        
    }
    public virtual void OnRelease()
    {
        
    }
    public virtual void OnUpdate(float deltaTime)
    {
        
    }
    public virtual void OnFixedUpdate(float deltaTime)
    {
        
    }
}