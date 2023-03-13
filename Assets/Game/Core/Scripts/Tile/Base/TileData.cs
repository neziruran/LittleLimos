using Utilities;
using UnityEngine;

namespace Core.Tile.Data
{
    [CreateAssetMenu(fileName = "Tile", menuName = "Create New Tile Data", order = 1)]

    public class TileData : ScriptableObject
    {
        #region Getters & Setters

        public Material HighLightedMaterial => _highlightedMaterial;
        public Material DefaultMaterial => _defaultMaterial;
        public float HighlightSpeed => _highlightSpeed;
        public float EmissionActiveValue => _emissionActiveValue;
        public string EmissionExposureWeightKey => _emissionExposureWeightKey;
        #endregion

        #region Variables

        private const float _highlightSpeed = 2f;
        private const string _emissionExposureWeightKey = "_EmissiveExposureWeight";
        private const float _emissionActiveValue = .5f;

        [SerializeField] private Material _highlightedMaterial;
        [SerializeField] private Material _defaultMaterial;

        #endregion
        

    }
}