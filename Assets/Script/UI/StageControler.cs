using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageControler : MonoBehaviour
{
    public GManager gManager;
    public int stage;

    [SerializeField] private AudioClip missBGM;
    private BGM bgm;
    private Player player;
    private float goStartSceneTime_dead = 4f;
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
        GManager.instance.InitializeStage();
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
                GManager.instance.PlaySE(missBGM);
                isDead = true;
                bgm.Stop();
                Invoke("MissStage", goStartSceneTime_dead);
            }
        }

        else if (GManager.instance.isClear)
        {
            if (!isClear)
            {
                isClear = true;
                bgm.Stop();
                Invoke("ClearStage", goStartSceneTime_clear);
            }
        }
    }

    private void MissStage()
    {
        GManager.instance.continueNum -= 1;
        GManager.instance.playerHP = GManager.instance.defaultHP;
        if (GManager.instance.continueNum >= 0)
        {
            SceneManager.LoadScene("Start");
        }
        else
        {
            GManager.instance.InitializeGame();
            SceneManager.LoadScene("GameOver");
        }
    }

    private void ClearStage()
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
