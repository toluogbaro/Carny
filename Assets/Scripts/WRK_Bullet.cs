using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WRK_Bullet : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet;
    bool spawned;

    [SerializeField]
    private GameObject[] bullets;
    

    private void Awake()
    {
        
    }


    private void Update()
    {
        bullets = GameObject.FindGameObjectsWithTag("Bullet");
        

        if (Input.GetButtonDown("Fire1"))
        {
            Spawn();
            
        }

        Shoot();
       
        
    }

    private void Spawn()
    {
        Instantiate(bullet, transform.position, Quaternion.identity);
    }
    
    private void Shoot()
    {
        foreach (var bullet in bullets)
        {
            bullet.transform.Translate(transform.forward * 5);
           

        }
        
     
    }

   


}
