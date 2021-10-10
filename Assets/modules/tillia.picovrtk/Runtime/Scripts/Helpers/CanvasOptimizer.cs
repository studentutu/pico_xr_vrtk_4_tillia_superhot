using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Softserve.Tilia.PicoIntegration
{
    /// <summary>
    /// taken from http://sdk.picovr.com/docs/UnitySDK/en/chapter_six.html
    /// </summary>
    public class CanvasOptimizer : MonoBehaviour
    {
        [SerializeField] private CanvasScaler _scaler;

        private static Camera _mainCam;

        protected Camera MainCamera
        {
            get
            {
                if (_mainCam == null)
                {
                    _mainCam = Camera.main;
                }

                return _mainCam;
            }
        }

        private float _initialInverseScale;
        private float _fovPremultiplied;
        private float _eyebuffer = 2048;

        private void OnEnable()
        {
            _initialInverseScale = 1 / _scaler.transform.lossyScale.x;
            _fovPremultiplied = Mathf.Tan(98f / 2) * 2;
        }

        public float GetPPU()
        {
            var distance = MainCamera.transform.position - _scaler.transform.position;
            return _eyebuffer / (distance.magnitude * _fovPremultiplied * _initialInverseScale);
        }

        public void SetPPU(float newValue)
        {
            _scaler.dynamicPixelsPerUnit = newValue;
        }
    }
}