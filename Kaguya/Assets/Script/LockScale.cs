using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockScale : MonoBehaviour
{
    [SerializeField] GameObject target;
    private Vector3 defaultLocalScale;
    // Start is called before the first frame update
    void Start()
    {
        defaultLocalScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {

        if (target.transform.localScale.x * transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(target.transform.localScale.x * defaultLocalScale.x, defaultLocalScale.y, defaultLocalScale.z);
        }
    }
}
