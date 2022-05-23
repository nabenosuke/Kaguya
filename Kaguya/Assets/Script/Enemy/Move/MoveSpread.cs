using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ターゲットの周囲から、外側に向けて飛ぶ
public class MoveSpread : MonoBehaviour
{
    [SerializeField] private GameObject target;
    private Rigidbody2D rb;
    private float angle;
    [SerializeField] private float spreadTime;
    [SerializeField] private float radius;
    [SerializeField] private float speed;
    [SerializeField] private AudioClip se;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetPara(float angle)
    {
        if (se != null)
        {
            GManager.instance.PlaySE(se);
        }
        this.angle = angle;
        transform.Rotate(0, 0, angle);
        var x = target.transform.position.x + Mathf.Cos(angle * Mathf.Deg2Rad) * radius;
        var y = target.transform.position.y + Mathf.Sin(angle * Mathf.Deg2Rad) * radius;
        rb = GetComponent<Rigidbody2D>();
        transform.position = new Vector2(x, y);
        GetComponent<SpriteRenderer>().enabled = true;
        StartCoroutine(StartSpread());
    }

    IEnumerator StartSpread()
    {
        yield return new WaitForSeconds(spreadTime);
        rb.velocity = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)) * speed;
    }
}
