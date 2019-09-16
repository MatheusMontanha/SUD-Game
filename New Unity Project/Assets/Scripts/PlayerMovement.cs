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
    private HashSet<KeyCode> pressedKeys;

    // Start is called before the first frame update
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        animator = GetComponent<Animator>();
        myrigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        pressedKeys = new HashSet<KeyCode>();
    }

    // Update is called once per frame
    void Update()
    {
        int countKeys = 0;

        VerifyDownCodes();
        VerifyUpCodes();
        VerifyPressedKeys();
        Debug.Log("aaaa" + pressedKeys.Count.ToString());


        if (pressedKeys.Count == 4)
        {
            EditorUtility.DisplayDialog("NO", "NO", "OK");
            pressedKeys.Remove(up);
        }
        change = new Vector3(0, 0, 0);
        // if (lastPressedKey == left) && lastPressedKey == right))
        // {
        //     change.x = 0;
        // }
        // else
        if (lastPressedKey == left)
        {
            change.x = -1;
        }
        else if (lastPressedKey == right)
        {
            change.x = 1;
        }
        else
        {
            change.x = 0;
        }

        // if (lastPressedKey == up) && lastPressedKey == down))
        // {
        //     change.y = 0;
        // }
        // else
        if (lastPressedKey == down)
        {
            change.y = -1;
        }
        else if (lastPressedKey == up)
        {
            change.y = 1;
        }
        else
        {
            change.y = 0;
        }


        // change.x = Input.GetAxis("Horizontal");
        // change.y = Input.GetAxis("Vertical");

        UpdateAnimationAndMove();
    }

    void VerifyDownCodes()
    {
        if (Input.GetKeyDown(left) && Input.GetKeyDown(right))
        {
            pressedKeys.Add(left);
            pressedKeys.Add(right);
        }
        else if (Input.GetKeyDown(up) && Input.GetKeyDown(down))
        {
            pressedKeys.Add(up);
            pressedKeys.Add(down);
        }
        else if (Input.GetKeyDown(right))
        {
            pressedKeys.Add(right);

        }
        else if (Input.GetKeyDown(left))
        {
            pressedKeys.Add(left);
        }
        else if (Input.GetKeyDown(up))
        {
            pressedKeys.Add(up);
        }
        else if (Input.GetKeyDown(down))
        {
            pressedKeys.Add(down);
        }
    }

    private KeyCode lastPressedKey;
    void VerifyPressedKeys()
    {


        switch (pressedKeys.Count)
        {
            case 0:
                lastPressedKey = KeyCode.None;
                break;
            case 1:
                //left
                if (pressedKeys.Contains(left))
                {
                    lastPressedKey = left;
                }
                // right
                else if (pressedKeys.Contains(right))
                {
                    lastPressedKey = right;
                }
                // up
                else if (pressedKeys.Contains(up))
                {
                    lastPressedKey = up;
                }
                // down
                else if (pressedKeys.Contains(down))
                {
                    lastPressedKey = down;
                }
                break;

            case 2:
                // left right
                if (pressedKeys.Contains(left) && pressedKeys.Contains(right) && !pressedKeys.Contains(up) && !pressedKeys.Contains(down))
                {
                    lastPressedKey = KeyCode.None;
                }
                //up down
                else if (!pressedKeys.Contains(left) && !pressedKeys.Contains(right) && pressedKeys.Contains(up) && pressedKeys.Contains(down))
                {
                    lastPressedKey = KeyCode.None;
                }
                break;
            case 3:
                //left right up
                if (pressedKeys.Contains(left) && pressedKeys.Contains(right) && pressedKeys.Contains(up) && !pressedKeys.Contains(down))
                {
                    lastPressedKey = up;
                }
                //left right down
                else if (pressedKeys.Contains(left) && pressedKeys.Contains(right) && !pressedKeys.Contains(up) && pressedKeys.Contains(down))
                {
                    lastPressedKey = down;
                }
                // left up down
                else if (pressedKeys.Contains(left) && !pressedKeys.Contains(right) && pressedKeys.Contains(up) && pressedKeys.Contains(down))
                {
                    lastPressedKey = left;
                }
                //right up down
                else if (!pressedKeys.Contains(left) && pressedKeys.Contains(right) && pressedKeys.Contains(up) && pressedKeys.Contains(down))
                {
                    lastPressedKey = right;
                }
                break;
            case 4:
                lastPressedKey = KeyCode.None;
                break;
        }
        if (pressedKeys.Count == 0)
        {
            lastPressedKey = KeyCode.None;
        }
        //left right up
        else if (pressedKeys.Contains(left) && pressedKeys.Contains(right) && pressedKeys.Contains(up) && !pressedKeys.Contains(down))
        {
            lastPressedKey = up;
        }
        //left right down
        else if (pressedKeys.Contains(left) && pressedKeys.Contains(right) && !pressedKeys.Contains(up) && pressedKeys.Contains(down))
        {
            lastPressedKey = down;
        }
        // left up down
        else if (pressedKeys.Contains(left) && !pressedKeys.Contains(right) && pressedKeys.Contains(up) && pressedKeys.Contains(down))
        {
            lastPressedKey = left;
        }
        //right up down
        else if (!pressedKeys.Contains(left) && pressedKeys.Contains(right) && pressedKeys.Contains(up) && pressedKeys.Contains(down))
        {
            lastPressedKey = right;
        }
        // left right
        else if (pressedKeys.Contains(left) && pressedKeys.Contains(right) && !pressedKeys.Contains(up) && !pressedKeys.Contains(down))
        {
            lastPressedKey = KeyCode.None;
        }
        //up down
        else if (!pressedKeys.Contains(left) && !pressedKeys.Contains(right) && pressedKeys.Contains(up) && pressedKeys.Contains(down))
        {
            lastPressedKey = KeyCode.None;
        }
        // left
        else if (pressedKeys.Contains(left))
        {
            lastPressedKey = left;
        }
        // right
        else if (pressedKeys.Contains(right))
        {
            lastPressedKey = right;
        }
        // up
        else if (pressedKeys.Contains(up))
        {
            lastPressedKey = up;
        }
        // down
        else if (pressedKeys.Contains(down))
        {
            lastPressedKey = down;
        }
    }

    void VerifyUpCodes()
    {
        if (pressedKeys.Contains(right) && Input.GetKeyUp(right))
        {
            pressedKeys.Remove(right);

        }
        if (pressedKeys.Contains(left) && Input.GetKeyUp(left))
        {
            pressedKeys.Remove(left);
        }
        if (pressedKeys.Contains(up) && Input.GetKeyUp(up))
        {
            pressedKeys.Remove(up);
        }
        if (pressedKeys.Contains(down) && Input.GetKeyUp(down))
        {
            pressedKeys.Remove(down);
        }
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
