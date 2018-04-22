using UnityEngine;

namespace DefaultNamespace
{
    public class DummyDamageReceiver : MonoBehaviour
    {
        public VulnerabilityPart Body;
        public VulnerabilityPart Leg1;
        public VulnerabilityPart Leg2;
        public VulnerabilityPart Head;
        public VulnerabilityPart Hand1;
        public VulnerabilityPart Hand2;

        private void Start()
        {
            Body.Hit += HandleBodyHit;
            Head.Hit += HandleHeadHit;
            Leg1.Hit += HandleLimbHit;
            Leg2.Hit += HandleLimbHit;
            Hand1.Hit += HandleLimbHit;
            Hand2.Hit += HandleLimbHit;
        }

        private void OnDestroy()
        {
            Body.Hit -= HandleBodyHit;
            Head.Hit -= HandleHeadHit;
            Leg1.Hit -= HandleLimbHit;
            Leg2.Hit -= HandleLimbHit;
            Hand1.Hit -= HandleLimbHit;
            Hand2.Hit -= HandleLimbHit;
        }

        private void HandleBodyHit()
        {
            Debug.Log("Body hit");
        }

        private void HandleHeadHit()
        {
            Debug.Log("Head hit");
        }

        private void HandleLimbHit()
        {
            Debug.Log("Limb hit");
        } 
    }
}