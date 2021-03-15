using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(GameObject))]
[RequireComponent(typeof(Animator))]

public class AIController : Controller
{
    [SerializeField]
    private float meleeDistance = 3f;
    [SerializeField]
    private float throwDistance = 5f;
    private NavMeshAgent nav;
    private GameObject target;
    private Animator anim;


    // Start is called before the first frame update
    public override void Start()
    {
        nav = GetComponent<NavMeshAgent>();//assign nav mesh agent
        
        anim = pawn.GetComponent<Animator>();//get the animator from the pawn

        base.Start();//run start from controller
    }

    // Update is called once per frame
    public override void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player"))//if there is a player object
        {
            nav.isStopped = false;//start navigating

            target = GameObject.FindGameObjectWithTag("Player");//set target to player object

           //transform.LookAt(target.transform);//tell the enemy to look at the player(because regular nav agent was not doing this properly)
            
            nav.SetDestination(target.transform.position);//tell the nav agent where to go
         
            Vector3 input = nav.desiredVelocity;//get velocity from nav agent

            pawn.Move(input);//pass info to pawn object

            //attacking 
            float distance = Vector3.Distance(transform.position, target.transform.position);//get distance to player
            float firetime = 1;
            if (distance < meleeDistance)
            {
                //Main attack start
                pawn.equippedWeapon.MainAttackDown();
            }
            else if (distance > meleeDistance && distance < throwDistance)
            {
                //alt attack start
                if (Time.time < firetime)//check time before next fire
                {
                    pawn.equippedWeapon.AltAttackDown();
                    pawn.equippedWeapon.AltAttackUp();
                    firetime = Time.time + 1;//add 1 second
                }
                
            }
            else
            {
                //no action
            }
        }
        else//no player found
        {
            nav.isStopped = true;//stop navigating
            anim.SetFloat("Forward", 0);//stop moving
            anim.SetFloat("Right", 0);//stop moving
        }

        base.Update();//run update from controller
    }
    
        
    
    private void OnAnimatorMove()//runs after animator decides how to change
    {
        nav.velocity = anim.velocity;//set the nav agent speed to the same as the animator speed so object moves at root motion animate speed
    }

    
}
