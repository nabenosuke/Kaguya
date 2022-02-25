using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GManager : MonoBehaviour
{
    public static GManager instance = null;
    [Header("スタート地点")] public Vector3 startPos = Vector3.zero;
    [Header("キャラクターID")] public int characterID = 7;
    [Header("キャラクターネーム")] public string characterName = "NICO";
    [Header("現在のステージ")] public int stageNum;
    [Header("現在の残機")] public int continueNum;
    [Header("デフォルト残機")] public int defaultContinueNum = 3;
    [Header("現在のHP")] public int playerHP;
    [Header("デフォルトHP")] public int defaultHP = 3;
    [Header("輝き数")] public int kagayaki;
    [Header("ボス戦前のステージ")] public int maxStageNum = 2;
    [Header("コンテニュー番号")] public int continuePointNum = 0;
    [Header("スコア")] public int score = 0;
    public int[] bossStage = new int[] { 11, 21, 31 };
    [Header("重力")] public float gravity = 20f;
    [Header("最速落下速度")] public float fallSpeed = 20f;
    public bool isDead;
    public bool isClear;

    private AudioSource audioSource = null;
    //このステージが一度初期化されたか
    //[SerializeField] private bool[] isStageInitialize;
    private string[] characterNameList = new string[] { "HONOKA", "KOTORI", "UMI", "RIN", "HANAYO", "MAKI", "NIKO", "NOZOMI", "ELI" };
    //各キャラの武器。クナイ、手裏剣、爆弾
    public int[] weponIDList = new int[] { 2, 0, 0, 0, 0, 0, 1, 0, 0, 0 };


    private void Awake()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        //インスタンス作成
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        playerHP = defaultHP;
        //isStageInitialize = new bool[33];
        audioSource = GetComponent<AudioSource>();
        /*
            stageControler = GameObject.Find("StageController").GetComponent<StageControler>();
            if (stageControler != null)
            {
              stageControler.Initialize();
            }
            else
            {
              Debug.Log("非ステージ");
            }
            */
        //フレームレートを60に
        Application.targetFrameRate = 60;
    }


    //ステージ初期化チェック。未初期化なら真を返す
    /*
    public bool InitializeCheck()
    {
        if (isStageInitialize[stageNum])
        {
            return false;
        }
        else
        {
            isStageInitialize[stageNum] = true;
            return true;
        }
    }
    */

    //ステージ開始時処理
    public void InitializeStage()
    {
        isClear = false;
        isDead = false;
    }

    //タイトルに戻る時の初期化
    public void InitializeGame()
    {
        isClear = false;
        isDead = false;
        stageNum = 1;
        continueNum = defaultContinueNum;
        kagayaki = 0;
        playerHP = defaultHP;
<<<<<<< HEAD
<<<<<<< Updated upstream
=======
        continuePointNum = 0;
        score = 0;
        /*
>>>>>>> Stashed changes
=======
        continuePointNum = 0;
        /*
>>>>>>> ver0.2
        for (int i = 0; i < 33; i++)
        {
            isStageInitialize[i] = false;
        }
        */
    }

    //SEを再生
    public void PlaySE(AudioClip audioClip)
    {
        if (!GManager.instance.isClear)
        {
            if (audioSource != null)
            {
                audioSource.PlayOneShot(audioClip);

            }
            else
            {
                Debug.Log("オーディオソースが設定されていません");
            }
        }
    }

    public void SetCharacterID(int charaID)
    {
        characterID = charaID;
        if (charaID <= characterNameList.Length && characterID > 0)
        {
            characterName = characterNameList[charaID - 1];
        }
        else
        {
            Debug.Log("キャラクターIDが不正です");
        }
    }
}
