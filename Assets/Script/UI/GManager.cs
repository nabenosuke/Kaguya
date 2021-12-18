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
    [Header("重力")] public float gravity = 20f;
    public bool isDead;
    public bool isClear;

    private StageControler stageControler;
    private AudioSource audioSource = null;
    //このステージが一度初期化されたか
    [SerializeField] private bool[] isStageInitialize;
    private string[] characterNameList = new string[] { "HONOKA", "KOTORI", "UMI", "RIN", "HANAYO", "MAKI", "NIKO", "NOZOMI", "ELI" };

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
        isStageInitialize = new bool[33];
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
    }


    //ステージ初期化チェック。未初期化なら真を返す
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

    public void InitializeStage()
    {
        isClear = false;
        isDead = false;

    }

    public void InitializeGame()
    {
        isClear = false;
        isDead = false;
        stageNum = 1;
        continueNum = defaultContinueNum;
        kagayaki = 0;
        playerHP = defaultHP;
        for (int i = 0; i < 33; i++)
        {
            isStageInitialize[i] = false;
        }
    }

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
