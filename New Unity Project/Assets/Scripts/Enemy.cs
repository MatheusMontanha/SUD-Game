using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    idle, walk, attack, stagger
}
public class Enemy : MonoBehaviour
{

    public int health;
    public string enemyName;
    public int baseAttack;
    public float moveSpeed;

    public EnemyState currentState;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Knock(Rigidbody2D myRB2D, float knockTime){
        currentState = EnemyState.stagger;
        StartCoroutine(KnockCo(myRB2D,knockTime) );
    }
    private IEnumerator KnockCo(Rigidbody2D myRB2D, float knockTime)
    {
        if (myRB2D != null)
        {
            yield return new WaitForSeconds(knockTime);
            myRB2D.velocity = Vector2.zero;
            currentState = EnemyState.idle;
        }
    }
}
