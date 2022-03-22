using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    // Start is called before the first frame update
    private Text scoreText = null;
    private int oldscore = 0;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<Text>();
        if (GManager.instance != null)
        {
            scoreText.text = GManager.instance.score.ToString("D7");
        }
        else
        {
            Debug.Log("ゲームマネージャー置き忘れてるよ！");
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (oldscore != GManager.instance.score)
        {
            scoreText.text = GManager.instance.score.ToString("D7");
            oldscore = GManager.instance.score;
        }
    }
}
