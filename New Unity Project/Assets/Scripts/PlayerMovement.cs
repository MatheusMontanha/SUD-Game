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
    void Start()
    {
        animator = GetComponent<Animator>();
        myrigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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

        if (Input.GetKeyDown(action2) && playerState!=PlayerState.attack)
        {
            Debug.Log("attack");
            StartCoroutine(AttackCo());
        }
        else if (playerState == PlayerState.walk)
            // Debug.Log("moving");
            UpdateAnimationAndMove();
    }

    private IEnumerator AttackCo(){
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
                     transform.position + Vector3.zero * speed * Time.deltaTime
                );
    }
    void MoveCharacter()
    {
        myrigidbody.MovePosition(
            transform.position + change * speed * Time.deltaTime
        );

    }
    private void LateUpdate()
    {
        GetComponent<SpriteRenderer>().sortingOrder = -(int)(transform.position.y * 100);
    }
}
