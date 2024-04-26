using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class GripController : MonoBehaviour
{
    private Rigidbody prevHit;
    private Rigidbody currHit;

    [SerializeField] 
    private LineRenderer lineRenderer;

    [SerializeField]
    private GameObject controller;

    [SerializeField]
    private GameObject otherController;

    [SerializeField]
    private InputActionReference grip;

    [SerializeField]
    private InputActionReference secondGrip;

    private float gripVal;
    private float secondGripVal;   
    float initialRotationAngle;
    private Vector3 hitPosition;
    private LayerMask layerMask;

    private float forwardScalar;
    private bool justPressedGrip = false;
    private Rigidbody heldObject;

    private bool justPressedSecondGrip = false;
    private float initialMagnitude = -1f;

    private void Awake() {
        grip.action.Enable();
    }

    private void OnDisable() {
        grip.action.Disable();
    }

    public void InitialRotationAngle(InputAction.CallbackContext context){
        initialRotationAngle = controller.transform.rotation.eulerAngles.y;
    }
    private void FixedUpdate()
    {
        gripVal = grip.action.ReadValue<float>();
        secondGripVal = secondGrip.action.ReadValue<float>();

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

                if (gripVal > 0.1f)
                {
                    if (!justPressedGrip)
                    {
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

        if (gripVal > 0.1f)
        {
            if (!justPressedGrip)
            {
                justPressedGrip = true;
            }
            lineRenderer.startColor = Color.blue;
            if (heldObject != null)
            {
                heldObject.transform.position = controller.transform.position + controller.transform.forward * forwardScalar;
                heldObject.transform.rotation = controller.transform.rotation;
            }
            if (secondGripVal > 0.1f)
            {
                if (!justPressedSecondGrip)
                {
                    justPressedSecondGrip = true;
                    initialMagnitude = (controller.transform.position - otherController.transform.position).magnitude;
                }
                if (initialMagnitude != -1f)
                {
                    float currentMagnitude = (controller.transform.position - otherController.transform.position).magnitude;
                    float currentScale = currentMagnitude / initialMagnitude;
                    heldObject.transform.localScale = new Vector3(currentScale, currentScale, currentScale);
                }
            }
            
        }
        else
        {
            if (heldObject != null)
            {
                heldObject.useGravity = true;
            }
            justPressedGrip = false;
            heldObject = null;
        }
        if (!(secondGripVal > 0.1f))
        {
            initialMagnitude = -1f;
            justPressedSecondGrip = false;
        }
    }
}
