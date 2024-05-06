using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TestUI : MonoBehaviour {
    [SerializeField] TMP_Text text;
    int counter = 0;
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            counter++;
            text.text = new string("" + counter);
            other.transform.transform.position = 
                new Vector3(other.transform.position.x, 8, transform.position.z);
        }
    }
}
