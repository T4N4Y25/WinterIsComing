using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MazeAI : MonoBehaviour
{   
    NavMeshAgent agent;
    [SerializeField] Transform[] patrolPoints; 
    private int currentPointIndex = 0;
    private float stoppingDistance = 1.0f;
    public GameObject pnlAlert;
    public GameObject player;
    public float detectDistance = 6.0f;
    private float currentDistance;
    // Start is called before the first frame update
    public AudioSource audioSource;
    public AudioSource audiosource;
    private bool hasPlayedAudio = false;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (patrolPoints.Length > 0)
        {
            agent.SetDestination(patrolPoints[currentPointIndex].position);
        }

        pnlAlert.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Patrol();
        Detection();
    }

    void Patrol()
    {
        if (!agent.pathPending && agent.remainingDistance <= stoppingDistance)
        {
            currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
            agent.SetDestination(patrolPoints[currentPointIndex].position);
        }
    }

    void Detection()
    {
        currentDistance = Vector3.Distance(player.transform.position, this.transform.position);

        if (currentDistance < detectDistance)
        {
            pnlAlert.SetActive(true);
            if (audioSource != null && !audioSource.isPlaying)
            {
                audioSource.Play();
                
                
                
            }
            

        }
        else 
        {
            pnlAlert.SetActive(false);

            
            
        }

    }
}
