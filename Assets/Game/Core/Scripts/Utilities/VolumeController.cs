using System;
using System.Collections;
using System.Collections.Generic;
using Core.InputS;
using Core.Player;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using Utilities;

namespace Core.Utilities
{
    public class VolumeController : MonoBehaviour
    {
        #region Components

        private static Volume _globalVolume;
        private GameEnums.PositionState _playerState;
        
        #endregion
        
        #region Variables
        
        [Header("Lens Distortion Settings")]
        [Range(0.1f,1f)]
        [SerializeField] private float lensDistortionIntensity;
        [Range(0.1f,1f)]
        [SerializeField] private float lensDistortionLifeTimeChange;
        [SerializeField] private Ease easeType = Ease.InOutSine;
        
        #endregion

        #region Built-In Methods

        private void Awake()
        {
            _globalVolume = GetComponent<Volume>();
        }

        private void Update()
        {
            if (InputListener.CameraLookUpPressed)
            {
                EnableDistortion();
            }
            else
            {
                DisableDistortion();
            }
        }

        private void EnableDistortion()
        {           
            _globalVolume.profile.TryGet(out LensDistortion lensDistortion);
            lensDistortion.active = true;

            lensDistortion.intensity.value = DOVirtual.EasedValue
            (lensDistortion.intensity.value, 
                lensDistortionIntensity, 
                lensDistortionLifeTimeChange,easeType);
        }

        private void DisableDistortion()
        {
            _globalVolume.profile.TryGet(out LensDistortion lensDistortion);
            lensDistortion.intensity.value = 0f;
            lensDistortion.active = false;
        }
        

        #endregion
        
    }

}
