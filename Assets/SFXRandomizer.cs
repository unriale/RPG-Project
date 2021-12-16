using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXRandomizer : MonoBehaviour
{
    [SerializeField] private AudioClip[] audioClips = null;

    private AudioSource audioSource = null;

    public void PlayRandomAudioClip()
    {
        if (audioClips != null)
        {
            int index = UnityEngine.Random.Range(0, audioClips.Length);
            var randomClip = audioClips[index];
            audioSource.PlayOneShot(randomClip);
        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
}
