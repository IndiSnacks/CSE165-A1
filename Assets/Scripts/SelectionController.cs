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
        var ray = new Ray(controller.transform.position, controller.transform.forward);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, Mathf.Infinity)){
            hitPosition = hit.point;
            lineRenderer.SetPosition(0, controller.transform.position);
            lineRenderer.SetPosition(1, hitPosition);
        }

        if (triggerVal > 0.1f) {
            lineRenderer.startColor = Color.red;
            lineRenderer.endColor = Color.red;
        } 
    }
}
