using System;
using System.Collections;
using Core.Camera;
using UnityEngine;
using UnityEngine.Events;

namespace Core.Symbol
{
    public abstract class SymbolBase : MonoBehaviour,ISymbol
    {

        public bool Activated { get; set; }
        public UnityAction OnCollected => _onCollected;

        [SerializeField] private SymbolData _symbolData;

        private bool isLerping = false;
        private float symbolAlpha = 0;
        private UnityAction _onCollected;

        #region Built-In Methods
    
        protected void Awake()
        {
            if (_symbolData != null)
            {
                ResetSymbol();
                Debug.Log(this.name + " Initialized");

            }
            else
            {
                gameObject.SetActive(false);
                if(gameObject.activeInHierarchy)
                    Debug.LogError("SYMBOL DATA IS NOT ASSIGNED!");
            }
            

        }

        protected void OnEnable()
        {
            
            Subscribe();
        }
        
       
        protected void OnDisable()
        {
            Unsubscribe();
        }

        #endregion

        #region CustomMethods

        protected void AlphaIn()
        {
            StartCoroutine(LerpAlphaIn());
        }
        
        private void Update()
        {
            if (isLerping)
                LerpAlpha();
        }

        private void LerpAlpha()
        {
            var material = GetSymbolData().PlayerMaterial;
            var matColor = material.color;
            material.color = new Color(matColor.r, matColor.g, matColor.b, symbolAlpha);
        }

        private IEnumerator LerpAlphaIn()
        {
            isLerping = true;
            float time = 0;
            while (time < 1f)
            {
                symbolAlpha = Mathf.Lerp(0, 1, time / 1f);
                time += Time.deltaTime;
                yield return null;
            }
            symbolAlpha = 1;
            isLerping = false;
        }
        private IEnumerator LerpAlphaOut()
        {
            isLerping = true;
            float time = 0;
            while (time < 1f)
            {
                symbolAlpha = Mathf.Lerp(1, 0, time / 1f);
                time += Time.deltaTime;
                yield return null;
            }
            symbolAlpha = 0;
            isLerping = false;

        }

        
        public virtual void ActivateSymbol()
        {
        }

        public virtual void DeactivateSymbol()
        {
           

        }
        
        
        public void OnProcessFinish()
        {
            
        }

        private void ResetSymbol() // Reset ALL symbols intensity vlaues
        {
            _symbolData.PlayerMaterial.SetFloat(_symbolData.EmissionExposureWeightKey,_symbolData.EmissionInActiveValue);
            _symbolData.DoorMaterial.SetFloat(_symbolData.EmissionExposureWeightKey,_symbolData.EmissionInActiveValue);
            _symbolData.PuzzleMaterial.SetFloat(_symbolData.EmissionExposureWeightKey,_symbolData.EmissionInActiveValue);
        }

        public SymbolData SetSymbolData
        {
            set => _symbolData = value;
        }

        public SymbolData GetSymbolData()
        {
            return _symbolData;
        }


        #endregion
        
        #region Subscription Methods
        
        private void Subscribe()
        {
            _onCollected += ActivateSymbol;

        }

        private void Unsubscribe()
        {
            _onCollected -= ActivateSymbol;
        }
        #endregion

        
    }

}
