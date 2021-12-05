using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tsubasa : MonoBehaviour, IBoss
{
    //Bossから攻撃呼び出されるのは一回にして、こっちの数値を直接いじってもらう
    public float attackTime = 10;

    [Header("浮遊間隔")] [SerializeField] private float movePeriod = 2;
    [Header("浮遊幅")] [SerializeField] private float moveWide = 0.1f;
    [Header("Attack1の白バケの幅")] [SerializeField] private float interval1 = 2;
    [Header("Attack1の白バケの上下の出現距離")] [SerializeField] private float buffer1 = 3;
    [Header("Attack2の緑バケの位置(0-1)")] [SerializeField] private float pos2 = 0.3f;
    [Header("Attack2の緑バケの出現間隔")] [SerializeField] private float interval2_pop = 0.5f;
    [Header("Attack2の緑バケの出現回数")] [SerializeField] private int pop_num2 = 7;

    private GameObject stageArea;
    private GameObject bakeW;
    private GameObject bakeG;
    private Rigidbody2D rb;
    private Boss boss;
    private Vector2 stageLowerLeft;
    private Vector2 stageUpperRight;
    private int attackID;
    //攻撃時間
    private float attackTimer = 0f;
    private float moveTimer = 0f;
    private float x = 0f;
    private float y = 0f;
    private float nextPopTime2 = 0f;
    //攻撃中か
    private bool isAttack = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boss = GetComponent<Boss>();
        stageArea = GameObject.Find("StageArea");
        bakeW = transform.Find("BakeW").gameObject;
        bakeG = transform.Find("BakeG").gameObject;
        //ステージの広さ取得
        stageLowerLeft = new Vector2(stageArea.transform.position.x - stageArea.transform.localScale.x / 2, stageArea.transform.position.y - stageArea.transform.localScale.y / 2);
        stageUpperRight = new Vector2(stageArea.transform.position.x + stageArea.transform.localScale.x / 2, stageArea.transform.position.y + stageArea.transform.localScale.y / 2);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //浮遊動作は移動以外に限定するべき?
        moveTimer += Time.deltaTime;
        if (moveTimer > movePeriod)
        {
            moveTimer = 0;
        }

        rb.velocity = new Vector2(0, moveWide * 2 * Mathf.PI / movePeriod * Mathf.Cos(moveTimer * 2 * Mathf.PI / movePeriod));
    }

    void Update()
    {
        if (isAttack)
        {
            switch (attackID)
            {
                case 0:
                    Attack0();
                    break;
                case 1:
                    Attack1();
                    break;
                default:
                    boss.FinishAct();
                    break;
            }
        }
    }

    void IBoss.SetAttack(int id)
    {
        isAttack = true;
        attackID = id;
    }

    void FinishAttack()
    {
        boss.FinishAct();
        isAttack = false;
        attackTimer = 0f;
    }
    void Attack0()
    { //左右から白バケ
        if (attackTimer == 0)
        {
            float x = stageUpperRight.x;
            for (float y = stageLowerLeft.y + buffer1; y < stageUpperRight.y - buffer1; y = y + interval1 * 2)
            {
                GameObject g = Instantiate(bakeW);
                g.transform.position = new Vector2(x, y);
                g.SetActive(true);
            }

            x = stageLowerLeft.x;
            for (float y = stageLowerLeft.y + buffer1 + interval1; y < stageUpperRight.y - buffer1; y = y + interval1 * 2)
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
            FinishAttack();
        }
    }


    void Attack1()
    { //左右から緑バケ
        if (attackTimer == 0)
        {
            nextPopTime2 = 0f;
        }
        if (nextPopTime2 < interval2_pop * pop_num2)
        {
            if (attackTimer > nextPopTime2)
            {
                x = stageUpperRight.x;
                GameObject g1 = Instantiate(bakeG);
                y = (stageUpperRight.y - stageLowerLeft.y) * pos2 + stageLowerLeft.y;
                g1.transform.position = new Vector2(x, y);
                g1.SetActive(true);

                x = stageLowerLeft.x;
                GameObject g2 = Instantiate(bakeG);
                y = (stageLowerLeft.y - stageUpperRight.y) * pos2 + stageUpperRight.y;
                g2.transform.position = new Vector2(x, y);
                g2.GetComponent<Enemy>().isRight = true;
                g2.SetActive(true);

                nextPopTime2 += interval2_pop;
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
