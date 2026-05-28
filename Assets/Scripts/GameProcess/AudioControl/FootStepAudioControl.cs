using System;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class FootStepAudioControl : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField]PhisicsBodyBehaviour PBB;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (PBB == null)
        {
            Debug.Log("WARING: PhisicsBodyBehaviour no added!");
            GetComponent<FootStepAudioControl>().enabled = false;
        }

        if (audioSource.clip == null)
        {
            Debug.Log("WARING: Sound no added!");
            GetComponent<FootStepAudioControl>().enabled = false;
        }
    }

    void Update()
    {
        if (PBB.IsGrounded && PBB.Velocity.magnitude > 0)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.pitch = 0.9f + UnityEngine.Random.value * 0.2f;
                audioSource.Play();
            }
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Pause();
            }
        }
    }
}
