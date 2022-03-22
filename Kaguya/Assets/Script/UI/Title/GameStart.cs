using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{

  private bool isGameStart;
  private float time;
  [SerializeField] private float goTime;
  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    if (isGameStart)
    {
      time += Time.deltaTime;
      if (time > goTime)
      {
        GManager.instance.stageNum = 1;
        SceneManager.LoadScene("Start");
      }
    }
  }

  public void StartGame()
  {
    isGameStart = true;
  }
}
