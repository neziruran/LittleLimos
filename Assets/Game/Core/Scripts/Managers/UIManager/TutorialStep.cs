using System;
using System.Collections;
using System.Collections.Generic;
using Core.Player;
using Core.Utilities;
using DG.Tweening;
using Michsky.MUIP;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core.Managers
{
    public class TutorialStep : MonoBehaviour
    {
        [SerializeField] private TutorialContent _UIContent;
        [SerializeField] private bool isFirstStep;
        [SerializeField] [Range(1,6)] private float closeDelay = 1;
        private bool _hasTriggered = false;
        

        private void Start()
        {
            if(GameController.Instance.IsTutorialLevel)
                GameController.Instance.SubscribeOnLevelStart(OnLevelStart,true);
            
        }

        private void OnDisable()
        {
            if(GameController.Instance.IsTutorialLevel)
                GameController.Instance.SubscribeOnLevelStart(OnLevelStart,false);
        }

        private void OnLevelStart()
        {
            _hasTriggered = false;
            AutoTrigger();
        }

        private void AutoTrigger()
        {
            if (isFirstStep && GameController.Instance.IsTutorialLevel)
            {
                var duration = 1f;
                var positionOffset = -.5f;
                var levelStartDelay = 6f;
                this.transform.DOMoveZ(positionOffset, duration).SetRelative().SetDelay(levelStartDelay);            
            }
            
        }

        private void OnTutorialTriggered()
        {
            ShowTutorial();
            _hasTriggered = true;
            DOVirtual.DelayedCall(closeDelay, HideTutorial);
        }

        private void ShowTutorial()
        {
            PlayerController.SetPlayerInput(false);
            _UIContent.ShowContent();

        }
        private void HideTutorial()
        {
            PlayerController.SetPlayerInput(true);
            _UIContent.HideContent();
        }

        private void OnTriggerEnter(Collider other)
        {
            var isPlayer = other.TryGetComponent(out PlayerController playerController);
            var canTrigger = isPlayer && !_hasTriggered  && PlayerController.PlayerActive;
            if (canTrigger)
            {
                OnTutorialTriggered();
            }
        }
    }

}
