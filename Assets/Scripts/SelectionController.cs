using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class SelectionController : MonoBehaviour
{
    private GameObject prevHit;
    private GameObject currHit;

    [SerializeField] 
    private LineRenderer lineRenderer;

    [SerializeField]
    private GameObject controller;

    [SerializeField]
    private InputActionReference trigger;

   [SerializeField]
    private InputActionReference grip;

    private float triggerVal;
    private float gripVal;
    float initialRotationAngle;
    private Vector3 hitPosition;
    private LayerMask layerMask;
    
    private float forwardScale = 0.1f;
    private float distToHit;
    private bool justPressedTrigger = false;
    private GameObject heldObject;

    private void Awake() {
        trigger.action.Enable();
        grip.action.Enable();
        grip.action.performed += InitialRotationAngle;
    }

    private void OnDisable() {
        trigger.action.Disable();
        grip.action.Disable();
        grip.action.performed -= InitialRotationAngle;
    }

    public void InitialRotationAngle(InputAction.CallbackContext context){
        initialRotationAngle = controller.transform.rotation.eulerAngles.y;
    }
    private void FixedUpdate()
    {
        triggerVal = trigger.action.ReadValue<float>();
        gripVal = grip.action.ReadValue<float>();

        var ray = new Ray(controller.transform.position, controller.transform.forward);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 100)){
            lineRenderer.SetPosition(0, controller.transform.position);
            lineRenderer.SetPosition(1, hit.point);            

            if(hit.collider.gameObject.tag.CompareTo("Interactable") == 0){
                lineRenderer.startColor = Color.green;
                lineRenderer.endColor = Color.green;
                
                prevHit = currHit;
                if (prevHit != null) prevHit.GetComponent<Outline>().enabled = false;
                currHit = hit.collider.gameObject;
                currHit.GetComponent<Outline>().enabled = true;
                

                if(triggerVal > 0.1f){
                    if (!justPressedTrigger)
                    {
                        distToHit = (controller.transform.position - hit.point).magnitude;
                        justPressedTrigger = true;
                        heldObject = currHit;
                    }
                    lineRenderer.startColor = Color.blue;
                    hit.rigidbody.useGravity = false;
                    //hit.collider.gameObject.transform.position = controller.transform.position + controller.transform.forward * distToHit;
                    //hit.collider.gameObject.transform.rotation = controller.transform.rotation;
                    heldObject.transform.position = controller.transform.position + controller.transform.forward * distToHit;
                    heldObject.transform.rotation = controller.transform.rotation;
                }
                else
                {
                    hit.rigidbody.useGravity = true;
                    justPressedTrigger = false;
                    heldObject = null;
                }

                if(gripVal > 0.1f){
                    lineRenderer.startColor = Color.yellow;
                    hit.rigidbody.useGravity = false;
                    Vector3 currScale = hit.collider.gameObject.transform.localScale;
                    float scaleMultiplier = (controller.transform.rotation.eulerAngles.y - initialRotationAngle) / 100000.0f;
                    Vector3 newScale = new Vector3(currScale.x + scaleMultiplier, currScale.y + scaleMultiplier, currScale.z + scaleMultiplier);
                    hit.collider.gameObject.transform.localScale = newScale;
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
            lineRenderer.SetPosition(0, controller.transform.position);
            lineRenderer.SetPosition(1, controller.transform.position + controller.transform.forward * 30f);
        }
    }
}
