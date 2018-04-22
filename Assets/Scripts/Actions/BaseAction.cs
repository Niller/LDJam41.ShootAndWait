using DefaultNamespace;
using UnityEngine;

[RequireComponent(typeof(TurnBasedActor))]
public abstract class BaseAction : MonoBehaviour, IAction
{
    private TurnBasedActor _actor;

    protected abstract bool Strict
    {
        get;
    }

    public abstract float PointCost
    {
        get;
    }
	
    protected virtual void Start()
    {
        _actor = GetComponent<TurnBasedActor>();
    }

    public virtual bool Perform()
    {
        return _actor.TryUsePoints(PointCost, Strict);
    }
}