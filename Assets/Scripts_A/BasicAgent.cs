using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAgent : MonoBehaviour {

    #region Vars
    public Vector3 m_pos;
    public Vector3 m_currentVel;
    public float m_maxVel;
    public float m_maxForce;
    public float m_mass;
    public float m_maxSpeed;
    public float m_slowingFactor;
    public float m_proximity;
    public BasicAgent aTarget;
    public Vector3 m_targetPos;
    #endregion

    SteeringBehaviours behaviours;

    [SerializeField] Vector3 eyesPos;
    [SerializeField] List<Transform> points2Follow = new List<Transform>();
    [SerializeField] float eyesRad;
    [SerializeField] string targetNameA;
    [SerializeField] string targetNameV;

    void Start() {
        behaviours = new SteeringBehaviours();
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
                behaviours.seek(this);
                break;

            case typeOfBehaviours.Flee:
                behaviours.flee(this);
                break;
            case typeOfBehaviours.Pursuit:
                behaviours.pursuit(this);
                break;
            case typeOfBehaviours.Evade:
                behaviours.evade(this);
                break;
            case typeOfBehaviours.Wander:
                //Needs to be fixed
                behaviours.wander(this, 100, 500);
                break;
            case typeOfBehaviours.FollowPath:
                behaviours.followingPath(this, points2Follow, m_proximity);
                break;
        }
        transform.position = m_pos;
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
        FollowPath
    }

    public typeOfBehaviours type = typeOfBehaviours.Seek;
}
