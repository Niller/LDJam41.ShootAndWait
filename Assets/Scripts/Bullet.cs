using UnityEngine;

namespace DefaultNamespace
{
    public class Bullet : MonoBehaviour
    {
        public float Speed = 65f;
        public float MaxDistance = 100f;

        private float _currentDistance = 0f;

        private void OnEnable()
        {
            _currentDistance = 0;
        }
        
        private void Update()
        {
            var deltaDistance = Vector3.forward * Speed * Time.smoothDeltaTime;
            transform.Translate(deltaDistance);
            _currentDistance += deltaDistance.magnitude;

            if (_currentDistance >= MaxDistance)
            {
                SimplePool.Despawn(gameObject);
            }
        }
    }
}