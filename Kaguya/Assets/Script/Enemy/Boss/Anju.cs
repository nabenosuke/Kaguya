using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anju : MonoBehaviour, IBoss
{
    //Bossから攻撃呼び出されるのは一回にして、こっちの数値を直接いじってもらう
    public float attackTime = 10;

    [SerializeField] private GameObject player;
    private Random rand = new Random();
    private Animator anim = null;
    [Header("浮遊間隔")][SerializeField] private float movePeriod = 2;
    [Header("浮遊幅")][SerializeField] private float moveWide = 0.1f;
    #region //攻撃
    [SerializeField] private GameObject Enemys;
    //Attack0
    [SerializeField] private GameObject[] cards0;
    private int[] cardList0 = new int[5];
    private int cardNum0;
    //Attack0のカード間の角度
    private float cardRad0 = 22.5f;
    //Attack0のカードを設置する時間
    private float attackTime0 = 1f;

    //Attack1
    [SerializeField] private GameObject card;
    private int[] cardList1;
    //Attack1のカード枚数
    private int cardNum1 = 8;
    //Attack1のカードの半径
    private float cardRadius1 = 9;
    //Attack1のカードの角度
    private float cardRadian1;
    //カードを広げる時間
    private float attackTime1 = 1f;
    //カードを広げてから収束させる時間
    private float attackBufTime1 = 2f;

    //Attack2
    [SerializeField] private GameObject[] cards2;
    [SerializeField] private GameObject joker;
    private int[] cardList2 = new int[4];
    //Attack2のカードの半径
    private float cardRadius2 = 2;
    //Attack2のカードを広げる角度
    private float cardRadian2 = 0.4f;
    #endregion

    private GameObject stageArea;

    private Rigidbody2D rb;
    private Boss boss;
    private Vector2 stageLowerLeft;
    private Vector2 stageUpperRight;
    private int popCount;
    //攻撃時間
    private float moveTimer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boss = GetComponent<Boss>();
        anim = GetComponent<Animator>();

        cardNum0 = cardList0.Length;
        for (int i = 0; i < cardNum0; i++)
        {
            cardList0[i] = i;
        }
        System.Array.Resize(ref cardList1, cardNum1);
        for (int i = 0; i < cardNum1; i++)
        {
            cardList1[i] = i;
        }
        cardRadian1 = 2 * Mathf.PI / cardNum1;
        for (int i = 0; i < 4; i++)
        {
            cardList2[i] = i;
        }


        stageArea = GameObject.Find("StageArea");
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
    }

    void Update()
    {

    }

    Vector2 IBoss.StandBy()
    {
        return new Vector2(moveWide * 2 * Mathf.PI / movePeriod * Mathf.Cos(moveTimer * 2 * Mathf.PI / movePeriod), 0);
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
    //フルハウス攻撃
    void Attack0()
    {
        StartCoroutine(CoroutineAttack0());
    }

    IEnumerator CoroutineAttack0()
    {
        ShuffleList(ref cardList0);

        Vector3 direction = player.transform.position - transform.position;
        for (int i = 0; i < cardNum0; i++)
        {
            GameObject g = Instantiate(cards0[cardList0[i]]);
            g.transform.SetParent(Enemys.transform, true);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            angle += (i - (cardNum0 - 1) / 2) * cardRad0;
            g.SetActive(true);
            g.GetComponent<MoveSpread>().SetPara(angle);
            yield return new WaitForSeconds(attackTime0 / cardNum0);

        }
    }

    //カードシャッフル
    void Attack1()
    {
        StartCoroutine(CoroutineAttack1());
    }
    IEnumerator CoroutineAttack1()
    {
        var playerPos = player.transform.position;
        ShuffleList(ref cardList1);
        for (int i = 0; i < cardNum1; i++)
        {
            GameObject g = Instantiate(card);
            g.transform.SetParent(Enemys.transform, true);
            float angle = cardRadian1 * i;
            g.transform.position = new Vector2(playerPos.x + Mathf.Sin(angle) * cardRadius1, playerPos.y + Mathf.Cos(angle) * cardRadius1);
            g.SetActive(true);
            g.GetComponent<MoveToPos>().SetPara(playerPos, attackTime1 - attackTime1 / cardNum1 * i + attackBufTime1 + attackTime1 / cardNum1 * cardList1[i]);
            yield return new WaitForSeconds(attackTime1 / cardNum1);

        }
    }

    //ババ抜き
    void Attack2()
    {
        StartCoroutine(CoroutineAttack2());
    }

    IEnumerator CoroutineAttack2()
    {
        for (int h = 0; h < 2; h++)
        {
            ShuffleList(ref cardList2);
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    GameObject g = Instantiate(cards2[cardList2[i * 2 + j]]);
                    g.transform.SetParent(Enemys.transform, true);
                    g.transform.position = new Vector3(transform.position.x + Mathf.Sin(cardRadian2 - cardRadian2 * 2 * j) * cardRadius2, transform.position.y + Mathf.Cos(cardRadian2 - cardRadian2 * 2 * j) * cardRadius2);
                    g.SetActive(true);
                    g.GetComponent<MoveToPlayer>().SetPara(player, 0.5f + 0.2f * (1 - j));
                    yield return new WaitForSeconds(0.2f);
                }
                yield return new WaitForSeconds(0.8f);
            }
        }
        GameObject g2 = Instantiate(joker);
        g2.transform.SetParent(Enemys.transform, true);
        g2.transform.position = new Vector3(transform.position.x, transform.position.y + 2);
        g2.SetActive(true);
        g2.GetComponent<MoveToPlayer>().SetPara(player, 0.5f);
    }
    void IBoss.Defeated()
    {
        Destroy(Enemys);
    }

    void ShuffleList(ref int[] list)
    {
        for (int i = 0; i < list.Length; i++)
        {
            int j = Random.Range(0, list.Length);
            int tmp = list[i];
            list[i] = list[j];
            list[j] = tmp;
        }
    }
}
