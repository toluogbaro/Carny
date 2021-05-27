using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WRK_Bullet : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet;
    bool spawned;
  



    private void Update()
    {

        if(Input.GetButtonDown("Fire1"))
        {
            Instantiate(bullet, transform.position, Quaternion.identity);
            spawned = true;
        }

        if(spawned)
        {
            bullet.transform.position += bullet.transform.forward * 10f;
        }
        
    }



}
