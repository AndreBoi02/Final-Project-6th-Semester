using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class ST 
{
    public static void seek(BA t_agent, Vector3 t_target) {
        Vector3 desiredVel = 
            //t_agent.aTarget.gameObject.transform.position - t_agent.gameObject.transform.position;
            t_target - t_agent.gameObject.transform.position;
        float distance =
            //distanceV(t_agent.gameObject.transform.position, t_agent.aTarget.gameObject.transform.position);
            distanceV(t_agent.gameObject.transform.position, t_target);
        baseBehaviour(desiredVel, t_agent);
        arrive(t_agent, distance);
    }

    public static void seek(Vector3 target) {
        
    }

    public static void flee(BasicAgent t_agent) {
        
    }

    static void arrive(BA t_agent, float t_distance) {
        if (t_distance <= t_agent.m_slowingFactor) {
            float slowing = t_distance / t_agent.m_slowingFactor;
            t_agent.m_currentVel *= slowing;
        }
    }

    public static void pursuit() {
        
    }

    public static void evade() {
        
    }

    public static void wander() {
       
    }

    static int currentNode = 0;
    public static void followingPath() {
        
    }

    static void baseBehaviour(Vector3 t_desiredVel, BA t_agent) {
        t_desiredVel = t_desiredVel.normalized;
        t_desiredVel *= t_agent.m_maxVel;
        Vector3 steering = t_desiredVel - t_agent.m_currentVel;
        steering = truncateVec(steering, t_agent.m_maxForce);
        steering /= t_agent.rb.mass;
        t_agent.m_currentVel =
        truncateVec(t_agent.m_currentVel + steering, t_agent.m_maxSpeed);
    }

    #region Necesary Functions
    static Vector3 truncateVec(Vector3 t_v, float t_limit)
    {
        Vector3 res = t_v;
        if (res.magnitude <= t_limit)
        {
            return res;
        }
        res = res.normalized;
        res *= t_limit;
        return res;
    }

    static void truncateVec(Vector3 t_v, float t_limit, BA t_agent)
    {
        Vector3 res = t_v;
        if (res.magnitude <= t_limit)
        {
            t_agent.rb.velocity = res;
        }
        res = res.normalized;
        t_agent.rb.velocity = res * t_limit;
    }

    static float distanceV(Vector3 t_this, Vector3 t_other)
    {
        return Mathf.Sqrt(Mathf.Pow((t_this.x - t_other.x), 2) + Mathf.Pow((t_this.y - t_other.y), 2) + Mathf.Pow((t_this.z - t_other.z), 2));
    }

    static Vector3 randomVector()
    {
        Vector3 ret = new Vector3(Random.Range(-1, 1.1f), .07f, Random.Range(-1, 1.1f));
        return ret;
    }
    #endregion
}
