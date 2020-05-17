using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterMotor : MonoBehaviour
{
    public static CharacterMotor characterMotorInstance;

    [SerializeField]
    private List<NavMeshAgent> Agents;

    private void Awake()
    {
        if (characterMotorInstance != null && characterMotorInstance != this)
        {
            Destroy(this.gameObject);
        }
        characterMotorInstance = this;
    }

    public void AddAgentsToList(GameObject agent)
    {
        Agents.Add(agent.GetComponent<NavMeshAgent>());
    }

    public void RemoveFromAgents(GameObject agent)
    {
        Agents.Remove(agent.GetComponent<NavMeshAgent>());
    }

    public void ClearAgents()
    {
        Agents.Clear();
    }

    public void MoveToPoint(Vector3 point)
    {
        if (Agents == null)
        {
            Debug.Log("Nobody selected");
            return;
        }

        var k = 0;

        for (int i = 0; i < Agents.Count; i++)
        {
            //k = 360 / (i+1);
            //Agents[i].SetDestination(new Vector3(point.x + (Mathf.Sin(k)), point.y, point.z + (Mathf.Cos(k))));
            Agents[i].SetDestination(new Vector3(point.x, point.y, point.z));
        }
    }
}
