using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Zinnia.Action;
using Zinnia.Process;


namespace Softserve.Tilia.PicoIntegration
{
    public enum XRUsages1DAxis
    {
        triggerButton,
        trigger,
        gripButton,
        grip,
        primaryButton,
        primaryTouch,
        secondaryButton,
        secondaryTouch,
        menuButton,
        primary2DAxisClick,
        primary2DAxisTouch,
        secondary2DAxisClick,
        secondary2DAxisTouch
    }

    public class UnityXRInputAxis1D : FloatAction, IProcessable
    {
        [SerializeField] private XRUsages1DAxis _xrUsages;

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

            float result = 0;
            switch (_xrUsages)
            {
                case XRUsages1DAxis.primaryButton:
                    InputDevice.Value.TryGetFeatureValue(CommonUsages.primaryButton, out var primaryButton);
                    result = primaryButton ? 1 : 0;
                    break;
                case XRUsages1DAxis.primaryTouch:
                    InputDevice.Value.TryGetFeatureValue(CommonUsages.primaryTouch, out var primaryTouch);
                    result = primaryTouch ? 1 : 0;
                    break;
                case XRUsages1DAxis.secondaryButton:
                    InputDevice.Value.TryGetFeatureValue(CommonUsages.secondaryButton, out var secondaryButton);
                    result = secondaryButton ? 1 : 0;
                    break;
                case XRUsages1DAxis.secondaryTouch:
                    InputDevice.Value.TryGetFeatureValue(CommonUsages.secondaryTouch, out var secondaryTouch);
                    result = secondaryTouch ? 1 : 0;
                    break;
                case XRUsages1DAxis.grip:
                    InputDevice.Value.TryGetFeatureValue(CommonUsages.grip, out var grip);
                    result = grip;
                    break;
                case XRUsages1DAxis.gripButton:
                    InputDevice.Value.TryGetFeatureValue(CommonUsages.gripButton, out var gripBtn);
                    result = gripBtn ? 1 : 0;
                    break;
                case XRUsages1DAxis.trigger:
                    InputDevice.Value.TryGetFeatureValue(CommonUsages.trigger, out var trigger);
                    result = trigger;
                    break;
                case XRUsages1DAxis.triggerButton:
                    InputDevice.Value.TryGetFeatureValue(CommonUsages.triggerButton, out var triggerBtn);
                    result = triggerBtn ? 1 : 0;
                    break;
                case XRUsages1DAxis.menuButton:
                    InputDevice.Value.TryGetFeatureValue(CommonUsages.menuButton, out var menuButtonPress);
                    result = menuButtonPress ? 1 : 0;
                    break;
                case XRUsages1DAxis.primary2DAxisClick:
                    InputDevice.Value.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out var primary2DAxisClick);
                    result = primary2DAxisClick ? 1 : 0;
                    break;
                case XRUsages1DAxis.primary2DAxisTouch:
                    InputDevice.Value.TryGetFeatureValue(CommonUsages.primary2DAxisTouch, out var primary2DAxisTouch);
                    result = primary2DAxisTouch ? 1 : 0;
                    break;
                case XRUsages1DAxis.secondary2DAxisClick:
                    InputDevice.Value.TryGetFeatureValue(CommonUsages.secondary2DAxisClick,
                        out var secondary2DAxisClick);
                    result = secondary2DAxisClick ? 1 : 0;
                    break;
                case XRUsages1DAxis.secondary2DAxisTouch:
                    InputDevice.Value.TryGetFeatureValue(CommonUsages.secondary2DAxisTouch,
                        out var secondary2DAxisTouch);
                    result = secondary2DAxisTouch ? 1 : 0;
                    break;
            }

            Receive(result);
        }
    }
}