using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class MoveTo : MonoBehaviour
{
    // public variables
    public Transform goal;

    public Animator anim;

    // private variables are private by default
    NavMeshAgent agent;
    
    // Start is called before the first frame update
    void Start()
    {
       agent = this.GetComponent<NavMeshAgent>(); 
      
       if(goal == null) {
           goal = GameObject.Find("FPSController").transform;
       }

       //anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // go to the goal every frame
        // if statement gets distance of AI from player and stops AI from,
        // getting closer than 4 units away from player
       if(Vector3.Distance(goal.position, this.transform.position) > 1 && Vector3.Distance(goal.position, this.transform.position) < 20) 
        {
            agent.destination = goal.position;  
            anim.SetBool("Attacking", true);
        } 
        else 
        {
            agent.destination = this.transform.position;
            anim.SetBool("Attacking", false);
        }
       
    }
}