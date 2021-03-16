using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifeTime;
    public float speed;
    
    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * (speed * Time.deltaTime);

        lifeTime -= Time.deltaTime;
        
        if (lifeTime <= 0) Destroy(gameObject);
    }
}
