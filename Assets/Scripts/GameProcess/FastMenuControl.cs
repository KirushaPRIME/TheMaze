using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;
using WorkingWithScenes;
using Curs = UnityEngine.Cursor;

namespace gameProcess{
    public class FastMenuControl : MonoBehaviour
    {

        [SerializeField] private Slider sliderSens;
        [SerializeField] private Slider sliderVolume;
        [SerializeField] private AudioMixer audioMixer;
        [SerializeField] private GameObject Menu;

        public static bool IsMenuActive { get; private set; }

        void Start()
        {
            sliderSens.onValueChanged.AddListener(delegate { SensUpdate(); });
            sliderSens.value = SettingManager.GetFloatSetting("MauseSensitivity");
            UseMause.multiplier = sliderSens.value;

            sliderVolume.onValueChanged.AddListener(delegate { VolumeUpdate(); });
            sliderVolume.value = SettingManager.GetFloatSetting("MaterVolume");
            audioMixer.SetFloat("MasterVolume", (sliderVolume.value == 0) ? -80 : Mathf.Log10(sliderVolume.value) * 20);

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
            SettingManager.SetSetting("MaterVolume", sliderSens.value);
            UseMause.multiplier = sliderSens.value;
        }

        void VolumeUpdate()
        {
            SettingManager.SetSetting("MaterVolume", sliderVolume.value);
            audioMixer.SetFloat("MasterVolume", (sliderVolume.value == 0) ? -80 : Mathf.Log10(sliderVolume.value) * 20);
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