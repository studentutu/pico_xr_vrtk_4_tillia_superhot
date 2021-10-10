using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

namespace Softserve.Tilia.PicoIntegration
{
    public class GetAllXRDevices : MonoBehaviour
    {
        [SerializeField] private Text _currentDevices;

        private Dictionary<string, string> _devices = new Dictionary<string, string>();

        private void OnEnable()
        {
            _devices.Add(XRSettings.loadedDeviceName, XRSettings.loadedDeviceName);
        }

        private void Update()
        {
            InputDevice currentDevice = InputDevices.GetDeviceAtXRNode(XRNode.Head);
            string modelName = "";
            if (currentDevice != null)
            {
                modelName = currentDevice != null && currentDevice.name != null ? currentDevice.name : "";
            }

            if (!_devices.ContainsKey(XRSettings.loadedDeviceName))
            {
                _devices.Add(XRSettings.loadedDeviceName, "");
            }

            _devices[XRSettings.loadedDeviceName] = modelName;
            GetStringFromDevices();
        }


        private void GetStringFromDevices()
        {
            string s = "";
            foreach (var device in _devices.Keys)
            {
                s += $"{device} model {_devices[device]} /n ";
            }

            _currentDevices.text = s;
        }
    }
}