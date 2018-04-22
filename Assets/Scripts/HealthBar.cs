using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class HealthBar : MonoBehaviour
    {
        public EnemyDamageReceiver DamageReceiver;
        public Slider Filler;
        public Text Name;

        private void Update()
        {
            if (DamageReceiver != null)
            {
                if (DamageReceiver.Health <= 0)
                {
                    Destroy(gameObject);
                    return;
                }
                
                var screenCoords = Camera.main.WorldToScreenPoint(DamageReceiver.transform.position);
                transform.position = screenCoords + new Vector3(0, 70, 0);
                Filler.value = DamageReceiver.Health / (float)DamageReceiver.MaxHealth;
                
            }
        }
    }
}