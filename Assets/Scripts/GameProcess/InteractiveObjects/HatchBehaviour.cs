using UnityEngine;

[RequireComponent(typeof(IsInteractive))]
public class HatchBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject PrefEndRoom;

    void Start()
    {
        GetComponent<IsInteractive>().OnInteractive += EscapeFromTheMaze;
    }

    void EscapeFromTheMaze(GameObject WhoInter)
    {
        if (WhoInter.CompareTag("Player"))
        {
            GameObject EndRoom = Instantiate(PrefEndRoom);
            EndRoom.GetComponent<Transform>().position = new Vector3(0, -10, 0);
            if (WhoInter.GetComponent<PhisicsBodyBehaviour>() != null)
                WhoInter.GetComponent<PhisicsBodyBehaviour>().TransformObject(new Vector3(0, -10, 0));
            else
                WhoInter.GetComponent<Transform>().position = new Vector3(0, -10, 0);
        }
        Debug.Log("You are escape!");
    }
}
