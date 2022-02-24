using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    private Transform targetPlayer;
    public float enemySpeed;
    public SpriteRenderer spriteRenderer;
    public float enemyDistance;

    void Start()
    {
        targetPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, targetPlayer.position) <= enemyDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPlayer.position, enemySpeed * Time.deltaTime);
        }

        Vector2 direction = (targetPlayer.position - transform.position).normalized;
        spriteRenderer.flipX = direction.x > 0;

    }
}
