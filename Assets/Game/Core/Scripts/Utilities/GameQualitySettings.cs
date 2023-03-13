using System;
using System.Collections;
using System.Collections.Generic;
using Core.InputS;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Utilities
{
    public class GameQualitySettings : MonoBehaviour
    {
        public void SetQualityLevel(int qualityLevel)
        {
            QualitySettings.SetQualityLevel(qualityLevel);
        }
        
    }

}
