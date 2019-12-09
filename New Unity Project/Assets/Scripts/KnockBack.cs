using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    public float thrust;
    public float knockTime;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("breakable") && other.gameObject.tag.Contains("Player"))
        {
            other.GetComponent<Pot>().Smash();
        }
        if (other.gameObject.CompareTag("enemy") || other.gameObject.tag.Contains("Player"))
        {
            Rigidbody2D HIT = other.gameObject.GetComponent<Rigidbody2D>();
            if (HIT != null)
            {

                Vector2 difference = HIT.transform.position - transform.position;
                difference = difference.normalized * thrust;
                HIT.AddForce(difference, ForceMode2D.Impulse);
                if (other.gameObject.CompareTag("enemy"))
                {
                    other.GetComponent<Enemy>().Knock(HIT, knockTime);
                }
                if (other.gameObject.tag.Contains("Player"))
                {
                    other.GetComponent<PlayerMovement>().Knock(knockTime);
                }

                // StartCoroutine(KnockCo(HIT));
            }
        }
    }

}
