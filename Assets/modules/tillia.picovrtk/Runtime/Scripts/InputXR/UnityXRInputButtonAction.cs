using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Zinnia.Action;
using UnityEngine.UI;
using Zinnia.Process;


namespace Softserve.Tilia.PicoIntegration
{
    public enum XRUsagesButtonAction
    {
        primaryButton,
        primaryTouch,
        secondaryButton,
        secondaryTouch,
        gripButton,
        triggerButton,
        menuButton,
        primary2DAxisClick,
        primary2DAxisTouch,
        secondary2DAxisClick,
        secondary2DAxisTouch
    }

    public class UnityXRInputButtonAction : BooleanAction, IProcessable
    {
        [SerializeField] private XRUsagesButtonAction _xrUsages;

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

            bool result = false;
            switch (_xrUsages)
            {
                case XRUsagesButtonAction.primaryButton:
                    InputDevice.Value.TryGetFeatureValue(CommonUsages.primaryButton, out var primaryButton);
                    result = primaryButton;
                    break;
                case XRUsagesButtonAction.primaryTouch:
                    InputDevice.Value.TryGetFeatureValue(CommonUsages.primaryTouch, out var primaryTouch);
                    result = primaryTouch;
                    break;
                case XRUsagesButtonAction.secondaryButton:
                    InputDevice.Value.TryGetFeatureValue(CommonUsages.secondaryButton, out var secondaryButton);
                    result = secondaryButton;
                    break;
                case XRUsagesButtonAction.secondaryTouch:
                    InputDevice.Value.TryGetFeatureValue(CommonUsages.secondaryTouch, out var secondaryTouch);
                    result = secondaryTouch;
                    break;
                case XRUsagesButtonAction.gripButton:
                    InputDevice.Value.TryGetFeatureValue(CommonUsages.gripButton, out var gripBtn);
                    result = gripBtn;
                    break;
                case XRUsagesButtonAction.triggerButton:
                    InputDevice.Value.TryGetFeatureValue(CommonUsages.triggerButton, out var triggerBtn);
                    result = triggerBtn;
                    break;
                case XRUsagesButtonAction.menuButton:
                    InputDevice.Value.TryGetFeatureValue(CommonUsages.menuButton, out var menuButtonPress);
                    result = menuButtonPress;
                    break;
                case XRUsagesButtonAction.primary2DAxisClick:
                    InputDevice.Value.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out var primary2DAxisClick);
                    result = primary2DAxisClick;
                    break;
                case XRUsagesButtonAction.primary2DAxisTouch:
                    InputDevice.Value.TryGetFeatureValue(CommonUsages.primary2DAxisTouch, out var primary2DAxisTouch);
                    result = primary2DAxisTouch;
                    break;
                case XRUsagesButtonAction.secondary2DAxisClick:
                    InputDevice.Value.TryGetFeatureValue(CommonUsages.secondary2DAxisClick,
                        out var secondary2DAxisClick);
                    result = secondary2DAxisClick;
                    break;
                case XRUsagesButtonAction.secondary2DAxisTouch:
                    InputDevice.Value.TryGetFeatureValue(CommonUsages.secondary2DAxisTouch,
                        out var secondary2DAxisTouch);
                    result = secondary2DAxisTouch;
                    break;
            }

            Receive(result);
        }
    }
}