using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInputs : MonoBehaviour
{

    public event EventHandler OnInteractAction;
    private UserInputs userInputs;

    private void Awake()
    {
        userInputs = new UserInputs();
        userInputs.Player.Enable();

        userInputs.Player.Interact.performed += Interact_performed;
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 PlrInputVectorNormalized()
    {
        Vector2 inputVector = userInputs.Player.Action_Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return inputVector;
    }
}
