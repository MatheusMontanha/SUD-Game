using System;
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

    public bool strongAgainstMelee;
    public bool strongAgainstProjectile;

    public bool weakToMelee;
    public bool weakToProjectile;
    Color originalColor; 
    public FloatValue enemyCountdown;

    protected void Start()
    {
        myRB2D = GetComponent<Rigidbody2D>();
        health = maxHealth.value;
        this.originalColor = GetComponent<SpriteRenderer>().color;
    }


    protected abstract void Die();
    void TakeDamage(float damage, string collisionTag)
    {
        if (collisionTag.Contains("Projectile") && strongAgainstProjectile)
        {
            health -= damage / 2;
            Debug.Log("strong");
        }
        else if (collisionTag.Contains("Projectile") && weakToProjectile)
        {
            health -= damage * 2;
            Debug.Log("weak");
        }
        else if (collisionTag.Contains("Melee") && strongAgainstMelee)
        {
            health -= damage / 2;
            Debug.Log("strong");
        }
        else if (collisionTag.Contains("Melee") && weakToMelee)
        {
            health -= damage * 2;
            Debug.Log("weak");
        }
        else
        {
            health -= damage;
        }
        if (health <= 0)
        {
            Die();
        }
    }

    internal void AttatchCounter(FloatValue enemyCounter)
    {
        this.enemyCountdown = enemyCounter;
    }

    public void TakeHit(float knockTime, float damage, string collisionTag)
    {

        TakeDamage(damage, collisionTag);
        if (health > 0)
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
            GetComponent<SpriteRenderer>().color = Color.red;
            yield return new WaitForSeconds(knockTime);
            myRB2D.velocity = Vector2.zero;
            currentState = EnemyState.idle;
            GetComponent<SpriteRenderer>().color = originalColor;
        }
    }

    private void LateUpdate()
    {
        GetComponent<SpriteRenderer>().sortingOrder = -(int)(transform.position.y * 100);
    }
}
