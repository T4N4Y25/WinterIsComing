using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Boss : MonoBehaviour
{
    [SerializeField] int Health = 100;
    [SerializeField] float DetectDistance = 6.0f;
    private float current;
    private bool isStopped = false;
    NavMeshAgent agent;
    public GameObject player;
    [SerializeField] Transform thronepos;
    public Animator anim;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Health -= 10;
            agent.velocity = Vector3.zero;
            agent.isStopped = false;
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(StopChase());
        }
    }

    private IEnumerator StopChase()
    {
        isStopped = true;
        agent.isStopped = true; 
        yield return new WaitForSeconds(2); 
        agent.isStopped = false; 
        isStopped = false;
    }

    void Detect()
    {
        if (isStopped) return; // Prevent Detect if boss is stopped

        current = Vector3.Distance(player.transform.position, this.transform.position);

        if (current < DetectDistance)
        {
            Chase();
        }
        else
        {
            agent.SetDestination(thronepos.position);
        }
    }

    void Chase()
    {
        agent.SetDestination(player.transform.position);
    }

    void Defeated()
    {
        if(Health <= 0)
        {
            Destroy(this.gameObject);
            Debug.Log("Boss defeated");
        }
    }

    void Update()
    {
        Detect();
        Debug.Log(Health);
        Defeated();

        if(agent.velocity.magnitude <= 0.1f) 
        {
            anim.SetBool("IsWalking",false);
            anim.SetBool("IsChasing", false);
        }
        else
        {
            anim.SetBool("IsWalking",true);
        }
    }
}
