using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackObject : MonoBehaviour
{
    [Header("スピード")] public float speed = 3.0f;
    [Header("最大飛距離")] public float maxDistance = 100f;
    [Header("向き")] private float left_1right1 = -1;
    [Header("攻撃力")] public int attack = 1;


    private Rigidbody2D rb;
    private Vector3 defaultPos;
    private float direction;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.Log("Fireの設定が足りません");
            Destroy(this.gameObject);
        }
        defaultPos = transform.position;
        if (transform.localScale.x < 0)
        {
            left_1right1 = 1;
            GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(transform.localEulerAngles.z * Mathf.Deg2Rad) * left_1right1, -Mathf.Sin(transform.localEulerAngles.z * Mathf.Deg2Rad)) * speed;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, -transform.localEulerAngles.z));
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(transform.localEulerAngles.z * Mathf.Deg2Rad) * left_1right1, -Mathf.Sin(transform.localEulerAngles.z * Mathf.Deg2Rad)) * speed;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float d = Vector3.Distance(transform.position, defaultPos);
        //最大移動距離を超えている
        if (d > maxDistance)
        {
            Destroy(this.gameObject);
        }
        /*
        else
        {
            rb.MovePosition(transform.position += Vector3.left * speed * Time.deltaTime * left1right_1);
        }
        */
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Ground")
        {
            Destroy(this.gameObject);
        }
        if (collider.tag == "Player")
        {
            var damage = collider.gameObject.GetComponent<IDamage>();
            damage.Damage(attack);
            Destroy(this.gameObject);
        }
    }
}
