using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundries : MonoBehaviour {
    void Update() {
        if (gameObject.transform.position.x >= 5.1) {
            gameObject.transform.position = new Vector3(-4.9f,
                transform.position.y, transform.position.z);
        }
        if (gameObject.transform.position.x <= -5.1) {
            gameObject.transform.position = new Vector3( 4.9f,
                transform.position.y, transform.position.z);
        }
        if (gameObject.transform.position.z >= 5.1) {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x,
                transform.position.y, -4.9f);
        }
        if (gameObject.transform.position.z <= -5.1) {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x,
                transform.position.y, 4.9f);
        }
    }
}
