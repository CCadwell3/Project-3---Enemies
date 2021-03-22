using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Weapons : MonoBehaviour
{

    public bool isAttacking = false;//tracker for weather or not to inflict damage

    [Header("Damage")]
    public float weaponDamage = 100;

    [Header("Projectile Behavior")]
    public Ammo projectilePrefab;
    public float projectileSpeed = 25;
    public Ammo thrown;
    

    [Header("IK Points")]
    public Transform rightHandPoint;
    public Transform leftHandPoint;

    [Header("Events")]
    public UnityEvent OnMainAttackDown;
    public UnityEvent OnMainAttackUp;
    public UnityEvent OnAltAttackDown;
    public UnityEvent OnAltAttackUp;

    [Header("Character")]
    public Pawn pawn;

    [SerializeField]//private everything here after testing
    public GameObject owner;
    public Transform origin;
    

    // Start is called before the first frame update
    public virtual void Start()
    {
        pawn = transform.root.GetComponent<Pawn>();//get the root pawn
        owner = transform.root.gameObject;
        origin = owner.transform.Find("FirePoint");
    }

    // Update is called once per frame
    public virtual void Update()
    {

    }
    public virtual void MainAttackDown()
    {
        isAttacking = true;//attacking
        OnMainAttackDown.Invoke();
    }
    public virtual void MainAttackUp()
    {
        isAttacking = false;//not attacking
        OnMainAttackUp.Invoke();
    }
    public virtual void AltAttackDown()
    {
        OnAltAttackDown.Invoke();
    }
    public virtual void AltAttackUp()
    {
        OnAltAttackUp.Invoke();
    }

    //Longsword
    public virtual void LongSwordAttackStart()
    {
        if (!pawn.anim.GetBool("LongSwordAttack"))//if not attacking
        {
            pawn.anim.SetBool("LongSwordAttack", true);//play attack animation
        }
    }
    public virtual void LongSwordAttackEnd()
    {
        if (pawn.anim.GetBool("LongSwordAttack"))//if attacking
        {
            pawn.anim.SetBool("LongSwordAttack", false);//stop attack animation
        }
        else
        {
            //nothing
        }
    }
    public virtual void LongSwordAltStart()
    {
        //block
        pawn.anim.SetBool("SwordBlock", true);//start block anim
    }
    public virtual void LongSwordAltEnd()
    {
        //return to normal
        if (pawn.anim.GetBool("SwordBlock"))//make sure blocking is happening
        {
            pawn.anim.SetBool("SwordBlock", false);//stop block animation
        }
    }

    //dagger
    public virtual void DaggerAttackStart()
    {
        if (!pawn.anim.GetBool("DaggerAttack"))//if not attacking
        {
            pawn.anim.SetBool("DaggerAttack", true);//play attack animation
        }
    }
    public virtual void DaggerAttackEnd()
    {
        if (pawn.anim.GetBool("DaggerAttack"))//if attacking
        {
            pawn.anim.SetBool("DaggerAttack", false);//stop attack animation
        }
    }
    public virtual void DaggerAltStart()
    {
        //nothing this attack happens on button up
    }
    public virtual void DaggerAltEnd()
    {
        //Throw
        Throw();
    }

    //spear
    public virtual void SpearAttackStart()
    {
        if (!pawn.anim.GetBool("SpearAttack"))//if not attacking
        {
            pawn.anim.SetBool("SpearAttack", true);//play attack animation
        }
    }
    public virtual void SpearAttackEnd()
    {
        if (pawn.anim.GetBool("SpearAttack"))//if attacking
        {
            pawn.anim.SetBool("SpearAttack", false);//stop attack animation
        }
    }
    public virtual void SpearAltStart()
    {
        //throw
        Throw();
    }
    public virtual void SpearAltEnd()
    {
        //nothing -- This fires on attack down
    }
    public void Throw()
    {
        Ammo thrown = Instantiate(projectilePrefab, origin.position, origin.rotation, origin) as Ammo;//create projectile object
        Ammo ThrownScript = thrown.GetComponent<Ammo>();//get component from new projectile
        thrown.gameObject.layer = gameObject.layer;//assign thrown object to parent objecst layer
        thrown.from = origin;
    }

    //collision events
    public virtual void OnCollisionEnter(Collision collision)
    {

    }
    public virtual void OnTriggerEnter(Collider thingWeHit)//make sure to use collider and not collision.  Pass in weapon damage from weapon
    {
        if (isAttacking == true)
        {
            if (thingWeHit.transform.root.GetComponent<Pawn>())//if we run into a pawn object
            {
                Pawn hitTarget = thingWeHit.GetComponent<Pawn>();//set the target to the the thing we hit

                if (hitTarget.transform.root.GetComponent<Health>())//if target has more than 0 health
                {
                    Health health = thingWeHit.transform.root.GetComponent<Health>();//reference for health component of object we are hitting
                    health.Damage(weaponDamage);//call objects damage function, give it the weapon damage of equipped weapon
                }
               
            }
            
        }
       
    }
}
