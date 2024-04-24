using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportControllerScript : MonoBehaviour
{
    [SerializeField] 
    private LineRenderer lineRenderer;

    [SerializeField] 
    private XRRayInteractor rayInteractor;

    [SerializeField] 
    private GameObject player;
  

    public InputActionReference trigger;
    private XRInteractorLineVisual lineVisual;
    private float triggerVal;
    private Vector3 hitLocation;
    private Vector3 hitNormal;
    private Vector3 playerPosition;

    private void Awake() {
        playerPosition = player.transform.position;
        rayInteractor = GetComponent<XRRayInteractor>();
        lineVisual = GetComponent<XRInteractorLineVisual>();
        lineVisual.enabled = false;

        trigger.action.Enable();
        trigger.action.canceled += Teleport;
    }

    private void OnDestroy() {
        trigger.action.Disable();
        trigger.action.canceled -= Teleport;
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

    /**
    * On trigger release, teleport the player to the location of the hit object
    */
    private void Teleport(InputAction.CallbackContext context) {
        rayInteractor.TryGetHitInfo(out Vector3 hitlocation, out Vector3 hitnormal, out int positionInLine, out bool isValidTarget);
        Vector3 newPosition = new Vector3(hitlocation.x, player.transform.position.y, hitlocation.z);
        player.transform.position = newPosition;
    }
}
