using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tsubasa : MonoBehaviour, IBoss
{
  public float attackTime = 10;
  public float movePeriod = 2;
  public float moveWide = 0.1f;
  public float interval1 = 2;

  private GameObject stageArea;
  private GameObject bakeW;
  private Rigidbody2D rb;
  private Boss boss;
  private Vector2 stageLowerLeft;
  private Vector2 stageUpperRight;
  private float attackTimer = 0f;
  private float moveTimer = 0f;
  private bool isAttack = false;
  // Start is called before the first frame update
  void Start()
  {
    rb = GetComponent<Rigidbody2D>();
    boss = GetComponent<Boss>();
    stageArea = GameObject.Find("StageArea");
    bakeW = transform.Find("BakeW").gameObject;
    //ステージの広さ取得
    stageLowerLeft = new Vector2(stageArea.transform.position.x - stageArea.transform.localScale.x / 2, stageArea.transform.position.y - stageArea.transform.localScale.y / 2);
    stageUpperRight = new Vector2(stageArea.transform.position.x + stageArea.transform.localScale.x / 2, stageArea.transform.position.y + stageArea.transform.localScale.y / 2);
  }

  // Update is called once per frame
  void FixedUpdate()
  {
    moveTimer += Time.deltaTime;
    if (moveTimer > movePeriod)
    {
      moveTimer = 0;
    }

    rb.velocity = new Vector2(0, moveWide * 2 * Mathf.PI / movePeriod * Mathf.Cos(moveTimer * 2 * Mathf.PI / movePeriod));
  }

  void IBoss.Attack0()
  { //左右から白バケ
    if (!isAttack)
    {
      isAttack = true;
      float x = stageUpperRight.x;
      for (float y = stageLowerLeft.y + interval1; y < stageUpperRight.y; y = y + interval1 * 2)
      {
        GameObject g = Instantiate(bakeW);
        g.transform.position = new Vector2(x, y);
        g.SetActive(true);
      }

      x = stageLowerLeft.x;
      for (float y = stageLowerLeft.y + interval1 * 2; y < stageUpperRight.y; y = y + interval1 * 2)
      {
        GameObject g = Instantiate(bakeW);
        g.transform.position = new Vector2(x, y);
        g.GetComponent<Enemy>().isRight = true;
        g.SetActive(true);
      }
    }
    if (attackTimer < attackTime)
    {
      attackTimer += Time.deltaTime;
    }
    else
    {
      boss.FinishAct();
      isAttack = false;
      attackTimer = 0f;
    }
  }
}
