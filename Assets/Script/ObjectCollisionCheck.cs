using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCollisionCheck : MonoBehaviour
{

  private IEnemyMove iNinja;
  // Start is called before the first frame update
  void Start()
  {
    iNinja = GetComponentInParent<IEnemyMove>();
  }

  // Update is called once per frame
  void Update()
  {

  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.tag == "Ground" || collision.tag == "Enemy")
    {
      iNinja.Turn();
    }
  }
}
