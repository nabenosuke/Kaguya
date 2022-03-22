using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour
{
    //回転
    // Start is called before the first frame update
    [Header("スピード")] public float speed = 12.0f;
    [Header("向き")] public float left1right_1 = 1;
    private Rigidbody2D rb;
    private float timer = 0f;
    private float returnTime = 0.4f;
    private float returnSpeed = 30f;
    private float rotateRate = 120f;
    private float rotatePeriod = 0.2f;
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
    void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        if (rb.velocity.x >= -speed && rb.velocity.x <= speed)
        {
            if (returnTime < timer)
            {
                rb.velocity = rb.velocity + new Vector2(returnSpeed * Time.deltaTime * left1right_1, 0);
            }
        }
    }

    void Update()
    {
        transform.eulerAngles = new Vector3(0, 0, Mathf.Floor(timer / rotatePeriod) * rotateRate);
    }
}
