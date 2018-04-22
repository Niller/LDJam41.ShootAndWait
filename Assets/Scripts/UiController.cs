using System;
using DefaultNamespace.Actions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class UiController : MonoBehaviour
    {
        public GameObject GameOverWindow;
        public GameObject WinDialog;
        public GameObject TotalWinDialog;
        public GameObject InfoWindow;

        public static UiController Instance;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            HotKeysController.Instance.OnF1Down += InstanceOnOnF1Down;
        }

        private void InstanceOnOnF1Down()
        {
            if (InfoWindow.activeSelf)
            {
                CloseHintWindow();
                return;
            }
            
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            GameManager.Instance.IsPause = true;
            HudController.Instance.Aim.SetActive(false);
            HudController.Instance.HealthBarContainer.SetActive(false);
            InfoWindow.SetActive(true);
        }

        public void Restart()
        {
            SceneManager.LoadScene(1);
        }
        
        public void RestartNextDifficulty()
        {
            GameSettings.CurrentDifficulty++;
            SceneManager.LoadScene(1);
        }

        public void OpenWinDialog()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            HudController.Instance.Aim.SetActive(false);
            if (GameSettings.CurrentDifficulty < 2)
            {
                WinDialog.SetActive(true);
            }
            else
            {
                TotalWinDialog.SetActive(true);
            }
        }

        public void CloseHintWindow()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            GameManager.Instance.IsPause = false;
            HudController.Instance.Aim.SetActive(true);
            HudController.Instance.HealthBarContainer.SetActive(true);
            InfoWindow.SetActive(false);
        }
        
    }
}