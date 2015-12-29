using UnityEngine;
using SceneManager = UnityEngine.SceneManagement.SceneManager;

namespace swantiez.unity.tools.ui
{
    public class SceneSwitcher : MonoBehaviour
    {
        public string sceneName;

        public void OnSwitchScene()
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
