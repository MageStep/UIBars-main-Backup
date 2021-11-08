using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol1 : MonoBehaviour
{

    public int health = 100;

    public AudioClip pain;
    AudioSource audioSource;

    public Transform[] points;  // the array of patrol points

    public Transform target;
    public float detectionDistance = 10f;

    private int destPoint = 0;    // the current point to go to
    private NavMeshAgent agent;

    bool waiting = false;
    private bool startAttacking = false;
    private bool stopAttacking = false;

    public enum team{RedTeam, BlueTeam};
    public team currentTeam = team.RedTeam;

    public enum role {Patrolling, Searching, Attacking, Dead};
    public role currentRole = role.Patrolling;

    public Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();

        // keep it from stopping at each patrol point
        agent.autoBraking = true;
        audioSource = GameObject.Find("Handgun_01_FPSController").GetComponent<AudioSource>();

        anim = this.GetComponent<Animator>();
        //anim.SetInteger("Health", health);

        StartCoroutine(GoToNextPoint());
    }

IEnumerator GoToNextPoint() 
{
    // if no points exist
    if(points.Length == 0) {
       yield return null;     // exit this method()
    }
    Debug.Log("Starting Wait");
    waiting = true;
    yield return new WaitForSeconds(2);
    Debug.Log("Ending wait, Going to next position");
    waiting = false;


    // Set the agent to go to the currently selected destination
    agent.destination = points[destPoint].position;

    // choose the next point in the array as the destination
    // cycling to the start if necessary
    destPoint = (destPoint + 1) % points.Length;
}

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("Speed", agent.velocity.magnitude);

        if(currentRole == role.Patrolling)
        {
        // when the AI gets close to the destination,
        // go to the next point
        // ! is the not operator
        if(!agent.pathPending && agent.remainingDistance <0.5f && !waiting) 
            {
                StartCoroutine(GoToNextPoint());

            }
        } else if(currentRole == role.Searching)
        {
            // go to last known position of player.
            // pause and look for player
            // if you see the player, currentRole = role.Attacking
            // if you cannot find the player, currentRole = role.Patrolling
        } else if(currentRole == role.Attacking)
        {
            // pursue player
            // agent.destination = enemy.position;
            // if the enemy is killed. go to patrolling
            // if the enemy is lost, go to searching
        } else if(currentRole == role.Dead)
        {
           stopAttacking = true;
        }
        float distanceFromTarget = Vector3.Distance(target.position, this.transform.position);

    if(distanceFromTarget < detectionDistance && startAttacking == false) {
            Debug.Log("I see the target!");
            startAttacking = true;
            stopAttacking = false;
            
        }
    if(distanceFromTarget > detectionDistance && startAttacking == true) {
            Debug.Log("I lost the target!");
            startAttacking = false;
            stopAttacking = true;
        }
        if(startAttacking) {
            currentRole = role.Attacking;
            agent.destination = target.position;
            anim.SetBool("Attacking", true);
            if(Vector3.Distance(target.position, this.transform.position) >2) {
       agent.destination = target.position;  
        } else {
            agent.destination = this.transform.position;
            }
        }
        if(stopAttacking) {
            currentRole = role.Patrolling;
            anim.SetBool("Attacking", false);
        }

        if(health < 1) 
        {
            StartCoroutine(KillEnemy());
        }
        
    }
   
   IEnumerator KillEnemy()
   {

        anim.enabled = false;
        agent.enabled = false;
        currentRole = role.Dead;
        detectionDistance = .01f;
        yield return new WaitForSeconds(5);
        Destroy(this.gameObject);
   }
   
   
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Bullet"))
        {
            health -= 5;
            audioSource.PlayOneShot(pain, 0.7F);
            //anim.SetInteger("Health", health);
        }
    }
}