using System.Collections;
using System.Collections.Generic;
using Moss;
using UnityEngine;
using UnityEngine.Serialization;

namespace TOTL
{
    public class AudioManager : MonoDontDestroySingleton<AudioManager>
    {
        [SerializeField] private AudioClip bgAudioClip;

        [FormerlySerializedAs("audioSource")] [SerializeField]
        private AudioSource bgAudioSource;

        [SerializeField] private AudioSource sfxAudioSource;

        [SerializeField] private AudioClip failSfx;
        [SerializeField] private AudioClip successSfx;
        [SerializeField] private AudioClip dingSfx;
        [SerializeField] private List<AudioClip> dongSfx;

        protected void Start()
        {
            bgAudioSource.loop = true;
            bgAudioSource.clip = bgAudioClip;
            bgAudioSource.Play();
        }

        public void Play(string audioName)
        {
            StartCoroutine(PlayCoroutine());

            IEnumerator PlayCoroutine()
            {
                if (audioName == "fail")
                {
                    sfxAudioSource.PlayOneShot(failSfx, 0.6f);
                    bgAudioSource.volume *= 0.2f;
                    yield return new WaitForSeconds(failSfx.length * 0.8f);
                    bgAudioSource.volume *= 5f;
                }
                else if (audioName == "success")
                {
                    sfxAudioSource.PlayOneShot(successSfx, 0.6f);
                    bgAudioSource.volume *= 0.2f;
                    yield return new WaitForSeconds(successSfx.length * 0.8f);
                    bgAudioSource.volume *= 5f;
                }
                else if (audioName == "ding")
                {
                    sfxAudioSource.PlayOneShot(dingSfx, 0.6f);
                    bgAudioSource.volume *= 0.5f;
                    yield return new WaitForSeconds(dingSfx.length * 0.8f);
                    bgAudioSource.volume *= 2f;
                }
                else if (audioName == "dong")
                {
                    // sfxAudioSource.PlayOneShot(dongSfx.GetRandom(), 0.6f);
                    // bgAudioSource.volume *= 0.5f;
                    // yield return new WaitForSeconds(dingSfx.length * 0.8f);
                    // bgAudioSource.volume *= 2f;
                }
            }
        }
    }
}