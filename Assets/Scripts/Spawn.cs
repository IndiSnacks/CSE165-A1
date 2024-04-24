using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Spawn : MonoBehaviour
{
    public InputActionReference aButton;
    private float aButtonVal;
    private bool justPressed;

    public GameObject prefab;

    // Start is called before the first frame update
    private void Awake()
    {
        justPressed = false;
        aButton.action.Enable();
    }

    private void OnDestroy()
    {
        aButton.action.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        aButtonVal = aButton.action.ReadValue<float>();
        if (aButtonVal > 0.1f)
        {
            if (!justPressed)
            {
                justPressed = true;
                Debug.Log("A button was just pressed.");
                Instantiate(prefab, transform.position, transform.rotation);
            }
        }
        else
        {
            justPressed = false;
            Debug.Log("A button is NOT pressed.");
        }
    }
}