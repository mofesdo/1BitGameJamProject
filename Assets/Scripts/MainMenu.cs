using UnityEngine;
using UnityEngine.SceneManagement;

namespace MetroidvaniaTools
{
    public class MainMenu : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartGame();
            }
        }

        public void StartGame()
        {
            SceneManager.LoadScene("1"); // Replace with the actual name of your first game scene
        }
    }
}