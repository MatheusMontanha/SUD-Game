using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Sign : MonoBehaviour
{


    public GameObject dialogBox;
    public Text dialogText;
    public string dialog;

    public KeyCode player1action;
    public KeyCode player2action;
    private bool player1InRange;
    private bool player2InRange;
    private PlayerCode startedAction;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(player1action) && player1InRange))
        {
            if (dialogBox.activeInHierarchy)
            {
                if (startedAction == PlayerCode.Player1)
                {
                    startedAction = PlayerCode.None;
                    dialogBox.SetActive(false);
                }
            }
            else if(startedAction==PlayerCode.None)
            {
                startedAction = PlayerCode.Player1;
                dialogText.text = dialog;
                dialogBox.SetActive(true);
            }
        }
        if ((Input.GetKeyDown(player2action) && player2InRange))
        {
            if (dialogBox.activeInHierarchy)
            {
                if (startedAction == PlayerCode.Player2)
                {
                    startedAction = PlayerCode.None;
                    dialogBox.SetActive(false);
                }
            }
            else if(startedAction==PlayerCode.None)
            {
                startedAction = PlayerCode.Player2;
                dialogText.text = dialog;
                dialogBox.SetActive(true);
            }
        }
        if ((!player1InRange && startedAction==PlayerCode.Player1 || (!player2InRange && startedAction==PlayerCode.Player2)))
        {
            startedAction = PlayerCode.None;
            dialogBox.SetActive(false);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player1"))
        {
            player1InRange = true;
        }
        if (other.CompareTag("Player2"))
        {
            player2InRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player1"))
        {
            player1InRange = false;
        }
        if (other.CompareTag("Player2"))
        {
            player2InRange = false;
        }
    }
}
