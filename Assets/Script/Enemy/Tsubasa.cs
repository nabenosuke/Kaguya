using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tsubasa : MonoBehaviour, IBoss
{
    //Bossから攻撃呼び出されるのは一回にして、こっちの数値を直接いじってもらう
    public float attackTime = 10;

    [Header("浮遊間隔")] [SerializeField] private float movePeriod = 2;
    [Header("浮遊幅")] [SerializeField] private float moveWide = 0.1f;
    #region //攻撃
    [SerializeField] private GameObject Enemys;
    [SerializeField] private GameObject bakeW;
    [Header("Attack0の白バケの幅")] [SerializeField] private float interval0 = 2.56f;
    [Header("Attack0の白バケの上下の出現距離")] [SerializeField] private float buffer0 = 0;
    [SerializeField] private GameObject bakeG;
    [Header("Attack1の緑バケの位置(0-1)")] [SerializeField] private float pos1 = 0.3f;
    [Header("Attack1の緑バケの出現間隔")] [SerializeField] private float intervalPop1 = 0.5f;
    [Header("Attack1の緑バケの出現回数")] [SerializeField] private int popNum1 = 7;
    [SerializeField] private GameObject bakeB;
    [Header("Attack2の黒バケの出現回数")] [SerializeField] private int bakeNum2 = 5;
    [Header("Attack2の黒バケが拡散する時間")] [SerializeField] private float spreadTime2 = 6f;
    private float bakeRad2;
    private float intervalPop2;

    #endregion

    private GameObject stageArea;


    private Rigidbody2D rb;
    private Boss boss;
    private Vector2 stageLowerLeft;
    private Vector2 stageUpperRight;
    private int popCount;
    //攻撃時間
    private float moveTimer = 0f;
    private float x = 0f;
    private float y = 0f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boss = GetComponent<Boss>();
        stageArea = GameObject.Find("StageArea");
        if (bakeW == null || bakeG == null || bakeB == null)
        {
            bakeW = transform.Find("BakeW").gameObject;
            bakeG = transform.Find("BakeG").gameObject;
            bakeB = transform.Find("BakeB").gameObject;
        }
        bakeRad2 = bakeB.GetComponent<MoveTurning>().radius;
        intervalPop2 = bakeB.GetComponent<MoveTurning>().period;
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

    }

    void IBoss.SetAttack(int id)
    {
        Invoke("FinishAttack", attackTime);
        switch (id)
        {
            case 0:
                Attack0();
                break;
            case 1:
                Attack1();
                break;
            case 2:
                Attack2();
                break;
            default:
                FinishAttack();
                break;
        }
    }

    void FinishAttack()
    {
        boss.FinishAct();
    }
    void Attack0()
    { //左右から白バケ
        float x = stageUpperRight.x;
        for (float y = stageLowerLeft.y + buffer0; y < stageUpperRight.y - buffer0; y = y + interval0 * 2)
        {
            GameObject g = Instantiate(bakeW);
            g.transform.SetParent(Enemys.transform, true);
            g.transform.position = new Vector2(x, y);
            g.SetActive(true);
        }

        x = stageLowerLeft.x;
        for (float y = stageLowerLeft.y + buffer0 + interval0; y < stageUpperRight.y - buffer0; y = y + interval0 * 2)
        {
            GameObject g = Instantiate(bakeW);
            g.transform.SetParent(Enemys.transform, true);
            g.transform.position = new Vector2(x, y);
            g.GetComponent<Enemy>().isRight = true;
            g.SetActive(true);
        }
    }

    //左右から緑バケ
    void Attack1()
    {
        StartCoroutine(CoroutineAttack1());
    }
    IEnumerator CoroutineAttack1()
    {
        for (int i = 0; i < popNum1; i++)
        {
            yield return new WaitForSeconds(intervalPop1);
            PopBakeG();
        }
    }
    void PopBakeG()
    {
        x = stageUpperRight.x;
        GameObject g1 = Instantiate(bakeG);
        g1.transform.SetParent(Enemys.transform, true);
        y = (stageUpperRight.y - stageLowerLeft.y) * pos1 + stageLowerLeft.y;
        g1.transform.position = new Vector2(x, y);
        g1.SetActive(true);

        x = stageLowerLeft.x;
        GameObject g2 = Instantiate(bakeG);
        g2.transform.SetParent(Enemys.transform, true);
        y = (stageLowerLeft.y - stageUpperRight.y) * pos1 + stageUpperRight.y;
        g2.transform.position = new Vector2(x, y);
        g2.GetComponent<Enemy>().isRight = true;
        g2.SetActive(true);

        popCount++;
    }

    void Attack2()
    {
        StartCoroutine(CoroutineAttack2());

    }

    IEnumerator CoroutineAttack2()
    {
        for (int i = 0; i < bakeNum2; i++)
        {
            GameObject g = Instantiate(bakeB);
            g.transform.SetParent(Enemys.transform, true);
            x = transform.position.x;
            y = transform.position.y + bakeRad2;
            g.transform.position = new Vector3(x, y);
            g.SetActive(true);
            g.GetComponent<MoveTurning>().spreadTime = spreadTime2 - i * intervalPop2 / bakeNum2;
            yield return new WaitForSeconds(intervalPop2 / bakeNum2);

        }
    }

    void IBoss.Defeated()
    {
        Destroy(Enemys);
    }
}
