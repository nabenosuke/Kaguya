using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kunai : MonoBehaviour
{
    [Header("スピード")] public float speed = 15.0f;
    [Header("向き")] public float left1right_1 = 1;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (transform.localScale.x < 0)
        {
            left1right_1 = -1;
        }
        rb.velocity = new Vector2(-speed * left1right_1, 0);
    }

    // Update is called once per frame
}
