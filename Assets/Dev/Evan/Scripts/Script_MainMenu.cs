using UnityEngine;
using UnityEngine.SceneManagement;

namespace Dev.Evan.Scripts
{
    public class ScriptMainMenu : MonoBehaviour
    {
        public void PlayGame()
        {
            SceneManager.LoadScene(1);
        }

        public void BackToMain()
        {
            SceneManager.LoadScene(0);
        }

        public void ExitGame()
        {
            Application.Quit();
        }
    }
}
