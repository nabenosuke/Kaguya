using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageControler : MonoBehaviour
{
    public GManager gManager;
    public int stage;

    private BGM bgm;
    private Player player;
    private float deadTime = 0f;
    private float clearTime = 0f;
    private float goStartSceneTime_dead = 3f;
    private float goStartSceneTime_clear = 6f;
    private bool isDead = false;
    private bool isClear = false;

    //始めてステージを起動した場合、スタート位置を設定
    void Awake()
    {

    }


    // Start is called before the first frame update
    void Start()
    {
        if (GManager.instance.InitializeCheck())
        {
            GManager.instance.startPos = GameObject.Find("StartPos").transform.position;
        }
        GManager.instance.Initialize();
        GManager.instance.stageNum = stage;
        bgm = GameObject.Find("BGM").GetComponent<BGM>();
        player = GameObject.Find("Player").GetComponentInChildren<Player>();
        player.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        if (GManager.instance.playerHP <= 0)
        {
            if (!isDead)
            {
                isDead = true;
                bgm.Stop();
            }
            deadTime += Time.deltaTime;
            if (deadTime > goStartSceneTime_dead)
            {
                GManager.instance.continueNum -= 1;
                GManager.instance.playerHP = GManager.instance.defaultHP;
                SceneManager.LoadScene("Start");
            }
        }

        else if (GManager.instance.isClear)
        {
            if (!isClear)
            {
                isClear = true;
                bgm.Stop();
            }
            clearTime += Time.deltaTime;
            if (clearTime > goStartSceneTime_clear)
            {
                if (GManager.instance.stageNum < GManager.instance.maxStageNum)
                {
                    GManager.instance.stageNum += 1;
                    SceneManager.LoadScene("Start");
                }
                else
                {
                    GManager.instance.stageNum = 11;
                    SceneManager.LoadScene("Start");
                }
            }
        }
    }

    public void Miss()
    {
    }


}
