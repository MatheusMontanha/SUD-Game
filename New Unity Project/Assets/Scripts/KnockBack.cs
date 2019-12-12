using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    public FloatValue thrust;
    public FloatValue knockTime;
    public FloatValue damage;
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
            if (this.tag!=other.tag && (other.gameObject.CompareTag("enemy") || other.gameObject.tag.Contains("Player")))
            {
                Rigidbody2D HIT = other.gameObject.GetComponent<Rigidbody2D>();
                if (HIT != null)
                {

                    Vector2 difference = HIT.transform.position - transform.position;
                    difference = difference.normalized * thrust.value;
                        HIT.AddForce(difference, ForceMode2D.Impulse);
                    if (this.tag != "enemy" && other.gameObject.CompareTag("enemy") && other.isTrigger)
                    {
                        other.GetComponent<Enemy>().TakeHit(knockTime.value, damage.value, this.tag);
                    }
                    if (!this.tag.Contains("Player") && other.gameObject.tag.Contains("Player"))
                    {

                        other.GetComponent<PlayerMovement>().TakeHit(knockTime.value, damage.value);
                    }

                    // StartCoroutine(KnockCo(HIT));
                }
            }
    }

}
