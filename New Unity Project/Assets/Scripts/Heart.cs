using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{

    void Start()
    {
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.tag.Contains("Player") && !other.gameObject.CompareTag("PlayerProjectile"))
        {
            
            PlayerMovement player = other.gameObject.GetComponent<PlayerMovement>();
            player.AddLife();
            //playsound
            Destroy(this.gameObject);
        }
    }
}
