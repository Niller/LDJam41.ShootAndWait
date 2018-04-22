using System;
using System.Runtime.Serialization.Formatters;
using DefaultNamespace;
using DefaultNamespace.Actions;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
	public FireAction FireAction;
	public MoveAction MoveAction;
	public RotationAction RotationAction;
	public TurnState TurnState;
	public TurnBasedActor Actor;

	private bool _wasChanged;
	

	private Rigidbody _rigidbody;

	private void Awake()
	{
		GetComponent<BaseDamageReceiver>().MaxHealth = GameSettings.DifficultySettings.PlayerHp;
		GetComponent<BaseDamageReceiver>().Health = GameSettings.DifficultySettings.PlayerHp;
	}
	
	private void Start ()
	{
		_rigidbody = GetComponent<Rigidbody>();
		_rigidbody.useGravity = false;
		_rigidbody.freezeRotation = true;
		
		HotKeysController.Instance.OnBackspaceDown += InstanceOnOnBackspaceDown;
		HotKeysController.Instance.OnSpaceDown += InstanceOnOnSpaceDown;
		Actor.TurnStarted += ActorOnTurnStarted;
		
	}

	private void InstanceOnOnSpaceDown()
	{
		Actor.EndTurn();
	}

	private void ActorOnTurnStarted()
	{
		TurnState.Clear();
	}

	private void OnDestroy()
	{
		HotKeysController.Instance.OnBackspaceDown -= InstanceOnOnBackspaceDown;
		HotKeysController.Instance.OnSpaceDown -= InstanceOnOnSpaceDown;
		Actor.TurnStarted -= ActorOnTurnStarted;
	}

	private void InstanceOnOnBackspaceDown()
	{
		TurnState.Replay();
	}

	private void FixedUpdate() 
	{
		if (TurnState.IsReplay || !Actor.YourTurn || GameManager.Instance.IsPause)
		{
			_rigidbody.velocity = Vector3.zero;
			return;
		}
		
		_wasChanged = Move() || _wasChanged;
	}

	private void Update()
	{
		if (TurnState.IsReplay || !Actor.YourTurn|| GameManager.Instance.IsPause)
		{
			return;
		}
		
		_wasChanged = Rotate() || _wasChanged;
		_wasChanged = Fire() || _wasChanged;

		if (_wasChanged)
		{
			TurnState.AddState();
			_wasChanged = false;
		}
				
	}
	
	private bool Move() 
	{
		
		return MoveAction.Perform();
	}

	private bool Rotate()
	{
		return RotationAction.Perform();

	}

	private bool Fire()
	{
		if (Input.GetMouseButtonDown(0))
		{
			return FireAction.Perform();
		}

		return false;
	}

	
}
