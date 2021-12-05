using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveJump : MonoBehaviour, IEnemyMoveOption
{
    [SerializeField] private GroundCheck groundCheck;
    [SerializeField] private float jumpPower = 10f;
    [SerializeField] private float jumpInterval = 4f;
    private SpriteRenderer sr = null;
    private float jumpTimer = 0f;
    private bool canJump = false;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (sr.isVisible)
        {
            if (groundCheck.IsGround())
            {
                if (!canJump)
                {
                    jumpTimer += Time.deltaTime;
                    if (jumpTimer > jumpInterval)
                    {
                        canJump = true;
                    }
                }
            }
        }
    }

    Vector2 IEnemyMoveOption.OptionVelocity()
    {
        if (canJump)
        {
            canJump = false;
            jumpTimer = 0f;
            return new Vector2(0, jumpPower);
        }
        else
        {
            return new Vector2(0, 0);
        }
    }
}
