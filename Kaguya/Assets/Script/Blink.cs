using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour
{
    private SpriteRenderer sr = null;
    private float blinkTimer = 0f;
    [SerializeField] private float blinkPeriod = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //明滅　ついている時に戻る
        if (blinkTimer > blinkPeriod)
        {
            sr.enabled = true;
            blinkTimer = 0.0f;
        }
        //明滅　消えているとき
        else if (blinkTimer > blinkPeriod / 2)
        {
            sr.enabled = false;
        }
        //明滅　ついているとき
        else
        {
            sr.enabled = true;
        }
        blinkTimer += Time.deltaTime;
    }
}
