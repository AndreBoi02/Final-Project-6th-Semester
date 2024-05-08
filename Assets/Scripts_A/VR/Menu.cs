using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Turning;

public class Menu : MonoBehaviour {
    [SerializeField] GameObject menu;
    [SerializeField] InputActionProperty inputAction;

    [SerializeField] Scrollbar scrollbar;
    
    [SerializeField] SnapTurnProvider snap;
    [SerializeField] ContinuousTurnProvider continuous;

    private void Start() {
        inputAction.action.performed += turnON;
        snap = gameObject.GetComponent<SnapTurnProvider>();
        continuous = gameObject.GetComponent<ContinuousTurnProvider>();
    }
    
    void turnON(InputAction.CallbackContext context) {
        menu.SetActive(!menu.activeSelf);
    }

    public void swtichC() {
        if (scrollbar.value == 1) {
            continuous.enabled = false;
            snap.enabled = true;
        }
        if (scrollbar.value == 0) {
            continuous.enabled = true;
            snap.enabled = false;
        }
    }
}
