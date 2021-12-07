using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayDestroy : MonoBehaviour
{
    [SerializeField] float destroyTime = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyThis());
    }

    // Update is called once per frame
    IEnumerator DestroyThis()
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }
}
