using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FordwardMov : MonoBehaviour
{
    private void Update() {
        gameObject.transform.position += new Vector3(0.1f, 0, 0);
    }
}
