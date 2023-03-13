using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Core.Managers;
using UnityEngine.Serialization;

namespace Core.Audio
{
    public class AudioManager : SingletonBehaviour<AudioManager>
    {
        [SerializeField] private SoundClass[] sounds;
        [SerializeField] private AudioSource sfxSource;
        [SerializeField] private AudioSource musicSource; 
        
        public void PlaySound(string input)
        {
            SoundClass sounds = Array.Find(this.sounds, SoundClass => SoundClass.SoundName == input);
            if(sounds == null)
            {
                return;
            }
            sounds.AudioSource = sfxSource;
            sounds.AudioSource.clip = sounds.LevelSound;
            sounds.AudioSource.loop = sounds.IsLoop;
            sounds.AudioSource.volume = sounds.Volume;
            sounds.AudioSource.Play();
        }
        public void StopSound(string input)
        {
            SoundClass sounds = Array.Find(this.sounds, SoundClass => SoundClass.SoundName == input);
            if(sounds == null)
            {
                return;
            }
            sounds.AudioSource = sfxSource;
            sounds.AudioSource.clip = sounds.LevelSound;
            sounds.AudioSource.loop = sounds.IsLoop;
            sounds.AudioSource.volume = sounds.Volume;
            sounds.AudioSource.Pause();
        }

        private void PlayLevelMusic()
        {
            StartCoroutine(FadeInRoutine(3f,1f));
        }

        private void DecreaseLevelMusic()
        {
            StartCoroutine(FadeOutRoutine(1.5f,0.1f));
        }
        
        private void StopLevelMusic()
        {
            StartCoroutine(FadeOutRoutine(1.5f,0f));
        }

        private void SetMainMusic(string input)
        {
            SoundClass sounds = Array.Find(this.sounds, SoundClass => SoundClass.SoundName == input);
            if(sounds == null)
            {
                return;
            }
            sounds.AudioSource = musicSource;
            sounds.AudioSource.clip = sounds.LevelSound;
            sounds.AudioSource.loop = sounds.IsLoop;
            sounds.AudioSource.Play();

        }
        
        private IEnumerator FadeInRoutine(float duration, float targetVolume)
        {
            musicSource.volume = 0f;
            string mainMusic = GameController.Instance.GetLevelData().LevelMusicKey;
            SetMainMusic(mainMusic);
            yield return new WaitForSeconds(1f);
            float currentTime = 0;
            float start = musicSource.volume;
            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                musicSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
                yield return null;
            }
        }

        private IEnumerator FadeOutRoutine(float duration, float targetVolume)
        {
            float currentTime = 0;
            float start = musicSource.volume;
            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                musicSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
                yield return null;
            }
        }

        #region Built-In Methods

        private void Awake()
        {
            GameController.Instance.SubscribeOnLevelStart(PlayLevelMusic,true);
            GameController.Instance.SubscribeOnLevelComplete(DecreaseLevelMusic,true);
            GameController.Instance.SubscribeOnLoadingStart(StopLevelMusic,true);

        }

        private void OnDisable()
        {
            GameController.Instance.SubscribeOnLevelStart(PlayLevelMusic,false);
            GameController.Instance.SubscribeOnLevelComplete(DecreaseLevelMusic,false);
            GameController.Instance.SubscribeOnLoadingStart(StopLevelMusic,false);
            

        }

        #endregion

    }
    
    [Serializable]
    public class SoundClass
    {
        public AudioClip LevelSound;
        public string SoundName;
        [Range(0,1)]
        public float Volume;
        public bool IsLoop;
    
        [HideInInspector]
        public AudioSource AudioSource;
    }

}
