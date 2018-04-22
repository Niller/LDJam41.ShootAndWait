using System;
using UnityEngine;
using Utilities.Extensions;

namespace DefaultNamespace.Actions
{
    public class HotKeysController : MonoBehaviour
    {
        public static HotKeysController Instance;

        public event Action OnBackspaceDown;
        public event Action OnSpaceDown;
        public event Action OnF1Down;
        
        private void Awake()
        {
            Instance = this;
        }
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                OnBackspaceDown.SafeInvoke();
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnSpaceDown.SafeInvoke();
            }
            if (Input.GetKeyDown(KeyCode.F1))
            {
                OnF1Down.SafeInvoke();
            }
        }
    }
}