using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Zinnia.Action;
using Zinnia.Process;


namespace Softserve.Tilia.PicoIntegration
{
    public enum XRUsages2DAxis
    {
        primary2DAxis,
        secondary2DAxis
    }

    public class UnityXRInputAxis2D : Vector2Action, IProcessable
    {
        [SerializeField] private XRUsages2DAxis _xrUsages;

        [SerializeField] private XRControllers _xrControllers;

        private InputDevice? _inputDevice = null;

        protected InputDevice? InputDevice
        {
            get
            {
                if (_inputDevice == null)
                {
                    var ControllerChara = InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Controller;
                    if (_xrControllers == XRControllers.leftController)
                    {
                        ControllerChara |= InputDeviceCharacteristics.Left;
                    }
                    else
                    {
                        ControllerChara |= InputDeviceCharacteristics.Right;
                    }

                    _inputDevice = XRControllerWatcher.GetDevice(ControllerChara);
                }

                return _inputDevice;
            }
        }


        public void Process()
        {
            if (InputDevice == null)
            {
                return;
            }

            var result = Vector2.zero;
            switch (_xrUsages)
            {
                case XRUsages2DAxis.primary2DAxis:
                    InputDevice.Value.TryGetFeatureValue(CommonUsages.primary2DAxis, out var primary2DAxis);
                    result = primary2DAxis;
                    break;
                case XRUsages2DAxis.secondary2DAxis:
                    InputDevice.Value.TryGetFeatureValue(CommonUsages.secondary2DAxis, out var secondary2DAxis);
                    result = secondary2DAxis;
                    break;
            }

            Receive(result);
        }
    }
}