using UnityEngine;
using VS.BumpRock.Utils;

namespace VS.BumpRock.Menu
{
    public class TestMenu : MonoBehaviour
    {
        private const float SPACE_BETWEEN_BUTTONS = 20f;
        private const float BUTTON_WIDTH = 200f;

        [HideInInspector]
        public string[] testScenes;
        [Range(1,5)]
        public int nbColumns;

        private float buttonHeight;
        private float margin;
        private Rect[] buttonsPos;

        void Start()
        {
            int nbRows = Mathf.CeilToInt((testScenes.Length+1)/(float) nbColumns);
            buttonHeight = (Screen.height - SPACE_BETWEEN_BUTTONS * (nbRows + 1)) / nbRows;
            margin = (Screen.width - nbColumns*(BUTTON_WIDTH + SPACE_BETWEEN_BUTTONS) + SPACE_BETWEEN_BUTTONS)/2f;
            //Debug.Log("Screen.height=" + Screen.height + " - buttonHeight=" + buttonHeight + " - BUTTON_WIDTH=" + BUTTON_WIDTH);
            buttonsPos = new Rect[testScenes.Length+1];

            for (int i = 0; i < nbRows; ++i)
            {
                for (int j = 0; j < nbColumns; ++j)
                {
                    buttonsPos[i * nbColumns + j] = new Rect(margin + j * (BUTTON_WIDTH + SPACE_BETWEEN_BUTTONS),
                                                             SPACE_BETWEEN_BUTTONS + i * (buttonHeight + SPACE_BETWEEN_BUTTONS),
                                                             BUTTON_WIDTH, buttonHeight);
                }
            }
        }

        public void addTestScene(string testScene)
        {
            testScenes = CollectionUtils.AddToArray(testScenes, testScene);
        }

        public void removeTestScene(string testScene)
        {
            testScenes = CollectionUtils.RemoveFromArray(testScenes, testScene);
        }

        void OnGUI()
        {
            for (int i = 0; i < testScenes.Length; ++i)
            {
                string sceneName = testScenes[i];
                Rect buttonPos = buttonsPos[i];

                if (GUI.Button(buttonPos, "Scene " + sceneName))
                {
                    Application.LoadLevel(sceneName);
                }
            }

            if (GUI.Button(buttonsPos[testScenes.Length], "Exit"))
            {
                Application.Quit();
            }
        }
    }
}