using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSin : MonoBehaviour, IEnemyMove
{
  //public GroundCheck ground;
  public float speed = 3;
  public float wide = 5;
  public float period = 2;
  [Header("画面外でも行動する")] public bool nonVisibleAct;
  //public bool isGravity;

  private Vector2 velocity = Vector2.zero;
  private Rigidbody2D rb = null;
  private SpriteRenderer sr = null;
  private float ySpeed;
  private float height;
  private float moveTime = 0;
  private int rightNum = 1;
  //private bool isGround;
  // Start is called before the first frame update
  void Start()
  {
    rb = GetComponent<Rigidbody2D>();
    sr = GetComponent<SpriteRenderer>();
    height = transform.position.y;

    if (GetComponent<Enemy>().isRight)
    {
      rightNum = 1;
    }
    else
    {
      rightNum = -1;
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
    }
    else
    {
      rb.Sleep();
    }
  }
  private float GetYSpeed()
  {
    moveTime += Time.fixedDeltaTime;
    ySpeed = wide * 2 * Mathf.PI / period * Mathf.Cos(moveTime * 2 * Mathf.PI / period);
    return ySpeed;
  }
  public void Turn()
  {
    transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
    rightNum = -1 * rightNum;
  }
}
