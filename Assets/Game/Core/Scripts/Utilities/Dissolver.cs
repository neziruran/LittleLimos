using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Managers;
using Core.Player;
using DG.Tweening;
using UnityEngine;

namespace Utilities
{
    public class Dissolver : MonoBehaviour
    {
        [SerializeField] private Material dissolveMat_01;
        [SerializeField] private Material dissolveMat_02;
        [SerializeField] private Material dissolveMat_03;

        [SerializeField] private float symbolAlpha = 0f;
        [SerializeField] private Material[] symbolMaterials;
        
        private const float Start = 1.5f;
        private const float End = -1.5f;

        private GameObject _dissolverCube;

        #region Built-In Methods

        private void Awake()
        {
            _dissolverCube = transform.GetChild(0).gameObject;
            _dissolverCube.SetActive(false);
            
            GameController.Instance.SubscribeOnLevelStart(OnLevelStart,true);
            GameController.Instance.SubscribeOnLevelComplete(OnLevelComplete,true);
            PlayerController.SubscribeOnOutOfBounds(OnOutOfBounds,true);
            PlayerController.SubscribeOnRespawn(OnRespawn,true);

        }

        private void OnDisable()
        {
            PlayerController.SubscribeOnOutOfBounds(OnOutOfBounds,false);
            PlayerController.SubscribeOnRespawn(OnRespawn,false);
            GameController.Instance.SubscribeOnLevelStart(OnLevelStart,false);
            GameController.Instance.SubscribeOnLevelComplete(OnLevelComplete,false);


        }

        #endregion

        #region Calls

        private async void OnLevelStart()
        {
            transform.rotation = Quaternion.Euler(0,0,0);
            await Task.Delay(1);
            StartCoroutine(DissolveOut(2f));
        }
        
        
        private void OnLevelComplete()
        {
            transform.rotation = Quaternion.Euler(0,0,0);
            StartCoroutine(DissolveIn(2f));
        }


        private void OnRespawn()
        {
            transform.rotation = Quaternion.Euler(0,0,0);
            StopCoroutine(DissolveIn(0f));
            StartCoroutine(DissolveOut(1f));

        }

        private void OnOutOfBounds()
        {
            transform.rotation = Quaternion.Euler(0,0,0);
            StopCoroutine(DissolveOut(0f));
            StartCoroutine(DissolveIn(1f));

        }


        #endregion

        #region Utils

        private void SetValues(bool dissolveIn)
        {
            if (dissolveIn)
            {
                dissolveMat_01.SetFloat("_CutoffHeight",Start);
                dissolveMat_02.SetFloat("_CutoffHeight",Start);
                dissolveMat_03.SetFloat("_CutoffHeight",Start);
            }
            else
            {
                dissolveMat_01.SetFloat("_CutoffHeight",End);
                dissolveMat_02.SetFloat("_CutoffHeight",End);
                dissolveMat_03.SetFloat("_CutoffHeight",End);
            }
        }

        #endregion

        #region DissolveRoutines

        private IEnumerator DissolveIn(float duration)
        {
            SetValues(true);
            _dissolverCube.SetActive(true);
            
            float currentTime = 0f;
            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                dissolveMat_01.SetFloat("_CutoffHeight",Mathf.Lerp(Start, End, currentTime / duration));
                dissolveMat_02.SetFloat("_CutoffHeight",Mathf.Lerp(Start, End, currentTime / duration));
                dissolveMat_03.SetFloat("_CutoffHeight",Mathf.Lerp(Start, End, currentTime / duration));
                yield return null;
            }
            _dissolverCube.SetActive(false);
        }
        
        private IEnumerator DissolveOut(float duration)
        {
            SetValues(false);
            _dissolverCube.SetActive(true);

            float currentTime = -1.5f;
            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                dissolveMat_01.SetFloat("_CutoffHeight",Mathf.Lerp(End, Start, currentTime / duration));
                dissolveMat_02.SetFloat("_CutoffHeight",Mathf.Lerp(End, Start, currentTime / duration));
                dissolveMat_03.SetFloat("_CutoffHeight",Mathf.Lerp(End, Start, currentTime / duration));
                yield return null;
            }
            _dissolverCube.SetActive(false);

        }

        #endregion
       
        
    }
 
}
