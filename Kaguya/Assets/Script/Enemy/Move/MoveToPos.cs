using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//指定した場所へ遅れて飛ぶ
public class MoveToPos : MonoBehaviour
{
    private Vector2 dist;
    private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private AudioClip se;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, dist) < 0.1f)
        {
            rb.MovePosition(dist);
            rb.velocity = new Vector2(0, 0);
        }
    }

    public void SetPara(Vector3 dist, float delayTime)
    {
        if (se != null)
        {
            GManager.instance.PlaySE(se);
        }
        this.dist = dist;
        rb = GetComponent<Rigidbody2D>();
        Invoke("StartMove", delayTime);
    }
    private void StartMove()
    {
        rb.velocity = new Vector2(dist.x - transform.position.x, dist.y - transform.position.y).normalized * speed;
    }
}
