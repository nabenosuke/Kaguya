using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, IDamage
{
    [Header("攻撃力")] public int attack = 1;
    [Header("体力")] public int hp = 1;
    public bool isRight;
    public bool isArmor;

    //private float gravity = 0f;
    [SerializeField] AudioClip hitSE;
    [SerializeField] AudioClip deathSE;
    [SerializeField] GameObject deathImage;
    #region//ボスのみ
    [SerializeField] Slider hpGauge;
    [SerializeField] bool isBoos;
    private int maxHP;
    #endregion
    private bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        if (isRight)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        if (hpGauge != null)
        {
            maxHP = hp;
        }

        //this.gravity = GManager.instance.gravity;
    }

    //被弾
    void IDamage.damage(int damage)
    {
        if (!isArmor)
        {
            hp -= damage;
            if (hpGauge != null)
            {
                hpGauge.value = (float)hp / (float)maxHP;
            }


            if (deathImage == null || hitSE == null || deathSE == null)
            {
                Debug.Log("enemyの何かが足りない");
            }
            else
            {
                //撃破
                if (hp <= 0)
                {
                    GManager.instance.PlaySE(deathSE);
                    if (isBoos) GetComponent<IBoss>().Defeated();
                    //撃破画像
                    GameObject g = Instantiate(deathImage);
                    g.transform.position = gameObject.transform.position;
                    g.SetActive(true);
                    isDead = true;
                    Destroy(gameObject);
                }
                else
                {
                    GManager.instance.PlaySE(hitSE);
                }
            }
        }
    }

    //攻撃
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            var damage = collider.gameObject.GetComponent<IDamage>();
            damage.damage(attack);
        }
    }



    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            var damage = collider.gameObject.GetComponent<IDamage>();
            damage.damage(attack);
        }
    }

    /*
      private void OnCollisionEnter2D(Collision2D Collision)
      {
        if (Collision.gameObject.tag == "Player")
        {
          var damage = Collision.gameObject.GetComponent<IDamage>();
          damage.damage(attack);
        }
      }



      private void OnCollisionStay2D(Collision2D Collision)
      {
        if (Collision.gameObject.tag == "Player")
        {
          var damage = Collision.gameObject.GetComponent<IDamage>();
          damage.damage(attack);
        }
      }
      */
}
