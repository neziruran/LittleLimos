using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    private TMP_Text _fpsText;
    private float _deltaTime;

    private void Awake()
    {
        _fpsText = GetComponent<TextMeshProUGUI>();
    }

    void Update () {
        _deltaTime += (Time.deltaTime - _deltaTime) * 0.1f;
        float fps = 1.0f / _deltaTime;
        _fpsText.text = "FPS: " + Mathf.Ceil (fps).ToString (CultureInfo.InvariantCulture);
    }

}
