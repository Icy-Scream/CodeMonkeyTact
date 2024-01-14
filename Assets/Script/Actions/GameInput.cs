using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    private ActionMaps actionMaps;
    public event EventHandler OnRightClickPerformed;

    private void Awake() {
        actionMaps = new ActionMaps();
    }

    private void OnEnable() {
        actionMaps.Player.Enable();
        actionMaps.Player.MouseRightClick.performed += MouseRightClick_performed;
    }

    private void MouseRightClick_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnRightClickPerformed?.Invoke(this,EventArgs.Empty);
    }
}
