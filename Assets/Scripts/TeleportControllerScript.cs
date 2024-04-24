using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportControllerScript : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
  

    public InputActionReference trigger;
    private XRInteractorLineVisual lineVisual;
    private float triggerVal;

    private void Awake() {
        lineVisual = GetComponent<XRInteractorLineVisual>();
        lineVisual.enabled = false;
        trigger.action.Enable();
    }

    private void OnDestroy() {
        trigger.action.Disable();
    }
    
    private void Update() {
        triggerVal = trigger.action.ReadValue<float>();
        if (triggerVal > 0.1f) {
            lineVisual.enabled = true;
            lineRenderer.enabled = true;
        } else {
            lineVisual.enabled = false;
            lineRenderer.enabled = false;
        }
    }
}
