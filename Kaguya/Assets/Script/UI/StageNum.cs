using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageNum : MonoBehaviour
{
    private Text stageText = null;
    [SerializeField] private Text stageNumText;

    // Start is called before the first frame update
    void Start()
    {
        stageText = GetComponent<Text>();
        if (GManager.instance != null)
        {
            if (GManager.instance.stageNum <= GManager.instance.maxStageNum)
            {
                stageText.text = "STAGE ";
                stageNumText.text = GManager.instance.stageNum + "";
            }
            else
            {
                switch (GManager.instance.stageNum)
                {
                    case 11:
                        stageText.text = "vsTSUBASA";
                        stageNumText.text = "";
                        break;
                }
            }
        }
        else
        {
            Debug.Log("ゲームマネージャー置き忘れてるよ！");
            Destroy(this);
        }
    }

    // Update is called once per frame
    /*
    void Update()
    {
        if (oldStageNum != GManager.instance.stageNum)
        {
            if (GManager.instance.stageNum <= GManager.instance.maxStageNum)
            {
                stageText.text = "STAGE ";
                stageNumText.text = GManager.instance.stageNum + "";
                oldStageNum = GManager.instance.stageNum;
            }
        }
        switch (GManager.instance.stageNum)
        {
            case 11:
                stageText.text = "vsTSUBASA";
                stageNumText.text = "";
                oldStageNum = GManager.instance.stageNum;
                break;
        }
    }
    */
}
