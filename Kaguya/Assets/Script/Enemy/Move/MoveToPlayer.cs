using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPlayer : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private AudioClip se;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public void SetPara(GameObject player, float delayTime)
    {
        if (se != null)
        {
            GManager.instance.PlaySE(se);
        }
        this.player = player;
        rb = GetComponent<Rigidbody2D>();
        Invoke("StartMove", delayTime);
    }
    private void StartMove()
    {
        transform.Rotate(0, 0, Mathf.Atan2(player.transform.position.y - transform.position.y, player.transform.position.x - transform.position.x) * Mathf.Rad2Deg - 90);
        rb.velocity = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y).normalized * speed;
    }
}
