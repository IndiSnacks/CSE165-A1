using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Spawn : MonoBehaviour
{
    public InputActionReference button;
    private float buttonVal;
    private bool justPressed;

    public GameObject spawnPoint;

    public GameObject prefab;
    private GameObject clone;

    public void SwapPrefab(GameObject newPrefab)
    {
        prefab = newPrefab;
    }

    // Start is called before the first frame update
    private void Awake()
    {
        justPressed = false;
        button.action.Enable();
    }

    private void OnDestroy()
    {
        button.action.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        buttonVal = button.action.ReadValue<float>();
        if (buttonVal > 0.1f)
        {
            if (!justPressed)
            {
                justPressed = true;
                // Debug.Log("A button was just pressed.");
                clone = Instantiate(prefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
            }

            clone.transform.position = spawnPoint.transform.position;
            clone.transform.rotation = spawnPoint.transform.rotation;
        }
        else
        {
            if (justPressed)
            {
                clone.AddComponent<Rigidbody>();
                clone = null;
                justPressed = false;
            }
            // Debug.Log("A button is NOT pressed.");
        }
    }
}