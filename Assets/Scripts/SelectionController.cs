using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class SelectionController : MonoBehaviour
{
    private Rigidbody prevHit;
    private Rigidbody currHit;

    [SerializeField] 
    private LineRenderer lineRenderer;

    [SerializeField]
    private GameObject controller;

    [SerializeField]
    private InputActionReference trigger;

    private float triggerVal;
    float initialRotationAngle;
    private Vector3 hitPosition;
    private LayerMask layerMask;
    
    private float distToHit;
    private bool justPressedTrigger = false;
    private Rigidbody heldObject;


    private void Awake() {
        trigger.action.Enable();
    }

    private void OnDisable() {
        trigger.action.Disable();
    }

    public void InitialRotationAngle(InputAction.CallbackContext context){
        initialRotationAngle = controller.transform.rotation.eulerAngles.y;
    }
    private void FixedUpdate()
    {
        triggerVal = trigger.action.ReadValue<float>();

        var ray = new Ray(controller.transform.position, controller.transform.forward);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 100)) {
            lineRenderer.SetPosition(0, controller.transform.position);
            lineRenderer.SetPosition(1, hit.point);            

            if(hit.collider.gameObject.tag.CompareTo("Interactable") == 0){
                lineRenderer.startColor = Color.green;
                lineRenderer.endColor = Color.green;
                
                prevHit = currHit;
                if (prevHit != null) prevHit.GetComponent<Outline>().enabled = false;
                currHit = hit.rigidbody;
                currHit.GetComponent<Outline>().enabled = true;

                if (triggerVal > 0.1f)
                {
                    if (!justPressedTrigger)
                    {
                        distToHit = (controller.transform.position - hit.point).magnitude;
                        heldObject = currHit;
                        heldObject.useGravity = false;
                    }
                }
                

            }
            else{
                lineRenderer.startColor = Color.white;
                lineRenderer.endColor = Color.white;
                if (currHit != null)
                {
                    currHit.GetComponent<Outline>().enabled = false;
                }
            }
        }
        else
        {
            lineRenderer.startColor = Color.white;
            lineRenderer.endColor = Color.white;
            lineRenderer.SetPosition(0, controller.transform.position);
            lineRenderer.SetPosition(1, controller.transform.position + controller.transform.forward * 30f);
        }

        if (triggerVal > 0.1f)
        {
            if (!justPressedTrigger)
            {
                justPressedTrigger = true;
            }
            lineRenderer.startColor = Color.blue;
            if (heldObject != null)
            {
                heldObject.transform.position = controller.transform.position + controller.transform.forward * distToHit;
                heldObject.transform.rotation = controller.transform.rotation;
            }
        }
        else
        {
            if (heldObject != null)
            {
                heldObject.useGravity = true;
            }
            justPressedTrigger = false;
            heldObject = null;
        }
    }
}
