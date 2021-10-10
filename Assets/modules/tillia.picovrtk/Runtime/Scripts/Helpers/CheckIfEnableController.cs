using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.PXR;
using UnityEngine;
using UnityEngine.Events;
using Zinnia.Process;

namespace Softserve.Tilia.PicoIntegration
{
    public class CheckIfEnableController : MonoBehaviour, IProcessable
    {
        [SerializeField] private PXR_ControllerLoader _loader;
        [SerializeField] private UnityEvent _foundModel;
        [SerializeField] private UnityEvent _loseModel;
        [SerializeField] private int _framesToWait = 2;

        private Coroutine runningOne = null;

        public void Process()
        {
            if (runningOne != null)
            {
                StopCoroutine(runningOne);
            }

            runningOne = StartCoroutine(WaitForFrame());
        }

        private IEnumerator WaitForFrame()
        {
            var waitForFrames = _framesToWait;
            while (waitForFrames > 0)
            {
                yield return null;
                waitForFrames--;
            }

            Debug.Log(transform.name + " found " + (_loader.gameObject.transform.childCount == 0));
            if (_loader.enabled && _loader.gameObject.transform.childCount == 0)
            {
                _loseModel?.Invoke();
            }
            else
            {
                _foundModel?.Invoke();
            }

            runningOne = null;
        }
    }
}