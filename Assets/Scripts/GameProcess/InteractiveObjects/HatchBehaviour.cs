using UnityEngine;

[RequireComponent(typeof(IsInteractive))]
public class HatchBehaviour : MonoBehaviour
{
    void Start()
    {
        GetComponent<IsInteractive>().OnInteractive += EscapeFromTheMaze;
    }

    void EscapeFromTheMaze()
    {
        Debug.Log("You are escape!");
    }

    void Update()
    {
        
    }
}
