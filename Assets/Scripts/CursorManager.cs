using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class CursorManager : MonoBehaviour
    {
        public TurnBasedActor PlayerActor;

        private void Start()
        {
            PlayerActor.TurnStarted += PlayerActorOnTurnStarted;
        }

        private void PlayerActorOnTurnStarted()
        {
            Cursor.visible = false;
        }

        private void Update()
        {
            if (PlayerActor.CurrentActionPoints <= 0f)
            {
                Cursor.visible = true;
            }
        }

        public void ShowCursor()
        {
            Cursor.visible = true;
        }
       
        
    }
}