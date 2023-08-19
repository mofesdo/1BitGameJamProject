using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetroidvaniaTools
{
    public class AudioManager : MonoBehaviour
    {
        public AudioSource musicSource; // Drag your GameObject with the Audio Source component here

        private void Start()
        {
            musicSource.Play();
        }
    }
}