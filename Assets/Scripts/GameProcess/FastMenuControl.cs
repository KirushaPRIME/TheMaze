using UnityEngine;
using UnityEngine.UI;
using Curs = UnityEngine.Cursor;

namespace gameProcess{
    public class FastMenuControl : MonoBehaviour
    {

        [SerializeField] private Slider slider;
        [SerializeField] private GameObject Menu;

        public static bool IsMenuActive { get; private set; }

        void Start()
        {

            slider.onValueChanged.AddListener(delegate { SensUpdate(); });
            slider.value = SettingManager.GetFloatSetting("MauseSensitivity");
            UseMause.multiplier = slider.value;

            Curs.lockState = CursorLockMode.Locked;
            Curs.visible = false;
            IsMenuActive = false;
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                IsMenuActive = !IsMenuActive;
                if (!IsMenuActive)
                {
                    Curs.lockState = CursorLockMode.Locked;
                    Curs.visible = false;
                    Menu.SetActive(false);
                }
                if (IsMenuActive)
                {
                    Curs.lockState = CursorLockMode.None;
                    Curs.visible = true;
                    Menu.SetActive(true);
                }
            }
        }

        void SensUpdate()
        {
            SettingManager.SetSetting("MauseSensitivity", slider.value);
            UseMause.multiplier = slider.value;
        }

        public void ClickContinue()
        {
            IsMenuActive = false;
            Curs.lockState = CursorLockMode.Locked;
            Curs.visible = false;
            Menu.SetActive(false);
        }
    }
}