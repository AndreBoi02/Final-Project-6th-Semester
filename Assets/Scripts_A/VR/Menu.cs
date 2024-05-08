using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Menu : MonoBehaviour {
    [SerializeField] GameObject menu;
    [SerializeField] InputActionProperty inputAction;

    [SerializeField] Scrollbar scrollbar;
    
    [SerializeField] GameObject xr;

    private void Start() {
        inputAction.action.performed += turnON;
    }
    
    void turnON(InputAction.CallbackContext context) {
        menu.SetActive(!menu.activeSelf);
    }

    public void swtichC() {
        if (scrollbar.value == 1) {
            
        }
        if (scrollbar.value == 0) {
            
        }
    }
}
