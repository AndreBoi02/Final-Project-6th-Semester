using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Menu : MonoBehaviour {
    [SerializeField] GameObject menu;
    [SerializeField] InputActionProperty inputAction;

    private void Start() {
        inputAction.action.performed += turnON;
    }

    private void Update() {
        
    }

    void turnON(InputAction.CallbackContext context) {
        menu.SetActive(!menu.activeSelf);
    }

    void swtichC() {
        
    }
}
