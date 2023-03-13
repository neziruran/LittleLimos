using System;
using System.Collections;
using System.Collections.Generic;
using Core.Managers;
using UnityEngine;

namespace Core.UI
{
    public class Fader : MonoBehaviour
    {
        [SerializeField] private GameObject fadePanel;
        private CanvasGroup _renderer;

        private const float Open = 1f;
        private const float Close = 0f;

        private void Awake()
        {
            SetPanel(false);
            _renderer = GetComponent<CanvasGroup>();
            
            GameController.Instance.SubscribeOnLevelStart(OnFadeOut,true);
            GameController.Instance.SubscribeOnLoadingStart(OnFadeIn,true);
            GameController.Instance.SubscribeOnLoadingComplete(OnFadeOut,true);

        }

        private void OnDisable()
        {
            GameController.Instance.SubscribeOnLoadingStart(OnFadeIn,false);
            GameController.Instance.SubscribeOnLoadingComplete(OnFadeOut,false);
            
            GameController.Instance.SubscribeOnLevelStart(OnFadeOut,true);



        }

        private void SetPanel(bool status)
        {
            fadePanel.SetActive(status);
        }

        private void OnFadeIn()
        {
            SetPanel(true);
            StartCoroutine(FadeIn(1.5f));
        }

        private void OnFadeOut()
        {
            SetPanel(true);
            StartCoroutine(FadeOut(1.5f));
        }
        
        private IEnumerator FadeIn(float duration)
        {
            float currentTime = 0;
            _renderer.alpha = 0;
            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                _renderer.alpha = Mathf.Lerp(Close, Open,currentTime / duration);
                yield return null;
            }
            SetPanel(false);
            
        }
        private IEnumerator FadeOut(float duration)
        {
            float currentTime = -1f;
            _renderer.alpha = 1;
            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                _renderer.alpha = Mathf.Lerp(Open, Close,currentTime / duration);
                yield return null;
            }
            SetPanel(false);
            
        }

    }

}

