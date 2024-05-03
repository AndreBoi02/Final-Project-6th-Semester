using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAgent : MonoBehaviour {

    SteeringBehaviours behaviours;

    [SerializeField] Vector3 eyesPos;
    [SerializeField] float eyesRad;

    // Start is called before the first frame update
    void Start() {
        behaviours = new SteeringBehaviours();
    }

    // Update is called once per frame
    void FixedUpdate() {
        perceptionManager();
    }

    void perceptionManager() {
        eyesPerception();
    }

    void eyesPerception() {
        Collider[] agentsViewed = Physics.OverlapSphere(eyesPos, eyesRad);
        foreach (Collider agent in agentsViewed) {
            if (agent.CompareTag("Agent")) {

            }
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(eyesPos, eyesRad);
    }
}
