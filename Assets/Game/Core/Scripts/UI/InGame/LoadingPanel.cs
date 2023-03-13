using System;
using System.Collections;
using System.Collections.Generic;
using Core.Managers;
using UnityEngine;
using UnityEngine.Events;

namespace Core.UI
{
    public class LoadingPanel : MonoBehaviour
    {
        
        [SerializeField] private GameObject loadingPanel;

        private void Awake()
        {
            DeactivateBar();
            GameController.Instance.SubscribeOnLoadingStart(StartLoading,true);

        }

        private void OnDisable()
        {
            GameController.Instance.SubscribeOnLoadingStart(StartLoading,false);
        }
        
        #region Loading Methods

        private void StartLoading()
        {
            Debug.LogWarning("Loading Started");
            StartCoroutine(StartLoading(3f));
        }
        
        private IEnumerator StartLoading(float duration)
        {
            yield return new WaitForSeconds(1.5f); // fade time
            loadingPanel.SetActive(true);
            float currentTime = 0;
            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                yield return null;
            }
            
            GameController.Instance.InvokeOnLoadingComplete();
            DeactivateBar();
        }

        private void DeactivateBar()
        {
            Debug.LogWarning("Loading Completed");
            loadingPanel.SetActive(false);
        }


        #endregion
        
    
    }

}
