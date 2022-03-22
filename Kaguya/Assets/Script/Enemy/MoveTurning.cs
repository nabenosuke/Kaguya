using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTurning : MonoBehaviour
{
    [SerializeField] private GameObject target;
    private Rigidbody2D rb;

    [SerializeField] public float period = 2.0f;
    [SerializeField] public float radius = 2.5f;
    [SerializeField] private float secondRadius = 15.0f;
    public float spreadTime = 6.0f;
    [SerializeField] private float spreadSpeed = 6.0f;
    [SerializeField] private float disTime = 8.0f;
    [SerializeField] private float index;
    private float timer = 0f;
    private float posX;
    private float posY;
    private float defaultRad;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        defaultRad = 2 * Mathf.PI * index;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        posX = target.transform.position.x + radius * Mathf.Sin(2 * Mathf.PI * timer / period + defaultRad);
        posY = target.transform.position.y + radius * Mathf.Cos(2 * Mathf.PI * timer / period + defaultRad);
        rb.MovePosition(new Vector2(posX, posY));

        if (timer > disTime)
        {
            timer = 0;
            Destroy(gameObject);
        }
        else if (timer > spreadTime)
        {
            if (radius < secondRadius)
            {
                radius += Time.deltaTime * spreadSpeed;
            }
        }
    }

}
