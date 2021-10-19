using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KagayakiNum : MonoBehaviour
{
  private Text kagayakiText = null;
  private int oldkagayaki = 0;

  // Start is called before the first frame update
  void Start()
  {
    kagayakiText = GetComponent<Text>();
    if (GManager.instance != null)
    {
      kagayakiText.text = GManager.instance.kagayaki.ToString();
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
    if (oldkagayaki != GManager.instance.kagayaki)
    {
      kagayakiText.text = GManager.instance.kagayaki.ToString();
      oldkagayaki = GManager.instance.kagayaki;
    }
  }
}
