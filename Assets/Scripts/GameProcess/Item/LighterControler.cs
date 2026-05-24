using UnityEngine;
using UnityEngine.InputSystem;

namespace gameProcess
{
    public class LighterControler : MonoBehaviour
    {
        public Light LightSource;
        public bool IsActive = false;
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                IsActive = !IsActive;
                LightSource.enabled = IsActive;
            }
        }
    }
}