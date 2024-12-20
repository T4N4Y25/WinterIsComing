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
    public Animator anim;
    private bool hasPlayedAudio = false;

    private Transform PlayerPos;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (patrolPoints.Length > 0)
        {
            agent.SetDestination(patrolPoints[currentPointIndex].position);
        }
        anim = GetComponent<Animator>();
        pnlAlert.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPos = player.transform;
        Patrol();
        Detection();

        if(agent.velocity.magnitude <= 0.1f)
        {
            anim.SetBool("IsWalking", false);
            anim.SetBool("IsChasing", false);
        }
    }

    void Patrol()
    {
        if (!agent.pathPending && agent.remainingDistance <= stoppingDistance)
        {
            currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
            agent.SetDestination(patrolPoints[currentPointIndex].position);
            anim.SetBool("IsWalking", true);
        }
    }

    void Detection()
    {
        currentDistance = Vector3.Distance(player.transform.position, this.transform.position);

        if (currentDistance < detectDistance)
        {
            pnlAlert.SetActive(true);
            audioSource.mute = false;
            if (audioSource != null && !audioSource.isPlaying)
            {
                audioSource.Play();
            }
            Chase();

        }
        else
        {
            pnlAlert.SetActive(false);
            if (audioSource != null && audioSource.isPlaying)
            {
                audioSource.mute = true;
            }
            anim.SetBool("IsChasing", false);
            // EndChase();
            Patrol();

        }

    }

    void Chase()
    {
        Debug.Log("Being chased");
        agent.SetDestination(PlayerPos.position);
        anim.SetBool("IsChasing", true);
    }
   // void EndChase()
    //{
      //  agent.SetDestination(patrolPoints[currentPointIndex].position);
   // }
}
