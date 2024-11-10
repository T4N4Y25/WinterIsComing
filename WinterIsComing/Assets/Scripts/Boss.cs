using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

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
    [SerializeField] TextMeshProUGUI tEndGame;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        anim.SetBool("IsWalking", true);
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
        anim.SetBool("IsWalking", false);    
    }

    void Detect()
    {
        if (isStopped) return; 

        current = Vector3.Distance(player.transform.position, this.transform.position);

        if (current < DetectDistance)
        {
            Chase();
            anim.SetBool("IsChasing", true);
        }
        else
        {
            // isStopped = true;
            anim.SetBool("IsChasing", false);
            anim.SetBool("IsWalking", false);
        }
    }

    void Chase()
    {
        agent.SetDestination(player.transform.position);
        anim.SetBool("IsChasing", true);
    }

    void Defeated()
    {
        if(Health <= 0)
        {
            Destroy(this.gameObject);
            Debug.Log("Boss defeated");
            tEndGame.text = "The monster lies defeated, its shadow fading like the last chill of winter. As the weight of darkness lifts, a gentle warmth fills the air, melting away the frost that held you captive. At long last, winter has ended, and with it, your journey to freedom begins. Returing to main menu.";
            StartCoroutine(EndGameSequence());
        }
    }

    private IEnumerator EndGameSequence()
    {
        yield return new WaitForSeconds(8); 
        SceneManager.LoadScene("MainMenu");
    }

    void Update()
    {
        Detect();
        Debug.Log(Health);
        Defeated();
       
    }
}
