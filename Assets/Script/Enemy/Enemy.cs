using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamage
{
  [Header("攻撃力")] public int attack = 1;
  [Header("体力")] public int hp = 1;
  public bool isRight;

  //private float gravity = 0f;
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
    //this.gravity = GManager.instance.gravity;
  }

  // Update is called once per frame
  void Update()
  {

  }


  //被弾
  void IDamage.damage(int damage)
  {
    hp -= damage;

    if (hp <= 0)
    {
      isDead = true;
      Destroy(gameObject);
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
