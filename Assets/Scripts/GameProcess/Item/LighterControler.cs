using UnityEngine;
using UnityEngine.InputSystem;

namespace gameProcess
{
    public class LighterControler : MonoBehaviour
    {
        public Light LightSource;
        public bool IsActive = true;
        public AudioSource SwitchAudio;
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                IsActive = !IsActive;
                LightSource.enabled = IsActive;
                SwitchAudio.Play();
            }
        }
    }
}