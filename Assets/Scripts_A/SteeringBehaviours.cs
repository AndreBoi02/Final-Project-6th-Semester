using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringBehaviours {
    public void seek(BasicAgent t_agent) {
        Vector3 desiredVel = t_agent.aTarget.m_pos - t_agent.m_pos;
        float distance = distanceV(t_agent.m_pos, t_agent.aTarget.m_pos);
        baseBehaviour(desiredVel, t_agent);
        arrive(t_agent, distance);
        t_agent.m_pos += t_agent.m_currentVel;
    }

    public void seek(BasicAgent t_agent, Vector3 t_targetV) {
        Vector3 desiredVel = t_targetV - t_agent.m_pos;
        float distance = distanceV(t_agent.m_pos, t_targetV);
        baseBehaviour(desiredVel, t_agent);
        arrive(t_agent, distance);
        t_agent.m_pos += t_agent.m_currentVel;
    }

    public void flee(BasicAgent t_agent) {
        Vector3 desiredVel = t_agent.m_pos - t_agent.aTarget.m_pos;
        baseBehaviour(desiredVel, t_agent);
        t_agent.m_pos += t_agent.m_currentVel;
    }

    void arrive(BasicAgent t_agent, float t_distance) {
        if (t_distance <= t_agent.m_slowingFactor) {
            float slowing = t_distance / t_agent.m_slowingFactor;
            t_agent.m_currentVel *= slowing;
        }
    }

    public void pursuit(BasicAgent t_agent) {
        float T = 3;
        Vector3 futurePos = t_agent.aTarget.m_pos + (t_agent.aTarget.m_currentVel * T);
        t_agent.aTarget.m_pos = futurePos;
        seek(t_agent);
    }

    public void evade(BasicAgent t_agent) {
        float T = 3;
        Vector3 futurePos = t_agent.aTarget.m_pos + (t_agent.aTarget.m_currentVel * T);
        t_agent.aTarget.m_pos = futurePos;
        flee(t_agent);
    }

    public void wander(BasicAgent t_agent, float t_disp, float t_radius) {
        Vector3 newWanderPos = t_agent.m_currentVel;
        newWanderPos = newWanderPos.normalized;
        newWanderPos = new Vector3(newWanderPos.x * t_disp, .07f, newWanderPos.z * t_disp);
        Vector3 vRandom = randomVector();
        vRandom = new Vector3(vRandom.x * t_radius, .07f, vRandom.z * t_radius);
        newWanderPos += vRandom;
        newWanderPos += t_agent.m_pos;
        float distance = distanceV(t_agent.m_pos, newWanderPos);
        seek(t_agent, newWanderPos);
        //if (distance <= t_agent.m_proximity) {
            
        //}
        //My new implementation to make it work

    }

    int currentNode = 0;
    public void followingPath(BasicAgent t_agent, List<Transform> t_list, float t_proximity) {
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

    void baseBehaviour(Vector3 t_desiredVel, BasicAgent t_agent) {
        t_desiredVel = t_desiredVel.normalized;
        t_desiredVel *= t_agent.m_maxVel;
        Vector3 steering = t_desiredVel - t_agent.m_currentVel;
        steering = truncateVec(steering, t_agent.m_maxForce);
        steering /= t_agent.m_mass;
        t_agent.m_currentVel = truncateVec(t_agent.m_currentVel + steering, t_agent.m_maxSpeed);
    }

    #region Necesary Functions
    Vector3 truncateVec(Vector3 t_v, float t_limit) {
        //Start of truncate function between a Vector and a limit
        Vector3 res = t_v;
        if (res.magnitude <= t_limit)
        {
            return res;
        }
        res = res.normalized;
        res *= t_limit;
        return res;
        //End of truncate function
    }

    float distanceV(Vector3 t_this, Vector3 t_other) {
        return Mathf.Sqrt(Mathf.Pow((t_this.x - t_other.x), 2) + Mathf.Pow((t_this.y - t_other.y), 2) + Mathf.Pow((t_this.z - t_other.z), 2));
    }

    Vector3 randomVector() {
        Vector3 ret = new Vector3(Random.Range(-1, 1.1f), .07f, Random.Range(-1, 1.1f));
        return ret;
    }
    #endregion
}
