using UnityEngine;
using UnityEngine.AI;

public class WaypointPatrol : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform player;
    public Transform[] waypoints;

    int m_CurrentWaypointIndex;
    bool m_IsPatrol = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        navMeshAgent.SetDestination(waypoints[0].position);
    }

    // Update is called once per frame
    void Update()
    {
        float dist = CaculateDistacne(player.transform.position, transform.position);
        

        if (dist <= 4.5f)
        {
            m_IsPatrol = false;
            navMeshAgent.SetDestination(player.transform.position);
        }

        else
            navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);

        if (m_IsPatrol && navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
        {
            m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
            navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
        }
    }

    float CaculateDistacne(Vector3 a, Vector3 b)
    {
        return Vector3.Distance(a, b);
    }
}
