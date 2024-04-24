using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class SelectionController : MonoBehaviour
{
    [SerializeField]
    public InputActionReference trigger;

    [SerializeField]
    private XRInteractorLineVisual line;

    private float triggerValue;

    private void Awake() {
        trigger.action.Enable();
    }

    private void OnDestroy() {
        trigger.action.Disable();
    }

    private void ColorChnage(){
        // line.setLineColorGradient(Color.green);
    }

    private void Update() {
        triggerValue = trigger.action.ReadValue<float>();
        if (triggerValue > 0.1f) {
            Gradient greenGradient = new Gradient();
            GradientColorKey[] colorKeys = new GradientColorKey[2];
            colorKeys[0].color = Color.green;
            colorKeys[0].time = 0f;
            colorKeys[1].color = Color.green;
            colorKeys[1].time = 1f;
            GradientAlphaKey[] alphaKeys = new GradientAlphaKey[2];
            alphaKeys[0].alpha = 1f;
            alphaKeys[0].time = 0f;
            alphaKeys[1].alpha = 1f;
            alphaKeys[1].time = 1f;
            greenGradient.SetKeys(colorKeys, alphaKeys);
            line.validColorGradient = greenGradient;
            line.invalidColorGradient = greenGradient;
        } else {
            Gradient redGradient = new Gradient();
            GradientColorKey[] colorKeys = new GradientColorKey[2];
            colorKeys[0].color = Color.red;
            colorKeys[0].time = 0f;
            colorKeys[1].color = Color.red;
            colorKeys[1].time = 1f;
            GradientAlphaKey[] alphaKeys = new GradientAlphaKey[2];
            alphaKeys[0].alpha = 1f;
            alphaKeys[0].time = 0f;
            alphaKeys[1].alpha = 1f;
            alphaKeys[1].time = 1f;
            redGradient.SetKeys(colorKeys, alphaKeys);
            line.validColorGradient = redGradient;
            line.invalidColorGradient = redGradient;
        }
    }
}
