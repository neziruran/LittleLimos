using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Core.Managers;
using Core.Door;
using Core.Player;
using DG.Tweening;
using TMPro;

namespace Core.UI
{
    
    public class LevelCompletePanel : MonoBehaviour
    {
        #region  Variables
        
        [SerializeField] private Button nextLevelButton;
        [SerializeField] private GameObject levelEndPanel;
        #endregion

        #region Built-In Method
        private void Awake()
        {
            GameController.Instance.SubscribeOnLevelStart(OnLevelStart,true);
            GameController.Instance.SubscribeOnLevelComplete(OnLevelEnd,true);
            AddListeners();
            SetPanel(false);
            
            
        }
        
        private void OnDisable()
        {
            GameController.Instance.SubscribeOnLevelStart(OnLevelStart,false);
            GameController.Instance.SubscribeOnLevelComplete(OnLevelEnd,false);
        }

        #endregion
        
        #region  Custom Methods

        private void ResetPanel()
        {
            // reset values
        }

        private void AddListeners()
        {
            nextLevelButton.onClick.AddListener(OpenNextLevel);
        }
        
        private void SetPanel(bool status)
        {
            levelEndPanel.SetActive(status);
        }

        private void OpenNextLevel()
        {
            GameController.Instance.InvokeOnLoadingStart();
            SetPanel(false);
        }

        #region Event Methods

        private void OnLevelStart()
        {
            SetPanel(false);
        }

        private void OnLevelEnd()
        {
            ResetPanel();
            DOVirtual.DelayedCall(2f, ()=>SetPanel(true));
            //SetPanel(true);
        }

        #endregion

        #endregion
    }

}
