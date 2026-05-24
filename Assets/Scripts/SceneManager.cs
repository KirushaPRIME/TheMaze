using Unity.VectorGraphics;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    [SerializeField] Scene[] Scenes;
    void Start()
    {
        DontDestroyOnLoad(gameObject);

    }

    void Update()
    {
        
    }
}
