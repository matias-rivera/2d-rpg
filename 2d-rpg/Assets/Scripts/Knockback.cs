using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{

    public float thrust; //impulse
    public float knockTime; //stop impulse


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //get enemy rigidbody
            Rigidbody2D enemy = collision.GetComponent<Rigidbody2D>();
            //if enemy exist
            if(enemy != null)
            {
                enemy.GetComponent<Enemy>().currentState = EnemyState.stagger;
                //get player and enemy difference position
                Vector2 difference = enemy.transform.position - transform.position;
                //get impulse using the difference 
                difference = difference.normalized * thrust;
                //impulse
                enemy.AddForce(difference, ForceMode2D.Impulse);
                //stop enemy impulse
                StartCoroutine(KnockCo(enemy));
            }
        }
    }

    private IEnumerator KnockCo(Rigidbody2D enemy)
    {
        if(enemy != null)
        {
            //wait for knocktime and then reduce velocity to zero to stop enemy's impulse, after that set enemy's rigidbody to kinematic
            yield return new WaitForSeconds(knockTime);
            enemy.velocity = Vector2.zero;
            enemy.GetComponent<Enemy>().currentState = EnemyState.idle;
        }
    }
}
