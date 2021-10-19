using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueNum : MonoBehaviour
{
  private Text continueText = null;
  private int oldContinueNum = 0;

  // Start is called before the first frame update
  void Start()
  {
    continueText = GetComponent<Text>();
    if (GManager.instance != null)
    {
      continueText.text = GManager.instance.continueNum.ToString();
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
    if (oldContinueNum != GManager.instance.continueNum)
    {
      continueText.text = GManager.instance.continueNum.ToString();
      oldContinueNum = GManager.instance.continueNum;
    }
  }
}
