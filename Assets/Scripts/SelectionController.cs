using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SelectionController : MonoBehaviour
{
    [SerializeField] 
    private LineRenderer lineRenderer;

    [SerializeField]
    private GameObject controller;

    [SerializeField]
    private InputActionReference trigger;

    private float triggerVal;
    private Vector3 hitPosition;
    private LayerMask layerMask;

    private void Update()
    {
        triggerVal = trigger.action.ReadValue<float>();
        if (triggerVal > 0.1f) {
            var ray = new Ray(controller.transform.position, controller.transform.forward);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, Mathf.Infinity)){
                Debug.Log("Hit point: " + hit.transform);
            }
        } 
    }
}
