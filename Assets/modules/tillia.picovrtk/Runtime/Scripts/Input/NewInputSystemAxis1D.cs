using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zinnia.Action;
using Zinnia.Process;

namespace Softserve.Tilia.PicoIntegration
{
    public class NewInputSystemAxis1D: FloatAction, IProcessable
    {
        [SerializeField] private InputAction _inputAction;
        protected override void OnEnable()
        {
            base.OnEnable();
            _inputAction.Enable();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _inputAction.Disable();
        }

        public void Process()
        {
            Receive(_inputAction.ReadValue<float>());
        }
    }
}