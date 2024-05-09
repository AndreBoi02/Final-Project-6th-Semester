using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BA : MonoBehaviour {

    [Header("")]
    public Vector3 m_currentVel;
    public float m_maxVel;
    public float m_maxForce;
    public float m_maxSpeed;
    public float m_slowingFactor;
    public float m_proximity;
    public BA aTarget;
    public Vector3 m_targetPos;

    public Rigidbody rb;

    public Vector3 dir;
    public Transform m_targetPosT;

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

    void Start() {
        m_targetPos = m_targetPosT.position;
    }


    void FixedUpdate() {
        m_targetPos = m_targetPosT.position;
        Move();
    }

    void Move() {
        switch (type) {
            case typeOfBehaviours.Seek:
                ST.seek(this, m_targetPos);
                rb.velocity = m_currentVel;
                break;

            case typeOfBehaviours.Flee:

                break;
            case typeOfBehaviours.Pursuit:

                break;
            case typeOfBehaviours.Evade:

                break;
            case typeOfBehaviours.Wander:

                break;
            case typeOfBehaviours.FollowPath:

                break;
            case typeOfBehaviours.none:

                break;
        }
    }

    
}
