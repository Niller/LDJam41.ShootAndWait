using System.Collections;
using UnityEngine;

public class FireAction : BaseAction
{
    public GameObject Bullet;
    public GameObject BulletOut;
    
    private bool _strict;
    private float _pointCost;

    private float _cooldown = 0.5f;
    private bool _onCooldown;
    

    public bool OnCooldown
    {
        get
        {
            return _onCooldown;
        }
    }

    protected override bool Strict
    {
        get
        {
            return true;
        }
    }

    public override float PointCost
    {
        get
        {
            return 3;
        }
    }

    public override bool Perform()
    {
        if (_onCooldown)
        {
            return true;
        }
        
        var baseReturn = base.Perform();
        
        if (baseReturn)
        {
            SimplePool.Spawn(Bullet, BulletOut.transform.position, BulletOut.transform.rotation);
            StartCoroutine(WaitCooldown());
            return true;
        }

        return false;
    }

    public IEnumerator WaitCooldown()
    {
        _onCooldown = true;
        yield return new WaitForSeconds(_cooldown);
        _onCooldown = false;
    }
}