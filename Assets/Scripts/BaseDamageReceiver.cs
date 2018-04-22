using System;
using System.Collections;
using UnityEngine;
using Utilities.Extensions;

namespace DefaultNamespace
{
    public class BaseDamageReceiver : MonoBehaviour
    {
        public VulnerabilityPart Part;
        public int Health;
        public int MaxHealth;

        public GameObject DieEffect;
        public event Action<BaseDamageReceiver> Died; 
        
        private void Start()
        {
            Part.Hit += HandleHit;   
        }

        private void HandleHit()
        {
            GetComponent<AudioSource>().Play();
            Health -= 10;

            if (Health <= 0)
            {
                StartDie();
            }
        }

        protected virtual void StartDie()
        {
            Died.SafeInvoke(this);
            StartCoroutine(Die());
        }

        private IEnumerator Die()
        {
            DieEffect.SetActive(true);
            yield return new WaitForSeconds(1f);
            Destroy(gameObject);
            
        }

        private void OnDestroy()
        {
            Part.Hit -= HandleHit;
        }

    }
}