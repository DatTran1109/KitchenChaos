using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnRebindBinding;

    public static GameInput Instance { get; private set; }

    public enum Binding {
        MoveUp,
        MoveDown,
        MoveLeft,
        MoveRight,
        Interact,
        InteractAlt,
        Pause,
    }

    private PlayerInputAction playerInputAction;
    private const string BINDINGS = "Bindings";
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    public event EventHandler OnPauseAction;

    private void Awake() {
        Instance = this;

        playerInputAction = new PlayerInputAction();
        if(PlayerPrefs.HasKey(BINDINGS)) {
            playerInputAction.LoadBindingOverridesFromJson(PlayerPrefs.GetString(BINDINGS));
        }
        playerInputAction.Player.Enable();

        playerInputAction.Player.Interact.performed += Interact_performed;
        playerInputAction.Player.InteractAlternate.performed += InteractAlternate_performed;
        playerInputAction.Player.Pause.performed += Pause_performed;
    }

    private void OnDestroy() {
        playerInputAction.Player.Interact.performed -= Interact_performed;
        playerInputAction.Player.InteractAlternate.performed -= InteractAlternate_performed;
        playerInputAction.Player.Pause.performed -= Pause_performed;

        playerInputAction.Dispose();
    }

    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVector() {
        Vector2 inputVector = playerInputAction.Player.Move.ReadValue<Vector2>();

        // Legacy Input System
        /*if (Input.GetKey(KeyCode.W)) {
            inputVector.y = 1;
        }
        if (Input.GetKey(KeyCode.S)) {
            inputVector.y = -1;
        }
        if (Input.GetKey(KeyCode.D)) {
            inputVector.x = 1;
        }
        if (Input.GetKey(KeyCode.A)) {
            inputVector.x = -1;
        }*/

        return inputVector;
    }

    public string GetBindingText(Binding binding) {
        switch (binding) {
            default:
            case Binding.Interact:
                return playerInputAction.Player.Interact.bindings[0].ToDisplayString();
            case Binding.InteractAlt:
                return playerInputAction.Player.InteractAlternate.bindings[0].ToDisplayString();
            case Binding.Pause:
                return playerInputAction.Player.Pause.bindings[0].ToDisplayString();
            case Binding.MoveUp:
                return playerInputAction.Player.Move.bindings[1].ToDisplayString();
            case Binding.MoveDown:
                return playerInputAction.Player.Move.bindings[2].ToDisplayString();
            case Binding.MoveLeft:
                return playerInputAction.Player.Move.bindings[3].ToDisplayString();
            case Binding.MoveRight:
                return playerInputAction.Player.Move.bindings[4].ToDisplayString();
        }
    }

    public void RebindBinding(Binding binding, Action onActionRebound) {
        playerInputAction.Player.Disable();

        InputAction inputAction;
        int bindingIndex;

        switch (binding) {
            default:
            case Binding.MoveUp:
                inputAction = playerInputAction.Player.Move;
                bindingIndex = 1;
                break;
            case Binding.MoveDown:
                inputAction = playerInputAction.Player.Move;
                bindingIndex = 2;
                break;
            case Binding.MoveLeft:
                inputAction = playerInputAction.Player.Move;
                bindingIndex = 3;
                break;
            case Binding.MoveRight:
                inputAction = playerInputAction.Player.Move;
                bindingIndex = 4;
                break;
            case Binding.Interact:
                inputAction = playerInputAction.Player.Interact;
                bindingIndex = 0;
                break;
            case Binding.InteractAlt:
                inputAction = playerInputAction.Player.InteractAlternate;
                bindingIndex = 0;
                break;
            case Binding.Pause:
                inputAction = playerInputAction.Player.Pause;
                bindingIndex = 0;
                break;
        }

        inputAction.PerformInteractiveRebinding(bindingIndex)
            .OnComplete(callback => {
                callback.Dispose();
                playerInputAction.Player.Enable();
                onActionRebound();

                PlayerPrefs.SetString(BINDINGS, playerInputAction.SaveBindingOverridesAsJson());
                PlayerPrefs.Save();

                OnRebindBinding?.Invoke(this, EventArgs.Empty);
            })
            .Start();
    }
}
