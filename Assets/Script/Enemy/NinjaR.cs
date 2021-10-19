using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaR : MonoBehaviour
{
  [Header("飛び道具")] public GameObject damageObj;
  [Header("攻撃間隔")] public float interval;
  public bool isRight = false;

  private Rigidbody2D rb = null;
  private SpriteRenderer sr = null;
  private Animator anim;
  private DamageObject damageObject;
  private Vector2 velocity = Vector2.zero;
  private float attackTimer = 0f;
  private bool isAttack = false;
  // Start is called before the first frame update
  void Start()
  {
    rb = GetComponent<Rigidbody2D>();
    sr = GetComponent<SpriteRenderer>();
    //anim = GetComponent<Animator>();
  }

  // Update is called once per frame
  void Update()
  {
    if (sr.isVisible)
    {
      if (!isAttack)
      {
        Attack();
        isAttack = true;
        attackTimer = 0.0f;
      }
      //攻撃インターバル
      else
      {
        attackTimer += Time.deltaTime;
        if (attackTimer > interval)
        {
          isAttack = false;
        }
      }
    }
  }

  void FixedUpdate()
  {
  }

  private void Attack()
  {
    GameObject g = Instantiate(damageObj);
    g.transform.SetParent(transform);
    g.transform.position = damageObj.transform.position;
    g.transform.localScale = damageObj.transform.localScale;
    g.SetActive(true);
    damageObject = g.GetComponent<DamageObject>();
    //fire.left1right_1=transform.localScale.x;
    g.transform.parent = null;
    //anim.SetTrigger("attack");
  }

}
