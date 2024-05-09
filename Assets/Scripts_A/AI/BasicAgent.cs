using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAgent : MonoBehaviour {
    #region Vars
    [HideInInspector] public Vector3 m_pos;
    [HideInInspector] public Vector3 m_currentVel;
    public float m_maxVel;
    public float m_maxForce;
    public float m_maxSpeed;
    public float m_slowingFactor;
    public float m_proximity;
    [HideInInspector] public BasicAgent aTarget;
    [HideInInspector] public Vector3 m_targetPos;
    [SerializeField] List<Transform> points2Follow = new List<Transform>();
    [HideInInspector] public Rigidbody rb;
    #endregion

    [SerializeField] Vector3 eyesPos;
    [SerializeField] Vector3 earsPos;
    [SerializeField] float eyesRad;
    [SerializeField] float earsRad;
    [SerializeField] string targetNameA;
    [SerializeField] string targetNameV;
    public List<BasicAgent> agentsList = new List<BasicAgent>();

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
        earsPos = transform.position;
        eyesPos = m_currentVel.normalized;
        eyesPos *= eyesRad;
        eyesPos += transform.position;
        Move();
        if (targetNameV == "") {
            return;
        }
        m_targetPos = GameObject.Find(targetNameV).transform.position;
        perceptionManager();
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
                SteeringBehaviours.wander(this, 1f, 1f, m_targetPos);
                break;
            case typeOfBehaviours.FollowPath:
                SteeringBehaviours.followingPath(this, points2Follow, m_proximity);
                break;
            case typeOfBehaviours.none:
                break;
        }
        rb.velocity = m_currentVel;
    }

    void perceptionManager() {
        eyesPerception();
        earsPerception();
    }

    void eyesPerception() {
        Collider[] agentsViewed = Physics.OverlapSphere(eyesPos, eyesRad);
        foreach (Collider agent in agentsViewed) {
            if (agent.CompareTag("Agent") == this)
            {
                continue;
            }
            agentsList.Add(agent.GetComponent<BasicAgent>());
        }
    }

    void earsPerception() {
        Collider[] agentsViewed = Physics.OverlapSphere(earsPos, earsRad);
        foreach (Collider agent in agentsViewed) {
            if (agent.CompareTag("Agent")) {
                //continue;
                agentsList.Add(agent.GetComponent<BasicAgent>());
            }
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(m_targetPos, .5f);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(eyesPos, eyesRad);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(earsPos, earsRad);
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
