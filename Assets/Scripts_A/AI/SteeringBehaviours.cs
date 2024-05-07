using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SteeringBehaviours {
    public static void seek(BasicAgent t_agent) {
        Vector3 desiredVel = t_agent.aTarget.m_pos - t_agent.m_pos;
        float distance = distanceV(t_agent.m_pos, t_agent.aTarget.m_pos);
        baseBehaviour(desiredVel, t_agent);
        arrive(t_agent, distance);
        //t_agent.m_pos += t_agent.m_currentVel;
    }

    public static void seek(BasicAgent t_agent, Vector3 t_targetV) {
        Vector3 desiredVel = t_targetV - t_agent.m_pos;
        float distance = distanceV(t_agent.m_pos, t_targetV);
        baseBehaviour(desiredVel, t_agent);
        arrive(t_agent, distance);
        //t_agent.m_pos += t_agent.m_currentVel;
    }

    public static void flee(BasicAgent t_agent) {
        Vector3 desiredVel = t_agent.m_pos - t_agent.aTarget.m_pos;
        baseBehaviour(desiredVel, t_agent);
        //t_agent.m_pos += t_agent.m_currentVel;
    }

    static void arrive(BasicAgent t_agent, float t_distance) {
        if (t_distance <= t_agent.m_slowingFactor) {
            float slowing = t_distance / t_agent.m_slowingFactor;
            t_agent.m_currentVel *= slowing;
        }
    }

    public static void pursuit(BasicAgent t_agent) {
        float T = 3;
        Vector3 futurePos = t_agent.aTarget.m_pos + (t_agent.aTarget.m_currentVel * T);
        t_agent.aTarget.m_pos = futurePos;
        seek(t_agent);
    }

    public static void evade(BasicAgent t_agent) {
        float T = 3;
        Vector3 futurePos = t_agent.aTarget.m_pos + (t_agent.aTarget.m_currentVel * T);
        t_agent.aTarget.m_pos = futurePos;
        flee(t_agent);
    }

    public static void wander(BasicAgent t_agent, float t_disp, float t_radius, Vector3 targetPos) {
        float distance = distanceV(t_agent.m_pos,targetPos);
        seek(t_agent, targetPos);
        if (distance >= t_agent.m_proximity) {
            return;
        }
        Vector3 newWanderPos = t_agent.m_currentVel;
        newWanderPos = newWanderPos.normalized;
        newWanderPos = new Vector3(newWanderPos.x * t_disp, .07f, newWanderPos.z * t_disp);
        Vector3 vRandom = randomVector();
        vRandom = new Vector3(vRandom.x * t_radius, .07f, vRandom.z * t_radius);
        newWanderPos += vRandom;
        newWanderPos += t_agent.m_pos;
        t_agent.m_targetPos = newWanderPos;
    }

    static int currentNode = 0;
    public static void followingPath(BasicAgent t_agent, List<Transform> t_list, float t_proximity) {
        float distance = distanceV(t_agent.m_pos, t_list[currentNode].position);
        if (distance <= t_proximity) {
            currentNode++;
            if (currentNode >= t_list.Count) {
                currentNode = t_list.Count - 1;
                Debug.Log("Llegamos al último");
            }
        }
        seek(t_agent, t_list[currentNode].position);
    }

    static void baseBehaviour(Vector3 t_desiredVel, BasicAgent t_agent) {
        t_desiredVel = t_desiredVel.normalized;
        t_desiredVel *= t_agent.m_maxVel;
        Vector3 steering = t_desiredVel - t_agent.m_currentVel;
        steering = truncateVec(steering, t_agent.m_maxForce);
        steering /= t_agent.rb.mass;
        truncateVec(t_agent.m_currentVel + steering, t_agent.m_maxSpeed, t_agent);
    }

    #region Necesary Functions
    static Vector3 truncateVec(Vector3 t_v, float t_limit) {
        Vector3 res = t_v;
        if (res.magnitude <= t_limit)
        {
            return res;
        }
        res = res.normalized;
        res *= t_limit;
        return res;
    }

    static void truncateVec(Vector3 t_v, float t_limit, BasicAgent t_agent) {
        Vector3 res = t_v;
        if (res.magnitude <= t_limit) {
            t_agent.rb.velocity = res;
        }
        res = res.normalized;
        t_agent.rb.velocity = res * t_limit;
    }

    static float distanceV(Vector3 t_this, Vector3 t_other) {
        return Mathf.Sqrt(Mathf.Pow((t_this.x - t_other.x), 2) + Mathf.Pow((t_this.y - t_other.y), 2) + Mathf.Pow((t_this.z - t_other.z), 2));
    }

    static Vector3 randomVector() {
        Vector3 ret = new Vector3(Random.Range(-1, 1.1f), .07f, Random.Range(-1, 1.1f));
        return ret;
    }
    #endregion
}
