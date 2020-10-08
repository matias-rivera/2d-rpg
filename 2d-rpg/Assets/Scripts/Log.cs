using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : Enemy
{
    public Transform target; //player position
    public float chaseRadius; //enemy vision 
    public float attackRadius; //enemy stop at this distance cause he will attack
    public Transform homePosition; //idle position

    // Start is called before the first frame update
    void Start()
    {
        //set player distance
        target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
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
            //chase player
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        }
    }
}
