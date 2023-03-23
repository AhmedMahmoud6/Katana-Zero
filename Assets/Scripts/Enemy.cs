using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public SpriteRenderer sr;
    public GameObject player;
    public float speed;

    private float distance;
    public float distanceBetween;




    private void Update()
    {
        distance = Vector2.Distance(transform.position,player.transform.position);


        if(distance < distanceBetween)
        {
            StartCoroutine(enemyHold());
        }
    }
    
        IEnumerator enemyHold()
        {
            yield return new WaitForSeconds(1);
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);


        }

}
