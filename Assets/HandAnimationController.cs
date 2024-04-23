using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandAnimationController : MonoBehaviour
{
    [SerializeField] private InputActionProperty triggerAction;
    [SerializeField] private InputActionProperty gripAction;
    private Animator amaintionComp;

    private void Start() {
        amaintionComp = GetComponent<Animator>();
    }

    private void Update() {
        float triggerValue = triggerAction.action.ReadValue<float>();
        float gripValue = gripAction.action.ReadValue<float>();

        amaintionComp.SetFloat("Trigger", triggerValue);
        amaintionComp.SetFloat("Grip", gripValue);
    }

}
