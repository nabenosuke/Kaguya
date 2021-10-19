using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageNum : MonoBehaviour
{
  private Text stageText = null;
  private int oldStageNum = 0;

  // Start is called before the first frame update
  void Start()
  {
    stageText = GetComponent<Text>();
    if (GManager.instance != null)
    {
      if (GManager.instance.stageNum <= GManager.instance.maxStageNum)
      {
        stageText.text = "STAGE " + GManager.instance.stageNum;
      }
      else
      {
        switch (GManager.instance.stageNum)
        {
          case 11:
            stageText.text = "vsTSUBASA";
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
  void Update()
  {
    if (oldStageNum != GManager.instance.stageNum)
    {
      if (GManager.instance.stageNum <= GManager.instance.maxStageNum)
      {
        stageText.text = "STAGE " + GManager.instance.stageNum;
        oldStageNum = GManager.instance.stageNum;
      }
    }
    switch (GManager.instance.stageNum)
    {
      case 11:
        stageText.text = "vsTSUBASA";
        oldStageNum = GManager.instance.stageNum;
        break;
    }
  }
}
