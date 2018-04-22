using System.Threading;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerDamageReceiver : BaseDamageReceiver
    {
        public GameObject GameOverWindow;
        public GameObject HudController;
        public GameObject GameOverCamera;
        
        protected override void StartDie()
        {
            GameOverWindow.SetActive(true);
            HudController.SetActive(false);
            GameOverCamera.SetActive(true);

            GameManager.Instance.EndGame = true;
            Cursor.visible = true;
            
            Destroy(gameObject);
            
        }
    }
}