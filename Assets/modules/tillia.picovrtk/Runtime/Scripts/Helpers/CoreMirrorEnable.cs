using System;
using System.Collections;
using System.Collections.Generic;
using Tilia.CameraRigs.TrackedAlias;
using UnityEngine;
using Zinnia.Tracking.CameraRig;

namespace Tilia.VRTK
{
    [DefaultExecutionOrder(-10000)]
    [DisallowMultipleComponent]
    public class CoreMirrorEnable : MonoBehaviour
    {
        [SerializeField]
        private List<LinkedAliasAssociationCollection> SourceObjects = new List<LinkedAliasAssociationCollection>();

        [SerializeField] private List<GameObject> TargetObjects = new List<GameObject>();
        [SerializeField] private TrackedAliasFacade RigAlias = null;

        private void OnValidate()
        {
            if (SourceObjects.Count != TargetObjects.Count)
            {
                for (int i = 0; i < (SourceObjects.Count - TargetObjects.Count); i++)
                {
                    TargetObjects.Add(null);
                }
            }
        }

        private void Awake()
        {
            RigAlias.TrackedAliasChanged.AddListener(ProcessMirror);
            if (RigAlias.ActiveHeadset != null)
            {
                ProcessMirror(RigAlias.ActiveLinkedAliasAssociation);
            }
        }

        private void OnDestroy()
        {
            RigAlias.TrackedAliasChanged.RemoveListener(ProcessMirror);
        }


        private void ProcessMirror(LinkedAliasAssociationCollection enabledObject)
        {
            var target = TargetObjects[0];
            for (int j = 0; j < SourceObjects.Count; j++)
            {
                if (SourceObjects[j] == enabledObject)
                {
                    target = TargetObjects[j];
                }
                else
                {
                    TargetObjects[j].SetActive(false);
                }
            }

            target.SetActive(true);
            Cursor.visible = false;
            StartCoroutine(WaitForAFrame());
        }

        private IEnumerator WaitForAFrame()
        {
            yield return new WaitForSeconds(0.5f);
            Cursor.lockState = CursorLockMode.Confined;
            yield return new WaitForSeconds(3f);
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}