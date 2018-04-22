using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class CursorManager : MonoBehaviour
    {
        public TurnBasedActor PlayerActor;

        private void Awake()
        {
            PlayerActor.TurnStarted += PlayerActorOnTurnStarted;
        }

        private void PlayerActorOnTurnStarted()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            if (PlayerActor.CurrentActionPoints <= 0f)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }

        public void ShowCursor()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
       
        
    }
}