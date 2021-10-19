using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamage
{
  #region//パブリック変数
  [Header("移動速度")] public float speed;
  [Header("移動モーション速度")] public float runAnimSpeed;
  [Header("ジャンプした瞬間の速度")] public float jumpPower;
  [Header("攻撃間隔")] public float interval;
  [Header("接地判定")] public GroundCheck ground; //接地判定
  [Header("飛び道具")] public GameObject attackObj;
  #endregion

  #region //プライベート変数
  private Animator anim = null;
  private Rigidbody2D rb = null;
  private CapsuleCollider2D capcol = null;
  private SpriteRenderer sr = null;
  private AttackObject attackObject = null;
  private float dashTime = 0.0f;
  private float beforeKey = 0.0f;
  private float attackTimer = 0.0f;
  private float blinkTime = 0.0f;
  private float damageTime = 0.0f;
  private float gravity;
  private bool isRun = false;
  private bool isGround = false;
  private bool isGetDownUp = false;
  private bool isJump = false;
  private bool isAttack = false;
  private bool isDamage = false;
  private bool isDead = false;

  //にこ
  private float spinSpeed = 2.0f;
  private float spinTimer = 0f;
  private float spinTime = 1.3f;
  private bool isSpin = false;
  private bool wasSpin = false;
  #endregion
  // Start is called before the first frame update
  void Start()
  {
    anim = GetComponent<Animator>();
    rb = GetComponent<Rigidbody2D>();
    capcol = GetComponent<CapsuleCollider2D>();
    sr = GetComponent<SpriteRenderer>();
  }

  public void Initialize()
  {
    transform.position = GManager.instance.startPos;
    this.gravity = GManager.instance.gravity;
  }

  void Update()
  {
    if (!isDead && !GManager.instance.isClear)
    {
      //攻撃
      if (Input.GetKeyDown(KeyCode.Z))
      {
        if (!isAttack)
        {
          Attack();
          isAttack = true;
          attackTimer = 0.0f;
        }
      }
      //攻撃インターバル
      if (isAttack)
      {
        attackTimer += Time.deltaTime;
        if (attackTimer > interval)
        {
          isAttack = false;
        }
      }

      //スピンタイム
      if (isSpin)
      {
        spinTimer += Time.deltaTime;
        if (spinTimer > spinTime)
        {
          isSpin = false;
          spinTimer = 0f;
        }
      }

      isGround = ground.IsGround();
      //ジャンプ判定
      if (Input.GetKeyDown(KeyCode.UpArrow))
      {
        isGetDownUp = true;
      }

      //被弾時点滅
      else if (isDamage)
      {
        //明滅　ついている時に戻る
        if (blinkTime > 0.2f)
        {
          sr.enabled = true;
          blinkTime = 0.0f;
        }
        //明滅　消えているとき
        else if (blinkTime > 0.1f)
        {
          sr.enabled = false;
        }
        //明滅　ついているとき
        else
        {
          sr.enabled = true;
        }

        //2秒たったら明滅終わり
        if (damageTime > 2.0f)
        {
          isDamage = false;
          blinkTime = 0f;
          damageTime = 0f;
          sr.enabled = true;
        }
        else
        {
          blinkTime += Time.deltaTime;
          damageTime += Time.deltaTime;
        }
      }
    }
    else if (GManager.instance.isClear)
    {
      sr.enabled = true;
    }
    else if (GManager.instance.isClear)
    {
      isJump = true;
      isSpin = false;
    }
  }
  void FixedUpdate()
  {
    if (!isDead && !GManager.instance.isClear)
    {
      //各座標値の速度を求める
      float xSpeed = GetXSpeed();
      float ySpeed = GetYSpeed();
      SetAnimation();

      rb.velocity = new Vector2(xSpeed, ySpeed);
    }
    else if (GManager.instance.isClear)
    {
      rb.velocity = Vector2.zero;
    }
    else
    {
      rb.velocity = new Vector2(0, rb.velocity.y + Physics.gravity.y * Time.fixedDeltaTime);
    }
  }

  private float GetXSpeed()
  {
    //キー入力で行動
    float horizontalKey = Input.GetAxis("Horizontal");
    float xSpeed = 0.0f;

    if (horizontalKey > 0)
    {
      transform.localScale = new Vector3(-1, 1, 1);
      dashTime += Time.fixedDeltaTime;
      xSpeed = speed;
      isRun = true;

    }
    else if (horizontalKey < 0)
    {
      transform.localScale = new Vector3(1, 1, 1);
      dashTime += Time.fixedDeltaTime;
      xSpeed = -speed;
      isRun = true;

    }
    else
    {
      dashTime += 0.0f;
      xSpeed = 0.0f;
      isRun = false;

    }

    //前回の入力からダッシュの反転を判断して速度を変える
    if (horizontalKey > 0 && beforeKey < 0)
    {
      dashTime = 0.0f;
    }
    else if (horizontalKey < 0 && beforeKey > 0)
    {
      dashTime = 0.0f;
    }
    beforeKey = horizontalKey;

    //アニメーションカーブに適用
    //xSpeed *= dashCurve.Evaluate(dashTime);

    return xSpeed;
  }

  //現在の速度に重力を適用し、ジャンプ力を付ける
  private float GetYSpeed()
  {
    float ySpeed = rb.velocity.y;
    if (isGround)
    {
      if (isGetDownUp)
      {
        isGetDownUp = false;
        if (!isJump)
        {
          ySpeed += jumpPower;
          isJump = true;
        }
      }
      else
      {
        isJump = false;
      }

      //にこスピン
      isSpin = false;
      wasSpin = false;
    }

    //にこスピン
    else
    {
      if (isGetDownUp)
      {
        isGetDownUp = false;
        if (!wasSpin)
        {
          if (!isSpin)
          {
            isSpin = true;
            wasSpin = true;
          }
        }
      }
    }
    if (isSpin)
    {
      return -spinSpeed;
    }

    ySpeed -= gravity * Time.fixedDeltaTime;
    return ySpeed;
  }

  private void SetAnimation()
  {
    anim.SetFloat("XSpeed", rb.velocity.x / runAnimSpeed);
    anim.SetBool("ground", isGround);
    anim.SetBool("run", isRun);
    anim.SetBool("jump", isJump);
    anim.SetBool("spin", isSpin);
  }

  public void Attack()
  {
    GameObject g = Instantiate(attackObj);
    g.transform.SetParent(transform);
    g.transform.position = attackObj.transform.position;
    g.transform.localScale = attackObj.transform.localScale;
    g.SetActive(true);
    attackObject = g.GetComponent<AttackObject>();
    //fire.left1right_1=transform.localScale.x;
    g.transform.parent = null;
    anim.SetTrigger("attack");
  }

  //被弾
  void IDamage.damage(int damage)
  {
    if (!isDamage && !isDead && !GManager.instance.isClear)
    {
      isDamage = true;
      if (GManager.instance.playerHP > damage)
      {
        GManager.instance.playerHP -= damage;
        anim.SetTrigger("damage");
      }
      //死亡
      else
      {
        GManager.instance.playerHP = 0;
        anim.SetTrigger("dead");
        isDead = true;
        Debug.Log("死");
      }
    }
  }
}
