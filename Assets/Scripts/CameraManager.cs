using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class CameraManager : MonoBehaviour
    {
        public TurnBasedActor Player;
        public Camera CameraPlayer;
        public Camera CameraGlobal;

        private void Start()
        {
            Player.TurnStarted+= PlayerOnTurnStarted;
            Player.TurnEnded += PlayerOnTurnEnded;
        }

        private void OnDestroy()
        {
            Player.TurnStarted -= PlayerOnTurnStarted;
            Player.TurnEnded -= PlayerOnTurnEnded;
        }

        private void PlayerOnTurnEnded()
        {
            CameraPlayer.gameObject.SetActive(false);
            CameraGlobal.gameObject.SetActive(true);
        }

        private void PlayerOnTurnStarted()
        {
            CameraGlobal.gameObject.SetActive(false);
            CameraPlayer.gameObject.SetActive(true);
        }
    }
}