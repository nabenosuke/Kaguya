using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkText : MonoBehaviour
{

    private Text text = null;
    private float blinkTimer = 0f;
    [SerializeField] private float blinkPeriod = 3f;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        //明滅　ついている時に戻る
        if (blinkTimer > blinkPeriod)
        {
            text.enabled = true;
            blinkTimer = 0.0f;
        }
        //明滅　消えているとき
        else if (blinkTimer > blinkPeriod / 2)
        {
            text.enabled = false;
        }
        //明滅　ついているとき
        else
        {
            text.enabled = true;
        }
        blinkTimer += Time.deltaTime;
    }
}
