using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ker
{
    public class SceneController : MonoBehaviour
    {
        public void LoadScene(string nameScene)
        {
            SceneManager.LoadScene(nameScene);
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}
