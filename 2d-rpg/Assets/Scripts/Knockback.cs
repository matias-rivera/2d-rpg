using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{

    public float thrust; //impulse
    public float knockTime; //stop impulse


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Breakable")
            && this.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<Pot>().Smash();
        }


        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Player"))
        {
            //get enemy rigidbody
            Rigidbody2D hit = collision.GetComponent<Rigidbody2D>();
            //if enemy exist
            if(hit != null)
            {
                //get player and enemy difference position
                Vector2 difference = hit.transform.position - transform.position;
                //get impulse using the difference 
                difference = difference.normalized * thrust;
                //impulse
                hit.AddForce(difference, ForceMode2D.Impulse);
                if (collision.gameObject.CompareTag("Enemy"))
                {
                    hit.GetComponent<Enemy>().currentState = EnemyState.stagger;
                    collision.GetComponent<Enemy>().Knock(hit, knockTime);
                }
                if (collision.gameObject.CompareTag("Player"))
                {
                    hit.GetComponent<PlayerMovement>().currentState = PlayerState.stagger;
                    collision.GetComponent<PlayerMovement>().Knock(knockTime);
                }
             
            }
        }
    }

}
