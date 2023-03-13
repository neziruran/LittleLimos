using System;
using Core.Managers;
using UnityEngine;
using Utilities;
using Core.Player;
using DG.Tweening;

namespace Core.Symbol
{
    public class PlayerSymbol : SymbolBase
    {
        #region Variables

        private Renderer _renderer01;
        private Renderer _renderer02;

        #endregion
        
        #region Built-In Methods
        private new void Awake()
        {
            base.Awake();
            
            if (GetSymbolData() == null) return;
            
            GetRenderers();
            SetMaterials();
        }

        private new void OnEnable()
        {
            base.OnEnable();
            
            GameController.Instance.SubscribeOnLevelStart(OnLevelStart,true);
            GameController.Instance.SubscribeOnLevelComplete(OnLevelComplete,true);
            PlayerController.SubscribeOnOutOfBounds(OnRespawnStart,true);
            PlayerController.SubscribeOnRespawn(OnRespawnComplete,true);

        }

        private new void OnDisable()
        {
            base.OnDisable();
            
            GameController.Instance.SubscribeOnLevelStart(SetMaterials,false);
            GameController.Instance.SubscribeOnLevelComplete(OnLevelComplete,false);

            PlayerController.SubscribeOnOutOfBounds(OnRespawnStart,false);
            PlayerController.SubscribeOnRespawn(OnRespawnComplete,false);
            
        }
        
        public override void ActivateSymbol()
        {
            Debug.Log("ACTIVATE SYMBOL + " + gameObject.name);
            var symbolData = GetSymbolData();
            symbolData.PlayerMaterial.DOFloat(symbolData.EmissionActiveValue, symbolData.EmissionExposureWeightKey, 1f).SetDelay(1.5f);
        }

        public override void DeactivateSymbol()
        {
            Debug.Log("DEACTIVATE SYMBOL + " + gameObject.name);
            var symbolData = GetSymbolData();
            symbolData.PlayerMaterial.SetFloat(symbolData.EmissionExposureWeightKey,symbolData.EmissionInActiveValue);
        }
        

        #endregion

        #region Custom Methods

        private void GetRenderers()
        {
            _renderer01 = transform.GetChild(0).GetComponent<Renderer>();
            _renderer02 = transform.GetChild(1).GetComponent<Renderer>();
        }

        private void SetMaterials()
        {
            _renderer01.material = GetSymbolData().PlayerMaterial;
            _renderer02.material = GetSymbolData().PlayerMaterial;

        }
        
        
        public GameEnums.SymbolID GetSymbolId()
        {
            return GetSymbolData().SymbolId;
        }

        private void SetSymbolVisibility(bool status)
        {
            if (status)
            {
                _renderer01.gameObject.SetActive(true);
                _renderer02.gameObject.SetActive(true);
            }
            else
            {
                _renderer01.gameObject.SetActive(false);
                _renderer02.gameObject.SetActive(false);
            }
        }
        
        
        #endregion
        

        #region Event Methods

        private void OnLevelStart()
        {
            SetMaterials();
            SetSymbolVisibility(false);
            DOVirtual.DelayedCall(3f, (delegate
            {
                SetSymbolVisibility(true);
                AlphaIn();
            }));

        }
        private void OnLevelComplete()
        {
            SetSymbolVisibility(false);
        }

        private void OnRespawnStart()
        {
            SetSymbolVisibility(false);
        }


        private void OnRespawnComplete()
        {
            DOVirtual.DelayedCall(2.5f ,()=>SetSymbolVisibility(true));
            //SetSymbolVisibility(true);

        }

        #endregion


    }
}