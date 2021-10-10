using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zinnia.Process;

namespace Softserve.Tilia.PicoIntegration
{
    public class CopyGameObjectState : MonoBehaviour, IProcessable
    {
        [SerializeField] private UnityEvent _onEnabled;
        [SerializeField] private UnityEvent _onDisabled;

        [SerializeField] private int _delay = 0; 

        private void OnEnable()
        {
            if (_delay == 0)
            {
                EnableEvent();
            }
            else
            {
                StartCoroutine(WaitFrames());
            }
        }

        private IEnumerator WaitFrames()
        {
            var wait = _delay;
            while (wait > 0)
            {
                yield return null;
                wait--;
            }
            EnableEvent();
        }

        private void EnableEvent()
        {
            _onEnabled?.Invoke();
        }

        private void OnDisable()
        {
            _onDisabled?.Invoke();
        }

        public void Process()
        {
            OnEnable();
        }
    }
}