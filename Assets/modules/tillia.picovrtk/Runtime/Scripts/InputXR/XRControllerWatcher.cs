using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

namespace Tilia.PicoIntegration
{
    public class XRControllerWatcher : MonoBehaviour
    {
        // Debug
        public Text TextLeftController;
        public Text TextRightController;
        public Text TextHeadController;

        // Left
        public bool LeftTriggerButton { get; set; } = false;
        public float LeftTrigger { get; set; } = 0.0f;
        public bool LeftGripButton { get; set; } = false;
        public float LeftGrip { get; set; } = 0.0f;
        public bool LeftPrimaryButton { get; set; } = false;
        public bool LeftPrimaryTouch { get; set; } = false;
        public bool LeftSecondaryButton { get; set; } = false;
        public bool LeftSecondaryTouch { get; set; } = false;
        public Vector2 LeftPrimary2DAxis { get; set; } = new Vector2();
        public bool LeftPrimary2DAxisClick { get; set; } = false;
        public bool LeftPrimary2DAxisTouch { get; set; } = false;
        public bool LeftMenuButtonPress { get; set; } = false;


        // Right
        public bool RightTriggerButton { get; set; } = false;
        public float RightTrigger { get; set; } = 0.0f;
        public bool RightGripButton { get; set; } = false;
        public float RightGrip { get; set; } = 0.0f;
        public bool RightPrimaryButton { get; set; } = false;
        public bool RightPrimaryTouch { get; set; } = false;
        public bool RightSecondaryButton { get; set; } = false;
        public bool RightSecondaryTouch { get; set; } = false;
        public Vector2 RightPrimary2DAxis { get; set; } = new Vector2();
        public bool RightPrimary2DAxisClick { get; set; } = false;
        public bool RightPrimary2DAxisTouch { get; set; } = false;
        public bool RightMenuButtonPress { get; set; } = false;

        // Head
        public bool HeadTriggerButton { get; set; } = false;
        public float HeadTrigger { get; set; } = 0.0f;
        public bool HeadGripButton { get; set; } = false;
        public float HeadGrip { get; set; } = 0.0f;
        public bool HeadPrimaryButton { get; set; } = false;
        public bool HeadPrimaryTouch { get; set; } = false;
        public bool HeadSecondaryButton { get; set; } = false;
        public bool HeadSecondaryTouch { get; set; } = false;
        public Vector2 HeadPrimary2DAxis { get; set; } = new Vector2();
        public bool HeadPrimary2DAxisClick { get; set; } = false;
        public bool HeadPrimary2DAxisTouch { get; set; } = false;
        public bool HeadMenuButtonPress { get; set; } = false;


        private InputDevice? LeftController;
        private InputDevice? RightController;
        private InputDevice? HeadsetController;

        private void Start()
        {
            var ControllerChara = InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Controller;
            LeftController = GetDevice(ControllerChara | InputDeviceCharacteristics.Left);
            RightController = GetDevice(ControllerChara | InputDeviceCharacteristics.Right);
            HeadsetController = GetDevice((InputDeviceCharacteristics.Camera | InputDeviceCharacteristics.Controller));
        }

        private void Update()
        {
            WatchLeftController();
            WatchRightController();
            CheckHeadSetNodes();
            CheckAllOldInputButtons();
            UpdateDebugText();
        }

        public static InputDevice? GetDevice(InputDeviceCharacteristics chara)
        {
            var Devices = new List<InputDevice>();
            InputDevices.GetDevicesWithCharacteristics(chara, Devices);

            if (Devices.Count == 0)
            {
                return null;
            }

            if (!Devices[0].isValid)
            {
                return null;
            }

            return Devices[0];
        }

        private void WatchLeftController()
        {
            if (LeftController == null)
            {
                return;
            }

            // Trigger
            LeftController.Value.TryGetFeatureValue(CommonUsages.triggerButton, out var triggerButton);
            LeftTriggerButton = triggerButton;


            LeftController.Value.TryGetFeatureValue(CommonUsages.trigger, out var trigger);
            LeftTrigger = trigger;


            // Grip
            LeftController.Value.TryGetFeatureValue(CommonUsages.gripButton, out var gripButton);
            LeftGripButton = gripButton;

            LeftController.Value.TryGetFeatureValue(CommonUsages.grip, out var grip);
            LeftGrip = grip;


            // PrimaryButton
            LeftController.Value.TryGetFeatureValue(CommonUsages.primaryButton, out var primaryButton);
            LeftPrimaryButton = primaryButton;


            LeftController.Value.TryGetFeatureValue(CommonUsages.primaryTouch, out var primaryTouch);
            LeftPrimaryTouch = primaryTouch;

            // SecondaryButton
            LeftController.Value.TryGetFeatureValue(CommonUsages.secondaryButton, out var secondaryButton);
            LeftSecondaryButton = secondaryButton;

            LeftController.Value.TryGetFeatureValue(CommonUsages.secondaryTouch, out var secondaryTouch);
            LeftSecondaryTouch = secondaryTouch;

            // Primary2DAxis
            LeftController.Value.TryGetFeatureValue(CommonUsages.primary2DAxis, out var primary2DAxis);
            LeftPrimary2DAxis = primary2DAxis;

            LeftController.Value.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out var primary2DAxisClick);
            LeftPrimary2DAxisClick = primary2DAxisClick;

            LeftController.Value.TryGetFeatureValue(CommonUsages.primary2DAxisTouch, out var primary2DAxisTouch);
            LeftPrimary2DAxisTouch = primary2DAxisTouch;

            // Home
            LeftController.Value.TryGetFeatureValue(CommonUsages.menuButton, out var menuButtonPress);
            LeftMenuButtonPress = menuButtonPress;
        }

        private void WatchRightController()
        {
            if (RightController == null)
            {
                return;
            }

            // Trigger
            RightController.Value.TryGetFeatureValue(CommonUsages.triggerButton, out var triggerButton);
            RightTriggerButton = triggerButton;

            RightController.Value.TryGetFeatureValue(CommonUsages.trigger, out var trigger);
            RightTrigger = trigger;

            // Grip
            RightController.Value.TryGetFeatureValue(CommonUsages.gripButton, out var gripButton);
            RightGripButton = gripButton;

            RightController.Value.TryGetFeatureValue(CommonUsages.grip, out var grip);
            RightGrip = grip;

            // PrimaryButton
            RightController.Value.TryGetFeatureValue(CommonUsages.primaryButton, out var primaryButton);
            RightPrimaryButton = primaryButton;

            RightController.Value.TryGetFeatureValue(CommonUsages.primaryTouch, out var primaryTouch);
            RightPrimaryTouch = primaryTouch;

            // SecondaryButton
            RightController.Value.TryGetFeatureValue(CommonUsages.secondaryButton, out var secondaryButton);
            RightSecondaryButton = secondaryButton;

            RightController.Value.TryGetFeatureValue(CommonUsages.secondaryTouch, out var secondaryTouch);
            RightSecondaryTouch = secondaryTouch;

            // Primary2DAxis
            RightController.Value.TryGetFeatureValue(CommonUsages.primary2DAxis, out var primary2DAxis);
            RightPrimary2DAxis = primary2DAxis;

            RightController.Value.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out var primary2DAxisClick);
            RightPrimary2DAxisClick = primary2DAxisClick;

            RightController.Value.TryGetFeatureValue(CommonUsages.primary2DAxisTouch, out var primary2DAxisTouch);
            RightPrimary2DAxisTouch = primary2DAxisTouch;

            // Home
            RightController.Value.TryGetFeatureValue(CommonUsages.menuButton, out var menuButtonPress);
            RightMenuButtonPress = menuButtonPress;
        }

        private void UpdateDebugText()
        {
            TextLeftController.text = $"{LeftController?.name}" +
                                      $"\r\nTrigger: {LeftTriggerButton}, {LeftTrigger:F2}" +
                                      $"\r\nGrip: {LeftGripButton}, {LeftGrip:F2}" +
                                      $"\r\nPrimary: {LeftPrimaryButton}, {LeftPrimaryTouch}" +
                                      $"\r\nSecondary: {LeftSecondaryButton}, {LeftSecondaryTouch}" +
                                      $"\r\n2DAxis: {LeftPrimary2DAxis}, {LeftPrimary2DAxisClick}, {LeftPrimary2DAxisTouch}" +
                                      $"\r\nMenu button: {LeftMenuButtonPress}";

            TextRightController.text = $"{RightController?.name}" +
                                       $"\r\nTrigger: {RightTriggerButton}, {RightTrigger:F2}" +
                                       $"\r\nGrip: {RightGripButton}, {RightGrip:F2}" +
                                       $"\r\nPrimary: {RightPrimaryButton}, {RightPrimaryTouch}" +
                                       $"\r\nSecondary: {RightSecondaryButton}, {RightSecondaryTouch}" +
                                       $"\r\n2DAxis: {RightPrimary2DAxis}, {RightPrimary2DAxisClick}, {RightPrimary2DAxisTouch}" +
                                       $"\r\nMenu button: {RightMenuButtonPress}";
            TextHeadController.text = $"{HeadsetController?.name}" +
                                      $"\r\nTrigger: {HeadTriggerButton}, {HeadTrigger:F2}" +
                                      $"\r\nGrip: {HeadGripButton}, {HeadGrip:F2}" +
                                      $"\r\nPrimary: {HeadPrimaryButton}, {HeadPrimaryTouch}" +
                                      $"\r\nSecondary: {HeadSecondaryButton}, {HeadSecondaryTouch}" +
                                      $"\r\n2DAxis: {HeadPrimary2DAxis}, {HeadPrimary2DAxisClick}, {HeadPrimary2DAxisTouch}" +
                                      $"\r\nMenu button: {HeadMenuButtonPress}";
        }

        private void CheckAllOldInputButtons()
        {
            foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(vKey))
                {
                    Debug.LogWarning("Clicked " + vKey);
                }
            }
        }


        private void CheckHeadSetNodes()
        {
            if (HeadsetController == null)
            {
                return;
            }

            // Trigger
            HeadsetController.Value.TryGetFeatureValue(CommonUsages.triggerButton, out var triggerButton);
            HeadTriggerButton = triggerButton;


            HeadsetController.Value.TryGetFeatureValue(CommonUsages.trigger, out var trigger);
            HeadTrigger = trigger;


            // Grip
            HeadsetController.Value.TryGetFeatureValue(CommonUsages.gripButton, out var gripButton);
            HeadGripButton = gripButton;

            HeadsetController.Value.TryGetFeatureValue(CommonUsages.grip, out var grip);
            HeadGrip = grip;


            // PrimaryButton
            HeadsetController.Value.TryGetFeatureValue(CommonUsages.primaryButton, out var primaryButton);
            HeadPrimaryButton = primaryButton;


            HeadsetController.Value.TryGetFeatureValue(CommonUsages.primaryTouch, out var primaryTouch);
            HeadPrimaryTouch = primaryTouch;

            // SecondaryButton
            HeadsetController.Value.TryGetFeatureValue(CommonUsages.secondaryButton, out var secondaryButton);
            HeadSecondaryButton = secondaryButton;

            HeadsetController.Value.TryGetFeatureValue(CommonUsages.secondaryTouch, out var secondaryTouch);
            HeadSecondaryTouch = secondaryTouch;

            // Primary2DAxis
            HeadsetController.Value.TryGetFeatureValue(CommonUsages.primary2DAxis, out var primary2DAxis);
            HeadPrimary2DAxis = primary2DAxis;

            HeadsetController.Value.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out var primary2DAxisClick);
            HeadPrimary2DAxisClick = primary2DAxisClick;

            HeadsetController.Value.TryGetFeatureValue(CommonUsages.primary2DAxisTouch, out var primary2DAxisTouch);
            HeadPrimary2DAxisTouch = primary2DAxisTouch;

            // Home
            HeadsetController.Value.TryGetFeatureValue(CommonUsages.menuButton, out var menuButtonPress);
            HeadMenuButtonPress = menuButtonPress;
        }
    }
}