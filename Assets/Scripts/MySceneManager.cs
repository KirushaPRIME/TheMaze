using UnityEngine;
using UnityEngine.SceneManagement;


namespace WorkingWithScenes
{
    public enum Scenes { BootMenu, Menu, Game}

    public class MySceneManager : MonoBehaviour
    {
        void Start()
        {
            DontDestroyOnLoad(gameObject);
            SettingManager.CheakConfigurationSettings();
            LoadScene(Scenes.Menu);
        }

        public static void LoadScene(Scenes scene)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene((int)scene);
        }

        public static void ExitGame()
        {
            Application.Quit();
        }
    }
}
