using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraTurn : MonoBehaviour
{
    [SerializeField]
    private InputActionReference joystick;

    private Vector2 joystickVal;

    public GameObject camera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        joystickVal = joystick.action.ReadValue<Vector2>();

        camera.transform.Rotate(0, joystickVal.x * 5f, 0);

        /*Debug.Log(joystickVal.ToString());*/
    }
}
