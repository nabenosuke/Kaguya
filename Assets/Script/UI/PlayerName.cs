using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerName : MonoBehaviour
{
  private Text playerText = null;
  private string oldName = null;

  // Start is called before the first frame update
  void Start()
  {
    playerText = GetComponent<Text>();
    if (GManager.instance != null)
    {
      playerText.text = " " + GManager.instance.playerName;
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
    if (oldName != GManager.instance.playerName)
    {
      playerText.text = GManager.instance.playerName;
      oldName = GManager.instance.playerName;
    }
  }
}
