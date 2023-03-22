using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public SpriteRenderer sr;

    float distance;


    void Update()
    {
        distance = Mathf.Abs(transform.position.x - player.position.x);

        //Debug.Log(Mathf.Abs(distance));
        if (distance < 10 && distance > 2)
        {
            StartCoroutine(enemyHold());

        }
    }
    IEnumerator enemyHold()
    {
        yield return new WaitForSeconds(1);
        if(transform.position.x > player.position.x)
        {
            

            sr.flipX = true;
            if(distance < 2)
            {
                
            }
            else if(distance > 2)
            {
                transform.Translate(new Vector2(-0.02f, 0));
            }
            
        }
        if (transform.position.x < player.position.x)
        {
            sr.flipX = false;
            if (distance !< 2)
            {
                Debug.Log("FOLLOW");
                transform.Translate(new Vector2(0.2f, 0));
            }

            
                
        }
    }
}
