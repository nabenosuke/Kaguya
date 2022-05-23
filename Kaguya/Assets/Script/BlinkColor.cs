using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkColor : MonoBehaviour
{
    // Start is called before the first frame update
    private SpriteRenderer sr = null;
    private float blinkTimer = 0f;
    private Color color = new Color(1, 1, 1);
    private Color colorChange;
    [SerializeField] private float blinkPeriod = 0.3f;
    [SerializeField] private float colorEdge;
    [SerializeField] private bool red;
    [SerializeField] private bool blue;
    [SerializeField] private bool green;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        colorChange = new Color(red ? 0 : 1, blue ? 0 : 1, green ? 0 : 1, 0) * colorEdge / blinkPeriod * 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (blinkTimer > blinkPeriod)
        {
            color = new Color(1, 1, 1, 1);
            blinkTimer = 0f;
        }
        //明滅　消えているとき
        else if (blinkTimer > blinkPeriod / 2)
        {
            color += colorChange * Time.deltaTime;
        }
        //明滅　ついているとき
        else
        {
            color -= colorChange * Time.deltaTime;
        }
        sr.color = color;
        blinkTimer += Time.deltaTime;
    }
}
