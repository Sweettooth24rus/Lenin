using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animator animator;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public Transform player;
    public float attackRate;
    public float attackRange;
    public LayerMask playerLayer;
    private float attackRateTemp;
    public int health = 100;
    public float agroRange = 4f;
    public float moveSpeed;
    private float moveSpeedTemp;
    public bool haveGun = false;
    private bool faceRight = true;
    public float jumpForce;
    public int meleeDamage;
    public int bulletSpeed;
    public Vector2 bottomOffset;
    public Vector2 collisionSize;
    public LayerMask groundLayer;
    private bool isAttack1 = false;
    private bool isAttack2 = false;
    private bool isShoot = false;

    Rigidbody2D rb;

    public GameObject deathEffect;

    private void Start()
    {
        moveSpeedTemp = moveSpeed;
        animator = GetComponent<Animator>();
        attackRateTemp = attackRate;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        
        float distToPlayer = Vector2.Distance(transform.position, player.position);
        if (!haveGun)
        {
            if (distToPlayer < agroRange)
            {
                ChasePlayerMelee();
            }
            else
            {
                StopChasingPlayerMelee();
            }
        }
        if (haveGun)
        {
            if (distToPlayer < agroRange * 2)
            {
                ChasePlayerRange();
            }
            else
            {
                StopChasingPlayerRange();
            }
        }
        if (moveSpeed == 0)
        {
            animator.SetFloat("Speed", 0);
        }
        else
        {
            animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x / moveSpeed));
        }
        animator.SetBool("IsGround", GroundCheck());
        animator.SetFloat("YSpeed", rb.velocity.y);
        animator.SetBool("IsAttack1", isAttack1);
        animator.SetBool("IsAttack2", isAttack2);
        animator.SetBool("IsShoot", isShoot);
        if(isAttack1 || isAttack2 || isShoot)
        StartCoroutine(ResetAttack(0.3f));
    }

    private void FixedUpdate()
    {
        attackRate -= 0.1f;
    }

    private void ChasePlayerRange()
    {
        if (transform.position.x + agroRange < player.position.x)
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            if (player.position.y - transform.position.y > 5f)
            {
                RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, new Vector3(1, 0.8f), 100f);
                Debug.DrawRay(transform.position, new Vector3(1, 1));
                if (hit.Length >= 3 && hit[2].collider.tag == "AI jump")
                {
                    Jump();
                }
            }
        }
        else if (transform.position.x - agroRange > player.position.x)
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            if (player.position.y - transform.position.y > 5f)
            {
                RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, new Vector3(-1, 0.8f), 100f);
                Debug.DrawRay(transform.position, new Vector3(-1, 1));
                if (hit.Length >= 3 && hit[2].collider.tag == "AI jump")
                {
                    Jump();
                }
            }
        }
        if (transform.position.x < player.position.x && faceRight)
            flip();
        else if (transform.position.x > player.position.x && !faceRight)
            flip();
        AttackRange();
    }

    private void StopChasingPlayerRange()
    {
        rb.velocity = new Vector2(0, 0);
    }

    void ChasePlayerMelee()
    {
        if (transform.position.x + 2 < player.position.x)
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            if(player.position.y - transform.position.y > 5f)
            {
                RaycastHit2D[] hit = Physics2D.RaycastAll (transform.position, new Vector3(1,0.8f), 100f);
                Debug.DrawRay(transform.position, new Vector3(1, 1));
                if (hit.Length >= 3 && hit[2].collider.tag == "AI jump")
                {
                    Jump();
                }
            }
        }
        else if (transform.position.x - 2 > player.position.x)
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            if (player.position.y - transform.position.y > 5f)
            {
                RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, new Vector3(-1, 0.8f), 100f);
                Debug.DrawRay(transform.position, new Vector3(-1, 1));
                if (hit.Length >= 3 && hit[2].collider.tag == "AI jump")
                {
                    Jump();
                }
            }
        }
        if (transform.position.x < player.position.x && faceRight)
            flip();
        else if (transform.position.x > player.position.x && !faceRight)
            flip();
        AttackMelee();
    }

    IEnumerator StopSpeed(float time)
    {
        if (GroundCheck())
        {
            moveSpeed = 0;
            yield return new WaitForSeconds(time);
            moveSpeed = moveSpeedTemp;
        }
    }

    IEnumerator ResetAttack(float time)
    {
        yield return new WaitForSeconds(time);
        isAttack1 = false;
        isAttack2 = false;
        isShoot = false;
    }

    bool GroundCheck()
    {
        return Physics2D.OverlapBox((Vector2)transform.position + bottomOffset, collisionSize, 0f, groundLayer);
    }

    void StopChasingPlayerMelee()
    {
        rb.velocity = new Vector2(0, 0);
    }

    
    public void TakeDamage (int damage) {
        health -= damage;

        if (health <= 0)
        Die();
    }

    void flip()
    {
        faceRight = !faceRight;
        transform.Rotate(0f, 180f, 0f);
    }

    void AttackMelee()
    {
        if (attackRate < 0f)
        {
            
            Collider2D[] playerToDamage = Physics2D.OverlapCircleAll(transform.position - (transform.position - player.transform.position).normalized * 2, attackRange, playerLayer);
            for (int i = 0; i < playerToDamage.Length; i++)
            {
                PlayerMoves player = playerToDamage[i].GetComponent<PlayerMoves>();
                Debug.Log(playerToDamage[i].tag);
                if (player != null)
                {
                    float choice = (float)UnityEngine.Random.Range(0, 2);
                    Debug.Log(choice);
                    if (choice == 1)
                    {
                        isAttack1 = true;
                    }
                    else
                    {
                        isAttack2 = true;
                    }
                    attackRate = attackRateTemp;
                    StartCoroutine(StopSpeed(0.2f));
                    //player.TakeDamage(meleeDamage);
                    StartCoroutine(StopSpeed(0.2f));
                }
            }
        }
    }

    void AttackRange()
    {
        bool haveWall = false;
        RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, -(transform.position - player.transform.position), agroRange, playerLayer);
        Debug.DrawRay(transform.position, -(transform.position - player.transform.position));
        if (attackRate < 0f)
        {
            for(int i = 0; i < hit.Length; i++)
            {
                isShoot = true;
                if( hit[i].collider.tag == "Ground" || hit[i].collider.tag == "Wall")
                {
                    haveWall = true;
                }
                if (hit[i].collider.tag == "Player" && haveWall == false)
                {
                    attackRate = attackRateTemp;
                    GameObject bullet = (GameObject)Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(-(transform.position + (transform.position - player.transform.position).normalized)));
                    bullet.GetComponent<Rigidbody2D>().velocity = -(transform.position - player.transform.position).normalized * bulletSpeed;
                }
                StartCoroutine(StopSpeed(0.2f));
            }
        }
        haveWall = false;
    }

    void Die () {
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube((Vector2)transform.position + bottomOffset, collisionSize);
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.velocity += Vector2.up * jumpForce;
    }

}
