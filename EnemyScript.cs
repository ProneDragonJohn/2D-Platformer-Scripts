using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] private int health;

    // Materials
    private Material matWhite;
    private Material matDefault;
    SpriteRenderer sr;
    
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        matWhite = Resources.Load("RedFlash", typeof(Material)) as Material;
        matDefault = sr.material;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Attack"))
        {
            health--;
            sr.material = matWhite;
            if(health <= 0)
            {
                Death();
            }

            Invoke("ResetMaterial", .2f);
        }
    }

    void ResetMaterial()
    {
        sr.material = matDefault;
    }

    private void Death()
    {
        Destroy(gameObject);
    }
}
