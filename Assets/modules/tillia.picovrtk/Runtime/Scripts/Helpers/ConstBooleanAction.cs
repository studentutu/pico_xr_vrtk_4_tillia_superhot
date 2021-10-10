using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zinnia.Action;
using Zinnia.Process;

namespace Tilia.PicoIntegration
{
    public class ConstBooleanAction : BooleanAction, IProcessable
    {
        [SerializeField] private bool _valueToBeUsed;

        public void SetNewValue(bool val)
        {
            _valueToBeUsed = val;
        }

        public void Process()
        {
            Receive(_valueToBeUsed);
        }
    }
}