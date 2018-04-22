using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace DefaultNamespace
{
    public class HitHighlight : MonoBehaviour
    {
        private const float ColorLerpTime = 2f;
        
        private Renderer _renderer;

        public VulnerabilityPart VulnerabilityPart;

        private float _currentLerpTime = ColorLerpTime;
        
        private void Start()
        {
            _renderer = GetComponent<Renderer>();
            VulnerabilityPart.Hit += VulnerabilityPartOnHit;
        }

        private void OnDestroy()
        {
            VulnerabilityPart.Hit -= VulnerabilityPartOnHit;
        }

        private void VulnerabilityPartOnHit()
        {
            _currentLerpTime = 0;
        }

        private void Update()
        {
            if (_currentLerpTime < ColorLerpTime)
            {
                _renderer.material.color = Color.Lerp(Color.red, Color.white, _currentLerpTime / ColorLerpTime);
                _currentLerpTime += Time.deltaTime;

                if (_currentLerpTime > ColorLerpTime)
                {
                    _renderer.material.color = Color.white;
                }
            }
        }
    }
}