using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
  private AudioSource audioSource;
  private BGM bGM;
  // Start is called before the first frame update
  void Start()
  {
    audioSource = GetComponent<AudioSource>();
  }

  // Update is called once per frame
  void Update()
  {

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
