using UnityEngine;
using UnityEngine.InputSystem;

public class OverworldPlayerInputManager : MonoBehaviour
{
    InputAction moveAction;
    InputAction moveCameraAction;
    InputAction dashAction;
    InputAction jumpAction;
    InputAction attackAction;
    InputAction interactAction;
    InputAction openGameMenuAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        moveCameraAction = InputSystem.actions.FindAction("Look");
        dashAction = InputSystem.actions.FindAction("Dash");
        jumpAction = InputSystem.actions.FindAction("Jump");
        attackAction = InputSystem.actions.FindAction("Attack");
        interactAction = InputSystem.actions.FindAction("Interact");
        openGameMenuAction = InputSystem.actions.FindAction("OpenGameMenu");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveValue = moveAction.ReadValue<Vector2>();

        if (jumpAction.WasPressedThisFrame())
        {

        }

        if (jumpAction.IsPressed())
        {

        }
    }
}
