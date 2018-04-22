using UnityEngine;

namespace DefaultNamespace
{
    public struct TurnFrameState
    {
        private readonly Vector3 _position;
        private readonly Quaternion _rotation;

        public TurnFrameState(Vector3 position, Quaternion rotation)
        {
            _position = position;
            _rotation = rotation;
        }

        public void Apply(Transform transform)
        {
            transform.position = _position;
            transform.rotation = _rotation;
        }
        
    }
}