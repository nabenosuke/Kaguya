using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageStart : MonoBehaviour
{
  private float time;
  [SerializeField] private float goTime;
  private string nextStage;

  void Start()
  {
    time = 0f;
  }
  void Update()
  {
    time += Time.deltaTime;
    if (time > goTime)
    {
      GoStage();
    }
  }
  public void GoStage()
  {
    if (GManager.instance.stageNum <= GManager.instance.maxStageNum)
    {
      nextStage = "Stage" + GManager.instance.stageNum;
      Debug.Log("Go " + nextStage);
      SceneManager.LoadScene(nextStage);
    }
    else
    {
      nextStage = "Boss_Tsubasa";
      Debug.Log("Go " + nextStage);
      SceneManager.LoadScene(nextStage);
    }
  }
}
