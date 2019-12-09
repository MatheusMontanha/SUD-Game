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
    void Start()
    {
        currentState = EnemyState.idle;
        myRigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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
    
    private IEnumerator GoToSleep()
    {
        logState = LogState.GOING_TO_SLEEP;
        yield return new WaitForSeconds(.3f);
        logState = LogState.SLEEPING;
    }

    private void CheckDistance()
    {
        Transform player1 = GameObject.FindWithTag("Player1").transform;
        Transform player2 = GameObject.FindWithTag("Player2").transform;
        Transform target;
        if (Vector3.Distance(player1.position, transform.position) < Vector3.Distance(player2.position, transform.position))
            target = player1;
        else
            target = player2;
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
            animator.SetBool("walking", false);
            animator.SetBool("wakeUp", false);
            StartCoroutine(GoToSleep());

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
}
