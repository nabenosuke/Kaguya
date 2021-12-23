using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tama : MonoBehaviour
{
    [SerializeField] private GameObject explosion;
    [Header("スピード")] public float speed = 15.0f;
    [Header("高度")] public float height = 15.0f;
    [Header("向き")] public float left1right_1 = 1;
    private Rigidbody2D rb;
    private bool isFall = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (transform.localScale.x < 0)
        {
            left1right_1 = -1;
        }
        rb.velocity = new Vector2(-speed * left1right_1, height);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /*落下し始めるまで地面貫通
        if (!isFall)
        {
            if (rb.velocity.y < 0)
            {
                Debug.Log("tama fall");
                isFall = true;
                GetComponent<AttackObject>().isIgnoreGround = false;
            }
        }
        */
    }
}
