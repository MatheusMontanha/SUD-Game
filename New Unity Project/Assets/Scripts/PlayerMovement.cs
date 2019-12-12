using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed;
    private Rigidbody2D myrigidbody;
    private Vector3 change;

    public KeyCode left;
    public KeyCode right;
    public KeyCode up;
    public KeyCode down;

    public KeyCode action1;
    public KeyCode action2;

    // Start is called before the first frame update
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private PlayerState playerState;
    public FloatValue currentHealth;
    public SSignal playerHealthSignal;

    public GameObject projectile;
    public bool attackWithProjectile;

    public BooleanValue playerAlive;
    void Start()
    {
        animator = GetComponent<Animator>();
        myrigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", -1);
        playerState = PlayerState.walk;
    }

    // Update is called once per frame
    void Update()
    {

        change = new Vector3(0, 0, 0);
        if (Input.GetKey(left) && Input.GetKey(right))
        {
            change.x = 0;
        }
        else
        if (Input.GetKey(left))
        {
            change.x = -1;
        }
        else if (Input.GetKey(right))
        {
            change.x = 1;
        }
        else
        {
            change.x = 0;
        }

        if (Input.GetKey(up) && Input.GetKey(down))
        {
            change.y = 0;
        }
        else
        if (Input.GetKey(down))
        {
            change.y = -1;
        }
        else if (Input.GetKey(up))
        {
            change.y = 1;
        }
        else
        {
            change.y = 0;
        }

        if (Input.GetKeyDown(action2) && playerState != PlayerState.attack && playerState != PlayerState.stagger && playerState != PlayerState.firing)
        {
            if (attackWithProjectile)
            {
                StartCoroutine(AttackWithProjectileCo());
            }
            else
            {

                StartCoroutine(AttackCo());
            }
        }
        else if (playerState == PlayerState.walk || playerState == PlayerState.firing)
        {
            UpdateAnimationAndMove();
        }

    }

    internal void TakeHit(float knockTime, float damage)
    {
        if (playerState != PlayerState.stagger)
        {
            TakeDamage(damage);
            if (currentHealth.value > 0)
            {
                Knock(knockTime);
            }
            else
            {
                Die();
            }

        }
    }
    private void Die()
    {
        this.playerAlive.value = false;
        this.gameObject.SetActive(false);

    }

    private void TakeDamage(float damage)
    {
        currentHealth.value -= damage;
        playerHealthSignal.Raise();

    }//AttackWithProjectileCo
    private IEnumerator AttackWithProjectileCo()
    {
        // animator.SetBool("attacking", true);
        playerState = PlayerState.firing;
        yield return null;
        MakeArrow();
        // animator.SetBool("attacking", false);
        yield return new WaitForSeconds(.3f);
        playerState = PlayerState.walk;
    }

    private void MakeArrow()
    {

        Vector2 temp = new Vector2(animator.GetFloat("moveX"), animator.GetFloat("moveY"));
        Arrow arrow = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Arrow>();
        arrow.Setup(temp, ChooseArrowDirection());
        Destroy(arrow.gameObject, 10f);
    }
    Vector3 ChooseArrowDirection()
    {
        float temp = Mathf.Atan2(animator.GetFloat("moveY"), animator.GetFloat("moveX")) * Mathf.Rad2Deg;
        return new Vector3(0, 0, temp);
    }
    private IEnumerator AttackCo()
    {
        animator.SetBool("attacking", true);
        playerState = PlayerState.attack;
        yield return null;
        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(.3f);
        playerState = PlayerState.walk;


    }

    void UpdateAnimationAndMove()
    {
        if (change != Vector3.zero)
        {
            MoveCharacter();
            change.x = Mathf.Round(change.x);
            change.y = Mathf.Round(change.y);
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("moving", true);
        }
        else
        {
            StopCharacter();
            animator.SetBool("moving", false);
        }
    }

    void StopCharacter()
    {
        myrigidbody.MovePosition(
                     transform.position + Vector3.zero
                );
    }
    void MoveCharacter()
    {
        myrigidbody.MovePosition(
            transform.position + change * speed * Time.deltaTime
        );

    }

    private void Knock(float knockTime)
    {
        playerState = PlayerState.stagger;
        StartCoroutine(KnockCo(knockTime));
    }
    private IEnumerator KnockCo(float knockTime)
    {
        if (myrigidbody != null)
        {
            yield return new WaitForSeconds(knockTime);
            myrigidbody.velocity = Vector2.zero;
            playerState = PlayerState.walk;
        }
    }
    private void LateUpdate()
    {
        GetComponent<SpriteRenderer>().sortingOrder = -(int)(transform.position.y * 100);
    }
}
