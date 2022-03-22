using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackObject : MonoBehaviour
{
    //[Header("最大飛距離")] public float maxDistance = 100f;
    [Header("消失時間")] public float destroyTime = 0.4f;
    [Header("攻撃力")] public int attack = 1;
    [Header("攻撃間隔")] public float attackInterval = 0.3f;
    [Header("攻撃エフェクト")] [SerializeField] private GameObject attackEffect;
    [SerializeField] public bool isIgnoreEnemy;
    [SerializeField] public bool isIgnoreGround;
    [SerializeField] private int attackScale = 1;
    [SerializeField] private float scale = 1;


    [SerializeField] private AudioClip se;
    // Start is called before the first frame update
    void Start()
    {
        GManager.instance.PlaySE(se);
        attack = attack * attackScale;
        transform.localScale = new Vector3(transform.localScale.x * scale, transform.localScale.y * scale, transform.localScale.z);
        Invoke("DestroyThis", destroyTime);
    }

    // Update is called once per frame

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            if (!isIgnoreGround)
            {
                if (attackEffect != null)
                {
                    generateEffect();
                }
                Destroy(this.gameObject);
            }

        }
        else if (collision.tag == "Enemy")
        {
            var damage = collision.gameObject.GetComponent<IDamage>();
            if (damage != null)
            {
                damage.damage(attack);
                if (!isIgnoreEnemy)
                {
                    if (attackEffect != null)
                    {
                        generateEffect();
                    }
                    Destroy(this.gameObject);
                }
            }
        }
    }

    private void generateEffect()
    {
        GameObject g = Instantiate(attackEffect);
        g.GetComponent<AttackObject>().SetScale(scale, attackScale);
        g.transform.position = transform.position;
        g.SetActive(true);
    }

    private void DestroyThis()
    {
        Destroy(this.gameObject);
    }

    public void SetScale(float s, int aS)
    {
        scale = s;
        attackScale = aS;
    }
}
