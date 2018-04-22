using System.Runtime.InteropServices;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float RotationSpeed = 1f;
    
    public FireAction FireAction;
    public TurnBasedActor Actor;
    public NavMeshAgent Agent;
    public NavMeshObstacle Obstacle;
    public GameObject Target;

    private Vector3 _oldPosition;
    private Quaternion _oldRotation;
    private float _oldPoints;
    private float _turnTime;

    private void Awake()
    {
        GetComponent<TurnBasedActor>().MaxActionPoints = GameSettings.DifficultySettings.EnemiesActionPoints;
    }
    
    private void Start()
    {
        Actor.TurnStarted += ActorOnTurnStarted;
        _oldPosition = transform.position;
        _oldRotation = transform.rotation;
    }

    private void OnDestroy()
    {
        Actor.TurnStarted -= ActorOnTurnStarted;	
    }

    private void ActorOnTurnStarted()
    {
        Agent.enabled = true;
        Obstacle.enabled = false;
        Agent.SetDestination(Target.transform.position);
        _oldPoints = Actor.CurrentActionPoints;
        _turnTime = 0f;
    }

    private void Update()
    {
        if (!Actor.YourTurn || GameManager.Instance.EndGame || GameManager.Instance.IsPause)
        {
            return;
        }

        _turnTime += Time.deltaTime;

        UpdateLogic();
		
        _oldPosition = transform.position;
        _oldRotation = transform.rotation;
        _oldPoints = Actor.CurrentActionPoints;

        if (_turnTime >= 10f)
        {
            Agent.isStopped = true;
            Agent.enabled = false;
            Obstacle.enabled = true;
            Actor.EndTurn();
        }
    }

    private void UpdateLogic()
    {
        if (FireAction.PointCost <= Actor.CurrentActionPoints)
        {
            if (CheckVisibility() && !CheckLookingAt())
            {
                Agent.isStopped = true;

                var angleDiff = Vector3.SignedAngle(transform.forward, Target.transform.position - transform.position, Vector3.up);
                
                transform.Rotate(new Vector3(0, Mathf.Sign(angleDiff) * RotationSpeed * Time.smoothDeltaTime, 0));
                var angleDiff1 = Vector3.SignedAngle(transform.forward, Target.transform.position - transform.position, Vector3.up);
                if (Mathf.Sign(angleDiff) != Mathf.Sign(angleDiff1))
                {
                    transform.LookAt(Target.transform);
                    transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0);
                }
                else
                {
                    return;
                }
                
                
                if (!Actor.TryUsePoints(Quaternion.Angle(transform.rotation, _oldRotation) * 0.01f, false))
                {
                    Agent.isStopped = true;
                    Agent.enabled = false;
                    Obstacle.enabled = true;
                    return;
                }

                

            }

            if (CheckVisibility() && CheckLookingAt())
            {
                if (FireAction.OnCooldown)
                {
                    Agent.isStopped = true;
                }

                if (FireAction.Perform())
                {
                    return;
                }
            }
        }

        var dist = Vector3.Distance(transform.position, _oldPosition);
        Agent.isStopped = false;
        Agent.updatePosition = true;
        if (!Actor.TryUsePoints(dist, false))
        {
            Agent.isStopped = true;
            Agent.enabled = false;
            Obstacle.enabled = true;
        }

        //Avoid stuck
        if (FireAction.PointCost > Actor.CurrentActionPoints)
        {
            if (Mathf.Approximately(_oldPoints, Actor.CurrentActionPoints))
            {
                Agent.isStopped = true;
                Agent.enabled = false;
                Obstacle.enabled = true;
                Actor.EndTurn();
            }
        }
    }

    private bool CheckVisibility()
    {
        RaycastHit hit;
        if (Physics.Linecast(transform.position, Target.GetComponent<Renderer>().bounds.center, out hit))
        {
            
            return hit.collider.gameObject.layer == LayerMask.NameToLayer("Player");
        }

        return false;
    }

    private bool CheckLookingAt()
    {
        var angle = 0.5f;
        if (Vector3.Angle(transform.forward, Target.transform.position - transform.position) < angle)
        {
            return true;
        }

        return false;
    }
}