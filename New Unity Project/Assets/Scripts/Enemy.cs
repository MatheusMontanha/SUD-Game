using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    idle, walk, attack, stagger
}
public abstract class Enemy : MonoBehaviour
{

    public FloatValue maxHealth;
    private float health;
    public string enemyName;
    public int baseAttack;
    public float moveSpeed;

    private Rigidbody2D myRB2D;
    public EnemyState currentState;
    // Start is called before the first frame update

    protected void Start()
    {
        myRB2D = GetComponent<Rigidbody2D>();
        health = maxHealth.value;
    }


    protected abstract void Die();
    void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }
    public void TakeHit(float knockTime, float damage)
    {
        
        TakeDamage(damage);
        if(health>0)
        Knock(knockTime);
    }

    private void Knock(float knockTime)
    {
        currentState = EnemyState.stagger;
        StartCoroutine(KnockCo(knockTime));
    }
    private IEnumerator KnockCo(float knockTime)
    {
        if (myRB2D != null)
        {
            yield return new WaitForSeconds(knockTime);
            Debug.Log("after");
            myRB2D.velocity = Vector2.zero;
            currentState = EnemyState.idle;
        }
    }
}
