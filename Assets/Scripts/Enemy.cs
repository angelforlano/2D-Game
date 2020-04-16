using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int minDamage = 1;
    public int maxDamage = 10;
    public float moveSpeed = 2;
    public float moveTime = 2;
    public SpriteRenderer renderer;

    int damage;
    bool direcc;

    void Start()
    {
        damage = Random.Range(minDamage, maxDamage);

        StartCoroutine(Move());
    }

    void Update()
    {
        if (direcc)
        {
            transform.Translate(new Vector3(1 * moveSpeed * Time.deltaTime, 0, 0));
            renderer.flipX = true;
        } else {
            transform.Translate(new Vector3(-1 * moveSpeed * Time.deltaTime, 0, 0));
            renderer.flipX = false;
        }
    }

    IEnumerator Move()
    {
        while (true)
        {
            direcc = false;
            yield return new WaitForSeconds(moveTime);
            
            direcc = true;
            yield return new WaitForSeconds(moveTime);
        }
    }

    public int GetDamage()
    {
        return damage;
    }
}