﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    // Variables
    public int hp = 100;
    public int coins;
    public int keys;

    // Eventos
    public UnityEvent onGameWin;

    // Read-Only Propertie
    public float HpPercent
    {
        get 
        {
            return (float) hp / 100;
        }
    }

    public int speed = 4;

    public ForceMode2D forceMode;

    public float jumpForce = 2.5f;

    // <<< Referencias >>>
    public Rigidbody2D rb;
    public SpriteRenderer renderer;
    public Animator animator;

    // <<< Variables Privadas >>>
    bool canJump = true;

    void Update()
    {
        if (!IsAlive())
            return;
            
        var movimiento = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        if (movimiento > 0)
        {
            renderer.flipX = false;
        }

        if (movimiento < 0)
        {
            renderer.flipX = true;
        }
        
        var direc = new Vector3(movimiento, 0, 0);
        
        transform.Translate(direc);
        
        if(Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            Jump();
        }

        if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Q))
        {
            Attack();
        }
    }

    public void Attack()
    {
        animator.SetTrigger("Attack");
    }

    public void Jump()
    {
        rb.AddForce(transform.up * jumpForce, forceMode);
        animator.SetTrigger("Jump");
        canJump = false;
    }

    void Die()
    {
        speed = 0;
        //animator.SetTrigger("Die");
    }

    public bool IsAlive()
    {
        return hp > 0;
    }

    public bool HasHp(int _hp)
    {
        return hp > _hp;
    }

    public void TakeDamage(int _damage)
    {
        hp -= _damage;

        if (hp <= 0)
        {
            hp = 0;
            Die();
        }

        HUDController.Instance.UpdateHud(this);

        Debug.Log("Player has take " + _damage + " Damage");
    }

    public void TakeHp(int _hp)
    {
        hp += _hp;
        Debug.Log("Player has take " + _hp + " HP");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Champi"))
        {
            TakeHp(5);
            Destroy(other.gameObject);       
        }

        if (other.gameObject.CompareTag("Corazon"))
        {
            TakeHp(10);
            Destroy(other.gameObject);       
        }

        if (other.gameObject.CompareTag("Enemigo"))
        {
            // Este "gameObject" tiene toda la info (component) del objecto...
            // other.gameObject
            
            // GetDamage() de ese enemigo.
            TakeDamage(other.gameObject.GetComponent<Enemy>().GetDamage());
            Destroy(other.gameObject);   
        }

        if (other.gameObject.CompareTag("Coin"))
        {
            coins++;
            Destroy(other.gameObject);
            HUDController.Instance.UpdateHud(this);
        }

        if (other.gameObject.CompareTag("Key"))
        {
            keys++;
            Destroy(other.gameObject);
            HUDController.Instance.UpdateHud(this);

            if (keys >= 3)
            {
                // call unity event...
                Debug.Log("Call unity event");
                onGameWin.Invoke();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            canJump = true;
            animator.SetTrigger("Ground");
        }
    }
}