using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
  public GameObject[] bossPositions;
  public float speed = 3;

  private enum BossState
  {
    StartEnsyutu,
    Battle,
    ClearEnsyutu,
  }
  private enum BossAct
  {
    Move,
    Atatck,
  }

  private IBoss iBoss;
  private BossState nowState = BossState.StartEnsyutu;
  private BossAct bossAct = BossAct.Move;
  private Rigidbody2D rb;
  private Vector2 nextPosition;
  private int bossActNum = 0;
  private int bossActVol = 1;
  private int bossPosNum = 0;
  private int bossPosVol = 4;
  // Start is called before the first frame update
  void Start()
  {
    bossPosVol = bossPositions.Length;
    nextPosition = bossPositions[bossPosNum].transform.position;
    rb = GetComponent<Rigidbody2D>();
    iBoss = GetComponent<IBoss>();
  }

  // Update is called once per frame
  void Update()
  {
    if (transform.position.x > 0)
    {
      transform.localScale = new Vector3(1, 1, 1);
    }
    else
    {
      transform.localScale = new Vector3(-1, 1, 1);
    }

    switch (nowState)
    {
      case BossState.StartEnsyutu:
        nowState = BossState.Battle;
        //登場演出時の処理を書く
        break;
      case BossState.Battle:
        switch (bossAct)
        {
          case BossAct.Move:
            Move();
            break;

          case BossAct.Atatck:
            Attack();
            break;
        }
        break;
      case BossState.ClearEnsyutu:
        //クリア時の処理を書く
        break;
    }
  }

  private void Move()
  {
    if (Vector2.Distance(transform.position, nextPosition) > 0.1f)
    {
      //現在地から次のポイントへのベクトルを作成
      Vector2 toVector = Vector2.MoveTowards(transform.position, nextPosition, speed * Time.deltaTime);

      //次のポイントへ移動
      rb.MovePosition(toVector);
    }
    else
    {
      rb.MovePosition(nextPosition);
      bossPosNum = Random.Range(0, bossPosVol);
      bossActNum = Random.Range(0, bossActVol);
      Debug.Log("Next Action:" + bossActNum);
      Debug.Log("Next Position:" + bossPosNum);
      nextPosition = bossPositions[bossPosNum].transform.position;
      bossAct = BossAct.Atatck;
    }
  }

  private void Attack()
  {
    switch (bossActNum)
    {
      case 0:
        iBoss.Attack0();
        break;
      default:
        FinishAct();
        break;
    }

  }

  public void FinishAct()
  {
    bossAct = BossAct.Move;
  }
}
