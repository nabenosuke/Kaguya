using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BakeG : MonoBehaviour
{
  public float speed = 3;
  public float wide = 5;
  public float period = 2;
  public bool isRight = false;
  private float height;
  private float moveTime = 0;
  private Vector2 velocity = Vector2.zero;
  private Rigidbody2D rb = null;
  private SpriteRenderer sr = null;


  // Start is called before the first frame update
  void Start()
  {
    rb = GetComponent<Rigidbody2D>();
    sr = GetComponent<SpriteRenderer>();
    height = transform.position.y;

    if (isRight)
    {
      transform.localScale = new Vector3(-1, 1, 1);
      velocity = new Vector2(speed, 0);
    }
    else
    {
      transform.localScale = new Vector3(1, 1, 1);
      velocity = new Vector2(-speed, 0);
    }
  }

  // Update is called once per frame
  void FixedUpdate()
  {
    if (sr.isVisible)
    {
      moveTime += Time.fixedDeltaTime;
      rb.velocity = velocity + new Vector2(0, wide * Mathf.Cos(moveTime * 2 * Mathf.PI / period));
    }
    else
    {
      rb.Sleep();
    }
  }


}
