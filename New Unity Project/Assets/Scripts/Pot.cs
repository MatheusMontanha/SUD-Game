using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Smash()
    {
        Debug.Log("SMAAAAAAAAAAAAAAAAAAAAAAAAAASH");
        animator.SetBool("broke", true);
        StartCoroutine(BreakCo());
    }

    private IEnumerator BreakCo(){
        yield return new WaitForSeconds(0.3f);
        this.gameObject.SetActive(false);
    }
}
