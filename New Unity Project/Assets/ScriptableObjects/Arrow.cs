using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed;
    public Rigidbody2D myRidigbody;
    void Start()
    {
        myRidigbody = GetComponent<Rigidbody2D>();
    }

    public void Setup(Vector2 velocity, Vector3 direction)
    {
        myRidigbody.velocity = velocity.normalized * speed;
        transform.rotation = Quaternion.Euler(direction);

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.tag.Contains("Player"))
            Destroy(this.gameObject);
    }
}
