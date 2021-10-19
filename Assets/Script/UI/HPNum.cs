using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPNum : MonoBehaviour
{
  private Text heartText = null;
  private int oldPlayerHP = 0;

  // Start is called before the first frame update
  void Start()
  {
    heartText = GetComponent<Text>();
    if (GManager.instance != null)
    {
      heartText.text = "HP " + GManager.instance.playerHP;
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
    if (oldPlayerHP != GManager.instance.playerHP)
    {
      heartText.text = "HP " + GManager.instance.playerHP;
      oldPlayerHP = GManager.instance.playerHP;
    }
  }
}
