using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveStraight : MonoBehaviour, IEnemyMove
{
    public GroundCheck ground;
    public float speed = 3f;
    public bool isGravity;
    [Header("画面外でも行動する")] public bool nonVisibleAct;

    private IEnemyMoveOption iEnemyMoveOption = null;
    private Vector2 velocity = Vector2.zero;
    private Rigidbody2D rb = null;
    private SpriteRenderer sr = null;
    private float gravity;
    private float fallSpeed;
    private int rightNum = 1;
    private bool isGround;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        iEnemyMoveOption = GetComponent<IEnemyMoveOption>();
        if (GetComponent<Enemy>().isRight)
        {
            rightNum = 1;
        }
        else
        {
            rightNum = -1;
        }
        if (isGravity)
        {
            gravity = GManager.instance.gravity;
            fallSpeed = -GManager.instance.fallSpeed;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        if (sr.isVisible || nonVisibleAct)
        {

            rb.velocity = new Vector2(speed * rightNum, GetYSpeed());
            if (iEnemyMoveOption != null)
            {
                rb.velocity += iEnemyMoveOption.OptionVelocity();
            }
            if (rb.velocity.y < fallSpeed)
            {
                rb.velocity = new Vector2(rb.velocity.x, fallSpeed);
            }
        }
        else
        {
            rb.Sleep();
        }
    }
    private float GetYSpeed()
    {
        float ySpeed = rb.velocity.y;
        //重力を適用する
        if (isGravity)
        {
            if (ground != null)
            {
                if (!ground.IsGround())
                {
                    ySpeed -= gravity * Time.fixedDeltaTime;
                }
                else
                {
                    ySpeed = 0;
                }
            }
        }
        return ySpeed;
    }
    public void Turn()
    {
        transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
        rightNum = -1 * rightNum;
    }
}
