using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MoveAction : BaseAction
{
    public float MoveSpeed = 0.1f;
    public float MaxVelocityChange = 10.0f;
    public float Gravity = 10.0f;

    private Rigidbody _rigidbody;

    protected override bool Strict
    {
        get
        {
            return false;
        }
    }

    public override float PointCost
    {
        get
        {
            return 0.01f;
        }
    }

    protected override void Start()
    {
        base.Start();
        _rigidbody = GetComponent<Rigidbody>();
    }
    
    public override bool Perform()
    {
        Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (targetVelocity == Vector3.zero)
        {
            _rigidbody.velocity = Vector3.zero;
            return false;
        }
        
        var baseReturn = base.Perform();
        
        if (baseReturn)
        {           
            targetVelocity = transform.TransformDirection(targetVelocity);
            targetVelocity *= MoveSpeed;

            Vector3 velocity = _rigidbody.velocity;
            Vector3 velocityChange = (targetVelocity - velocity);
            velocityChange.x = Mathf.Clamp(velocityChange.x, -MaxVelocityChange, MaxVelocityChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -MaxVelocityChange, MaxVelocityChange);
            velocityChange.y = 0;
            _rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);
 
            _rigidbody.AddForce(new Vector3 (0, -Gravity * _rigidbody.mass, 0));
            return true;
        }

        _rigidbody.velocity = Vector3.zero;

        return false;
    }
}