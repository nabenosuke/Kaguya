using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamage
{
    #region//パブリック変数
    [Header("接地判定")] public GroundCheck ground; //接地判定
    [Header("飛び道具のリスト")] public GameObject[] attackObjs;
    [Header("飛び道具")] private GameObject attackObj;
    #endregion

    #region //プライベート変数
    [SerializeField] private AudioClip jumpSE = null;
    [SerializeField] private AudioClip damageSE = null;
    private Animator anim = null;
    private Rigidbody2D rb = null;
    private CapsuleCollider2D capcol = null;
    private SpriteRenderer sr = null;
    private GameObject characterImage;
    [Header("キャラの画像リスト")] [SerializeField] private GameObject[] characterImages;
    private AttackObject attackObject = null;
    private int characterID = 1;
    private int number;//確立器
    [Header("移動速度")] [SerializeField] private float defaultSpeed;
    private float speed = 0.0f;
    [Header("移動モーション速度")] [SerializeField] private float defaultRunAnimSpeed;
    private float runAnimSpeed = 0.0f;
    [Header("ジャンプした瞬間の速度")] [SerializeField] private float jumpPower;
    [Header("攻撃間隔")] [SerializeField] private float attackInterval;
    private float dashTime = 0.0f;
    private float beforeKey = 0.0f;
    private float attackTimer = 0.0f;
    private float blinkTimer = 0.0f;
    private float damageTimer = 0.0f;
    private float jumpTimer = 0.0f;
    [SerializeField] private float jumpTime = 0.1f;
    private float gravity;
    private float fallSpeed;
    private bool isRun = false;
    private bool isGround = false;
    private bool wasGround = false;
    [SerializeField] private bool isGetUp = false;
    private bool isGetDownUp = false;
    private bool isJump = false;
    private bool isFly = false;
    private bool isJumpEnd = false;
    private bool isAttack = false;
    private bool isDamage = false;
    private bool isDead = false;


    //ほのか
    private float attackScale = 1.5f;
    private int attackDamageScale = 2;
    private int criticalRate = 5;
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
        SetCharacter(GManager.instance.characterID);
        //anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        capcol = GetComponent<CapsuleCollider2D>();
    }

    public void Initialize()
    {
        transform.position = GManager.instance.startPos;
        this.gravity = GManager.instance.gravity;
        this.fallSpeed = GManager.instance.fallSpeed;
    }

    void Update()
    {
        if (!isDead && !GManager.instance.isClear)
        {
            //攻撃
            if (Input.GetButtonDown("Fire1"))
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
                if (attackTimer > attackInterval)
                {
                    isAttack = false;
                }
            }

            //スピンタイム
            if (isSpin)
            {
                spinTimer += Time.deltaTime;
                //スピン解除
                if (spinTimer > spinTime || Input.GetButtonDown("Jump"))
                {
                    isSpin = false;
                    spinTimer = 0f;
                }
            }
            wasGround = isGround;
            isGround = ground.IsGround();
            //ジャンプ判定
            if (Input.GetButtonDown("Jump"))
            {
                isGetDownUp = true;
                isGetUp = true;
            }
            else if (Input.GetButtonUp("Jump"))
            {
                isGetDownUp = false;
                isGetUp = false;
            }

            //被弾時点滅
            else if (isDamage)
            {
                //明滅　ついている時に戻る
                if (blinkTimer > 0.2f)
                {
                    sr.enabled = true;
                    blinkTimer = 0.0f;
                }
                //明滅　消えているとき
                else if (blinkTimer > 0.1f)
                {
                    sr.enabled = false;
                }
                //明滅　ついているとき
                else
                {
                    sr.enabled = true;
                }

                //2秒たったら明滅終わり
                if (damageTimer > 2.0f)
                {
                    isDamage = false;
                    blinkTimer = 0f;
                    damageTimer = 0f;
                    sr.enabled = true;
                }
                else
                {
                    blinkTimer += Time.deltaTime;
                    damageTimer += Time.deltaTime;
                }
            }
        }
        else if (GManager.instance.isClear)
        {
            sr.enabled = true;
        }
        /*
        else if (GManager.instance.isClear)
        {
            isJump = true;
            isSpin = false;
        }
        */
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
            rb.velocity = new Vector2(0, rb.velocity.y - gravity * Time.fixedDeltaTime);
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
            //着地
            if (isFly)
            {
                jumpTimer = 0f;
                isJump = false;
                isJumpEnd = false;
                isFly = false;
            }
            //地上で上
            if (isGetDownUp)
            {
                isGetDownUp = false;
                ySpeed = jumpPower;
                if (!isJump)
                {
                    GManager.instance.PlaySE(jumpSE);
                    isJump = true;
                }
            }
            else
            {

            }

            //にこスピン解除
            spinTimer = 0f;
            isSpin = false;
            wasSpin = false;
        }
        else
        {
            isFly = true;
            //空中で上
            if (isGetUp && isJump)
            {

                if (jumpTimer < jumpTime && !isJumpEnd)
                {
                    jumpTimer += Time.fixedDeltaTime;
                    ySpeed = jumpPower;
                }
                else
                {
                    if (!isJumpEnd)
                    {
                        isJumpEnd = true;
                    }
                }
            }
            else
            {
                isJumpEnd = true;
            }

            //空中でジャンプ
            if (isGetDownUp)
            {
                isGetDownUp = false;
                //にこスピン開始
                if (characterID == 7)
                {
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


        }

        //にこスピン中なら落下速度減少
        if (isSpin)
        {
            return -spinSpeed;
        }

        if (ySpeed > -fallSpeed)
        {
            ySpeed -= gravity * Time.fixedDeltaTime;
        }
        else
        {
            ySpeed = -fallSpeed;
        }
        return ySpeed;
    }

    private void SetAnimation()
    {
        anim.SetFloat("XSpeed", rb.velocity.x * runAnimSpeed);
        anim.SetBool("ground", isGround);
        anim.SetBool("run", isRun);
        anim.SetBool("jump", isJump);
        if (characterID == 7)
        {
            anim.SetBool("spin", isSpin);
        }
    }

    public void Attack()
    {
        GameObject g = Instantiate(attackObj);
        //g.transform.SetParent(transform);
        g.transform.position = attackObj.transform.position;
        attackObject = g.GetComponent<AttackObject>();
        //ほのか
        if (characterID == 1)
        {
            number = Random.Range(0, 10);
            //クリティカル攻撃
            if (number < criticalRate)
            {
                attackObject.SetScale(attackScale, attackDamageScale);
            }
        }
        //g.transform.localScale = attackObj.transform.localScale;
        g.transform.localScale = new Vector3(g.transform.localScale.x * transform.localScale.x, g.transform.localScale.y, 1);
        g.SetActive(true);

        //g.transform.parent = null;
        anim.SetTrigger("attack");
    }

    //被弾
    void IDamage.damage(int damage)
    {
        if ((damage == 99 || !isDamage) && !isDead && !GManager.instance.isClear)
        {
            isDamage = true;
            GManager.instance.PlaySE(damageSE);
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

    //キャラ選択
    void SetCharacter(int charaID)
    {
        //キャラクターの画像変更
        foreach (var chara in characterImages)
        {
            chara.SetActive(false);
        }
        characterID = charaID;
        characterImage = characterImages[characterID - 1];
        sr = characterImage.GetComponent<SpriteRenderer>();
        anim = characterImage.GetComponent<Animator>();
        characterImage.SetActive(true);
        /*
        Debug.Log("charaID:" + charaID);
        Debug.Log("characterID:" + characterID);
        Debug.Log("GMcharaID:" + GManager.instance.characterID);
        */
        //キャラ毎のステータス設定
        speed = defaultSpeed;
        runAnimSpeed = defaultRunAnimSpeed;
        attackObj = attackObjs[GManager.instance.weponIDList[characterID - 1]];
        switch (charaID)
        {
            //ほのか 球
            case 1:
                break;
            //りん クナイ
            case 4:
                speed = defaultSpeed * 1.2f;
                runAnimSpeed = defaultRunAnimSpeed * 1.2f;
                break;
            //にこ 手裏剣
            case 7:
                break;
            default:
                break;
        }
        attackInterval = attackObj.GetComponent<AttackObject>().attackInterval;
    }
}
