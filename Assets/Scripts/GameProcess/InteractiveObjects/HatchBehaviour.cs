using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(IsInteractive))]
public class HatchBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject PrefEndRoom;
    [SerializeField] private AudioMixer mainMixer;

    string groupName = "AudioInPillow";

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

            for (int i = 0; i < WhoInter.transform.childCount; i++)
            {
                var AS = WhoInter.transform.GetChild(i).GetComponent<AudioSource>();
                if (AS != null)
                {
                    AS.outputAudioMixerGroup = mainMixer.FindMatchingGroups(groupName)[0];
                }
            }



            Debug.Log("You are escape!");
        }

        
    }
}
