using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    [SerializeField] string sceneName;
    public GameObject deathEffect;
    public float hp = 4f;
    public static int numOfEnemies = 0;


    void Start()
    {
        numOfEnemies++;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.relativeVelocity.magnitude > hp)
        {
            Die();
        }
        Debug.Log(collision.relativeVelocity.magnitude);
    }

    void Die ()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        numOfEnemies--;
        Debug.Log(numOfEnemies);
        Destroy(gameObject);
        if(numOfEnemies <= 0)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
