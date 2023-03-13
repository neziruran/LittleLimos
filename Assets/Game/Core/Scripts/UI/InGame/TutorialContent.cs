using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Core.Utilities
{
    public class TutorialContent : MonoBehaviour
    {

        [SerializeField] private string title;
        [SerializeField] private string content;
        [SerializeField] private TMP_Text contentText;
        [SerializeField] private TMP_Text titleText;
        private GameObject _contentUI;


        private void Awake()
        {
            _contentUI = transform.GetChild(0).gameObject;
            if (_contentUI != null)
            {
                contentText.text = content;
                titleText.text = title; 
            }
            else
            {
                Debug.LogError("UI content not found check the gameObject");
            }
            
             
        }
        
        public void ShowContent()
        {
            _contentUI.SetActive(true);
        }

        public void HideContent()
        {
            _contentUI.SetActive(false);
        }


        
    }

}
