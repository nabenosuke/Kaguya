using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{

    private AudioSource audioSource;
    [SerializeField] private AudioClip intro;
    private AudioClip bgm;
    [SerializeField] private float introLen = 8f;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (intro != null)
        {
            bgm = audioSource.clip;
            audioSource.clip = intro;
            audioSource.loop = false;
            audioSource.Play();
            Invoke("SetBGM", introLen);
        }
    }

    // Update is called once per frame
    void SetBGM()
    {
        audioSource.clip = bgm;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void Stop()
    {
        audioSource.Pause();
    }

    public void Restart()
    {
        audioSource.UnPause();
    }
}
