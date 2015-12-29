using UnityEngine;

namespace swantiez.unity.tools.ui
{
    public class DisableComponentOnWeb : MonoBehaviour
    {
        void Start()
        {
            gameObject.SetActive(!Application.isWebPlayer);
        }
    }
}
