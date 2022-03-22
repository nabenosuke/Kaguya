using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private float goTime;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("GoStage", goTime);
    }

    // Update is called once per frame
    void GoStage()
    {
        SceneManager.LoadScene("Title");
    }
}
