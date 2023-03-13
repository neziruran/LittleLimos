
using UnityEngine;
using UnityEngine.Serialization;
using Utilities;

namespace Core.Symbol
{
    [CreateAssetMenu(fileName = "Symbol", menuName = "Create New Symbol Data", order = 1)]

    public class SymbolData : ScriptableObject
    {
        #region Variables
        
        [FormerlySerializedAs("baseMaterial")] [SerializeField] private Material puzzleMaterial;
        [SerializeField] private Material doorMaterial;
        [SerializeField] private Material playerMaterial;

        [SerializeField] private GameEnums.SymbolID _symbolId;
        [SerializeField] private int _symbolOrder = 0;
        [SerializeField] private Vector3 _rotationMatchAngle;

        private const string EmissionKey = "_EmissiveExposureWeight";
        private const float _emissionInActiveValue = .9f;
        private const float _emissionActiveValue = 0.5f;

        #endregion

       
        #region Getters & Setters

        public Material PuzzleMaterial => puzzleMaterial;

        public Material PlayerMaterial => playerMaterial;

        public Material DoorMaterial => doorMaterial;
        public Vector3 RotationMatchAngle => _rotationMatchAngle; 
        public string EmissionExposureWeightKey => EmissionKey;
        
        public float EmissionActiveValue=> _emissionActiveValue;

        public float EmissionInActiveValue => _emissionInActiveValue;
        
        public int SymbolOrder => _symbolOrder;
        
        public GameEnums.SymbolID SymbolId => _symbolId;


        #endregion



    }

}