using UnityEngine;

namespace MyGame.General
{
    public class Billboard : MonoBehaviour
    {
        void Update()
        {
            if (UnityEngine.Camera.main != null)
            {
                transform.forward = UnityEngine.Camera.main.transform.forward;
            }
            else
            {
                Debug.LogWarning("Main Camera not found!");
            }
        }
    }
}