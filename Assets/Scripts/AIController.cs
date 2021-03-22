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
    private float fireTime;


    // Start is called before the first frame update
    public override void Start()
    {
        nav = GetComponent<NavMeshAgent>();//assign nav mesh agent
        
        anim = pawn.GetComponent<Animator>();//get the animator from the pawn
        nav.isStopped = false;//start navigating
        base.Start();//run start from controller
    }

    // Update is called once per frame
    public override void Update()
    {
        if (nav.enabled == true)//nav object is not disabled
        {
            if (GameObject.FindGameObjectWithTag("Player") != null)//if there is a player object and navigation is not stopped
            {
                nav.isStopped = false;//resume navigation

                target = GameObject.FindGameObjectWithTag("Player");//set target to player object

                nav.SetDestination(target.transform.position);//tell the nav agent where to go

                Vector3 input = nav.desiredVelocity;//get velocity from nav agen

                pawn.Move(input);//pass info to pawn object

                //attacking 
                float distance = Vector3.Distance(transform.position, target.transform.position);//get distance to player

                if (distance < meleeDistance)
                {
                    //Main attack start
                    pawn.equippedWeapon.isAttacking = true;//tell the system we are attacking
                    pawn.equippedWeapon.MainAttackDown();
                }
                else if (distance > meleeDistance && distance < throwDistance)
                {
                    //alt attack start
                    if (Time.time > fireTime)//check time before next fire
                    {
                        pawn.equippedWeapon.AltAttackDown();
                        pawn.equippedWeapon.AltAttackUp();
                        fireTime = Time.time + 1;//add 1 second
                    }
                }
                else
                {
                    //not attacking
                    pawn.equippedWeapon.isAttacking = false;
                }
            }
            else//no player found
            {
                nav.isStopped = true;//stop navigating
                anim.SetFloat("Forward", 0);//stop moving
                anim.SetFloat("Right", 0);//stop moving
            }
        }
        base.Update();//run update from controller
    }

    private void OnAnimatorMove()//runs after animator decides how to change
    {
        nav.velocity = anim.velocity;//set the nav agent speed to the same as the animator speed so object moves at root motion animate speed
    }

}
