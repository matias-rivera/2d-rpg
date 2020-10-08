using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : Enemy
{
    private Rigidbody2D myRigidbody;
    public Transform target; //player position
    public float chaseRadius; //enemy vision 
    public float attackRadius; //enemy stop at this distance cause he will attack
    public Transform homePosition; //idle position
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        currentState = EnemyState.idle;
        //set player distance
        target = GameObject.FindWithTag("Player").transform;
        myRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();   
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckDistance();   
    }

    
    void CheckDistance()
    {
        //Check for distance to chase the player, it chases if player is in chaseRadius and enemy dont have a difference distance of attackradius
        if(Vector3.Distance(target.position,
            transform.position) <= chaseRadius
            && Vector3.Distance(target.position,
            transform.position) > attackRadius
            )
        {
            if(currentState == EnemyState.idle || currentState == EnemyState.walk
                && currentState != EnemyState.stagger)
            {

                //chase player
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

                myRigidbody.MovePosition(temp);
                ChangeState(EnemyState.walk);
            }
        }
    }

    private void ChangeState(EnemyState newState)
    {
        //set new state
        if (currentState != newState)
        {
            currentState = newState;
        }
    }
}
