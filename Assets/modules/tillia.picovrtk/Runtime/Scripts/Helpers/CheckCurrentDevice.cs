using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zinnia.Association;
using Zinnia.Process.Moment;

namespace Tilia.PicoIntegration
{
    /// <summary>
    /// Latest enabled systems should be also counted.
    /// </summary>
    [DefaultExecutionOrder(30000)]
    public class CheckCurrentDevice : MonoBehaviour
    {
        [SerializeField] private GameObjectsAssociationActivator _platformActivator;
        [SerializeField] private PlatformDeviceAssociation _defaultSdk;
        [SerializeField] private MomentProcess _PlatformActivatorProcessor;

        private void OnEnable()
        {
            StartCoroutine(WaitForFewFrames());
        }

        private IEnumerator WaitForFewFrames()
        {
            int frames = 2;
            while (frames > 0)
            {
                frames--;
                yield return null;
            }

            _platformActivator.enabled = true;
            _PlatformActivatorProcessor.enabled = true;
            _platformActivator.Process();
            _platformActivator.Activate();

            if (_platformActivator.CurrentAssociation == null)
            {
                _defaultSdk.gameObject.SetActive(true);
                foreach (GameObject associatedObject in _defaultSdk.GameObjects.NonSubscribableElements)
                {
                    associatedObject.SetActive(true);
                }
            }
        }
    }
}