using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAgent : MonoBehaviour {

    #region Vars
    public Vector3 m_pos;
    public Vector3 m_currentVel;
    public float m_maxVel;
    public float m_maxForce;
    public float m_maxSpeed;
    public float m_slowingFactor;
    public float m_proximity;
    public BasicAgent aTarget;
    public Vector3 m_targetPos;
    #endregion

    [SerializeField] Vector3 eyesPos;
    [SerializeField] List<Transform> points2Follow = new List<Transform>();
    [SerializeField] float eyesRad;
    [SerializeField] string targetNameA;
    [SerializeField] string targetNameV;
    public Rigidbody rb;

    void Start() {
        rb = gameObject.GetComponent<Rigidbody>();
        m_pos = transform.position;
        if (targetNameA != "") {
            aTarget = GameObject.Find(targetNameA).GetComponent<BasicAgent>();
        }
        if (targetNameV != "") {
            m_targetPos = GameObject.Find(targetNameV).transform.position;
        }
    }

    void FixedUpdate() {
        m_pos = transform.position;
        Move();
        if (targetNameV == "") {
            return;
        }
        m_targetPos = GameObject.Find(targetNameV).transform.position;
        //perceptionManager();
    }

    void Move() {
        switch (type) {
            case typeOfBehaviours.Seek:
                SteeringBehaviours.seek(this);
                break;

            case typeOfBehaviours.Flee:
                SteeringBehaviours.flee(this);
                break;
            case typeOfBehaviours.Pursuit:
                SteeringBehaviours.pursuit(this);
                break;
            case typeOfBehaviours.Evade:
                SteeringBehaviours.evade(this);
                break;
            case typeOfBehaviours.Wander:
                //Needs to be fixed
                SteeringBehaviours.wander(this, 10f, 1f, m_targetPos);
                break;
            case typeOfBehaviours.FollowPath:
                SteeringBehaviours.followingPath(this, points2Follow, m_proximity);
                break;
            case typeOfBehaviours.none:
                //SteeringBehaviours.followingPath(this, points2Follow, m_proximity);
                break;
        }
        //transform.position = m_pos;
        //Vector3 newPos = (m_pos - transform.position).normalized;
        //rb.velocity = newPos*2;
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

    public enum typeOfBehaviours {
        Seek,
        Flee,
        Pursuit,
        Evade,
        Wander,
        FollowPath,
        none
    }

    public typeOfBehaviours type = typeOfBehaviours.Seek;
}
