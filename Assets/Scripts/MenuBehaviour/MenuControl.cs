using UnityEngine;
using WorkingWithScenes;

namespace menuSpace{
    public class MenuControl : MonoBehaviour
    {
        void Start()
        {

        }

        public static void ClickOnPlay()
        {
            MySceneManager.LoadScene(Scenes.Game);
        }

        public void ClickOnExit()
        {
            MySceneManager.ExitGame();
        }
    }
}