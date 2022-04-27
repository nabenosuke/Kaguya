using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageObject : MonoBehaviour
{
    [Header("攻撃力")] public int attack = 1;
    public bool isHit = false;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            var damage = collider.gameObject.GetComponent<IDamage>();
            damage.Damage(attack);
            isHit = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            var damage = collider.gameObject.GetComponent<IDamage>();
            damage.Damage(attack);
        }
    }
}
