using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageStart : MonoBehaviour
{
    [SerializeField] private AudioClip museIntro;
    [SerializeField] private AudioClip ariseIntro;
    private AudioSource audioSource;
    [SerializeField] private float goTime;


    private string nextStage;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Invoke("GoStage", goTime);
        //arise操作時はXOR?
        if (GManager.instance.stageNum <= GManager.instance.maxStageNum)
        {
            audioSource.clip = museIntro;
        }
        else
        {
            audioSource.clip = ariseIntro;
        }
        audioSource.Play();
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
            nextStage = "Boss";
            Debug.Log("Go " + nextStage);
            SceneManager.LoadScene(nextStage);
        }
    }
}
