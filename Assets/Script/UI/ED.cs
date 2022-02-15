using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ED : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            GManager.instance.InitializeGame();
            SceneManager.LoadScene("Title");
        }
    }
}
