using System.Diagnostics.CodeAnalysis;
using DefaultNamespace;
using UnityEngine;

public class RotationAction : BaseAction
{
    public float RotationSpeedHorizontal = 1f;
    public float RotationSpeedVertical = 1f;
    public float MinRotationVertical = -45;
    public float MaxRotationVertical = 45;
    private float _currentRotationVertical;
    private float _currentRotationHorizontal;

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

    [SuppressMessage("ReSharper", "CompareOfFloatsByEqualityOperator")]
    public override bool Perform()
    {
        
		
        var horDelta = Input.GetAxis("Mouse X");
        var verDelta = Input.GetAxis("Mouse Y");

        if (horDelta == 0f && verDelta == 0f)
        {
            return false;
        }
        
        var baseReturn = base.Perform();

        if (baseReturn)
        {

            _currentRotationVertical = MathUtilities.Normalize180Angle(transform.rotation.eulerAngles.x);
            _currentRotationHorizontal = transform.rotation.eulerAngles.y;

            float rotationVertical = -(verDelta * RotationSpeedVertical) * Time.smoothDeltaTime;
            var finalRotationVertical =
                Mathf.Clamp(rotationVertical + _currentRotationVertical, MinRotationVertical, MaxRotationVertical);

            float rotationHorizontal = horDelta * RotationSpeedHorizontal * Time.smoothDeltaTime;
            transform.rotation =
                Quaternion.Euler(finalRotationVertical, _currentRotationHorizontal + rotationHorizontal, 0);
            return true;
        }

        return false;
    }
}