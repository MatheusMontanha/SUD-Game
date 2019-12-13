using System.Collections;
using System.Collections.Generic;
using UnityEngine;
enum LogState
{
    SLEEPING, WAKING_UP, WAKE_UP, BLOCKED, GOING_TO_SLEEP
}
public class Log : Enemy
{
    public float chaseRadius;
    public float attackRadius;
    public Transform homePosition;
    // Start is called before the first frame update

    private Animator animator;
    private Rigidbody2D myRigidBody;
    private LogState logState = LogState.SLEEPING;

    private bool logWalking = false;

    private Collider2D myCollider;
    private SpriteRenderer myRenderer;
    public GameObject deathEffect;
    public GameObject life;

    new void Start()
    {
        base.Start();
        currentState = EnemyState.idle;
        myRigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        myCollider = GetComponent<BoxCollider2D>();
        myRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckDistance();
    }

    private IEnumerator WakeUpCo()
    {
        logState = LogState.WAKING_UP;
        yield return new WaitForSeconds(.3f);
        logState = LogState.WAKE_UP;
    }

    private void GoToSleep()
    {
        animator.SetBool("walking", false);
        animator.SetBool("wakeUp", false);
        StartCoroutine(GoToSleepCo());
    }
    private IEnumerator GoToSleepCo()
    {
        logState = LogState.GOING_TO_SLEEP;
        yield return new WaitForSeconds(.3f);
        logState = LogState.SLEEPING;
    }


    
    private void CheckDistance()
    {
        GameObject player1 = GameObject.FindWithTag("Player1");
        GameObject player2 = GameObject.FindWithTag("Player2");
        Transform target;
        // transform.Translate(Vector3.up * Time.deltaTime);
        if (player1 == null && player2 == null)
        {
            GoToSleep();
            return;
        }
        else if (player1 == null)
        {
            target = player2.transform;

        }
        else if (player2 == null)
        {
            target = player1.transform;
        }
        else if (Vector3.Distance(player1.transform.position, transform.position) < Vector3.Distance(player2.transform.position, transform.position))
            target = player1.transform;
        else
            target = player2.transform;

        if (Vector3.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position, transform.position) > attackRadius)
        {
            if (currentState == EnemyState.idle || currentState == EnemyState.walk && currentState != EnemyState.stagger)
            {

                if (logState == LogState.SLEEPING)
                    StartCoroutine(WakeUpCo());
                if (logState == LogState.WAKING_UP)
                {
                    animator.SetBool("wakeUp", true);
                }
                if (logState == LogState.WAKE_UP)
                {
                    animator.SetBool("walking", true);
                    Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                    ChangeAnim(temp - transform.position);
                    myRigidBody.MovePosition(temp);
                    ChangeCurrentState(EnemyState.walk);
                }
            }

            else
            {
                animator.SetBool("walking", false);
            }
        }
        else if (Vector3.Distance(target.position, transform.position) > chaseRadius)
        {
            GoToSleep();

        }

    }

    private void SetAnimFloat(Vector2 vector)
    {
        animator.SetFloat("moveX", vector.x);
        animator.SetFloat("moveY", vector.y);
    }
    private void ChangeAnim(Vector2 direction)
    {

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
            {
                SetAnimFloat(Vector2.right);
            }
            else if (direction.x < 0)
            {
                SetAnimFloat(Vector2.left);
            }
        }
        else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
        {
            if (direction.y > 0)
            {
                SetAnimFloat(Vector2.up);
            }
            else if (direction.y < 0)
            {
                SetAnimFloat(Vector2.down);
            }
        }

    }
    private void ChangeCurrentState(EnemyState newState)
    {
        if (newState != currentState)
        {
            currentState = newState;
        }
    }
    protected override void Die()
    {
        currentState = EnemyState.idle;
        logState = LogState.BLOCKED;
        this.gameObject.SetActive(false);
        if (deathEffect != null)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, 1f);
        }
        int x = new System.Random().Next(3);
        if (x == 0)
        {
            GameObject effect = Instantiate(life, transform.position, Quaternion.identity);
        }
        Destroy(this.gameObject, 1f);
        enemyCountdown.value--;

    }


    // void OnCollisionStay2D(Collision2D collision)
    //     {
    //         Collider2D collider = collision.collider;
    //         bool collideFromLeft = false;
    //         bool collideFromTop = false;
    //         bool collideFromRight = false;
    //         bool collideFromBottom = false;
    //         int thisWidth = (int)this.GetComponent<Collider2D>().bounds.size.x;
    //         int thisHeight = (int)this.GetComponent<Collider2D>().bounds.size.y;
    //         int circleRad = (int)collider.bounds.size.x;


    //         if (!collider.tag.StartsWith("Player"))
    //         {

    //             Vector3 contactPoint = collision.contacts[0].point;
    //             Debug.Log(contactPoint);
    //             Vector3 center = collider.bounds.center;

    //             if(contactPoint.y>center.y && contactPoint.x<center.x+thisWidth/2)
    //             if (contactPoint.y > center.y && //checks that circle is on top of rectangle
    //                 (contactPoint.x < center.x + thisWidth / 2 && contactPoint.x > center.x - thisWidth / 2))
    //             {
    //                 collideFromTop = true;
    //             }
    //             else if (contactPoint.y < center.y &&
    //                 (contactPoint.x < center.x + thisWidth / 2 && contactPoint.x > center.x - thisWidth / 2))
    //             {
    //                 collideFromBottom = true;
    //             }
    //             else if (contactPoint.x > center.x &&
    //                 (contactPoint.y < center.y + thisHeight / 2 && contactPoint.y > center.y - thisHeight / 2))
    //             {
    //                 collideFromRight = true;
    //             }
    //             else if (contactPoint.x < center.x &&
    //                 (contactPoint.y < center.y + thisHeight / 2 && contactPoint.y > center.y - thisHeight / 2))
    //             {
    //                 collideFromLeft = true;
    //             }
    //             if (collideFromBottom || collideFromLeft || collideFromRight || collideFromTop)
    //             {

    //                 Debug.ClearDeveloperConsole();
    //                 Debug.Log(collideFromLeft);
    //                 Debug.Log(collideFromTop);
    //                 Debug.Log(collideFromRight);
    //                 Debug.Log(collideFromBottom);
    //             }
    //         }
    //     }




}
