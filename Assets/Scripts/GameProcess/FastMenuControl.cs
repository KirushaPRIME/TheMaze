using UnityEngine;
using UnityEngine.UI;
using WorkingWithScenes;
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
                if (IsMenuActive)
                {
                    ClouseMenu();
                }
                if (!IsMenuActive)
                {
                    UseMause.multiplier = 0;
                    IsMenuActive = true;
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
            ClouseMenu();
        }

        public void ClickOnReturnMenu()
        {
            MySceneManager.LoadScene(Scenes.Menu);
        }

        void ClouseMenu()
        {
            UseMause.multiplier = SettingManager.GetFloatSetting("MauseSensitivity");
            IsMenuActive = false;
            Curs.lockState = CursorLockMode.Locked;
            Curs.visible = false;
            Menu.SetActive(false);
        }
    }
}