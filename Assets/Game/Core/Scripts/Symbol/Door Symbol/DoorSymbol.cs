using UnityEngine;
using Core.Symbol;
using DG.Tweening;

namespace Core.Door
{
    public class DoorSymbol : SymbolBase
    {
        #region Variables

        private Renderer _renderer;

        #endregion

        #region  Built-In Methods
        
        private new void Awake()
        {
            base.Awake();
            SetMaterial();
        }
        
        
        private new void OnEnable()
        {
            base.OnEnable();
        }

        private new void OnDisable()
        {
            base.OnDisable();
        }

        #endregion
        
        #region  Custom Methods
        
        public override void ActivateSymbol()
        {
            Debug.Log("ACTIVATED SYMBOL + " + gameObject.name);
            var symbolData = GetSymbolData();
            symbolData.DoorMaterial.DOFloat(symbolData.EmissionActiveValue, symbolData.EmissionExposureWeightKey, 1f);
        }

        public override void DeactivateSymbol()
        {
            Debug.Log("DEACTIVATE SYMBOL + " + gameObject.name);
            var symbolData = GetSymbolData();
            symbolData.DoorMaterial.SetFloat(symbolData.EmissionExposureWeightKey,symbolData.EmissionInActiveValue);
        }


        private void SetMaterial()
        {
            _renderer = GetComponent<Renderer>();
            _renderer.material = base.GetSymbolData().DoorMaterial;
        }

        #endregion
        
    }

}
