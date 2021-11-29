using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ker
{
    
    [RequireComponent(typeof(AudioSource))]
    public class AudioSys : MonoBehaviour
    {
        private AudioSource aud;

        private void Awake()
        {
            aud = GetComponent<AudioSource>();
        }

        public void PlaySound(AudioClip sound)
        {
            aud.PlayOneShot(sound);
        }

        public void PlaySoundRandomVolume(AudioClip sound)
        {
            float volume = Random.Range(0.7f, 1.2f);
            aud.PlayOneShot(sound, volume);
        }
    }
}


