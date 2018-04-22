using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class MainMenuController : MonoBehaviour
    {
        public void Play()
        {
            SceneManager.LoadScene(1);
        }

        public void OnDifficultyChange(int index)
        {
            GameSettings.CurrentDifficulty = index;
        }
    }
}