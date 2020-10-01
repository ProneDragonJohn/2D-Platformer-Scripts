using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAi : MonoBehaviour
{
    Rigidbody2D rb2d;
    Coroutine slimeUpdate;

    [SerializeField] public float walkSpeed = 1;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        slimeUpdate = StartCoroutine(Idle());
    }

    IEnumerator Idle()
    {
        int direction = 1;

        while(true)
        {
            rb2d.velocity = new Vector2(walkSpeed * direction, 0);

            direction *= -1;
            transform.localScale = new Vector3(direction, 1, 1);

            yield return new WaitForSeconds(2f);
        }
    }
}
