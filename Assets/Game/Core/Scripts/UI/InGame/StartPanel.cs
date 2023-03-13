using System;
using System.Collections;
using System.Collections.Generic;
using Core.Managers;
using Michsky.MUIP;
using UnityEngine;
using UnityEngine.UI;


namespace Core.UI
{
    public class StartPanel : MonoBehaviour
    {
        [Header("Main Menu Panel")] 
        
        [SerializeField] private GameObject mainMenuPanel;
        [SerializeField] private Button startButton;
        [SerializeField] private Button optionsButton;
        [SerializeField] private Button quitButton;
        

        private void Start()
        {
            AddListeners();
            
        }

        private void SetPanel(bool status)
        {
            mainMenuPanel.SetActive(status);
        }

        private void AddListeners()
        {
            startButton.onClick.AddListener(StartTheLevel);
            optionsButton.onClick.AddListener(OpenOptions);
            quitButton.onClick.AddListener(QuitGame);
        }

        private void QuitGame()
        {
            //todo
        }

        private void OpenOptions()
        {
            //todo
        }

        private void StartTheLevel()
        {
            SetPanel(false);
            GameController.Instance.InvokeOnLevelStart();
        }
    }
}