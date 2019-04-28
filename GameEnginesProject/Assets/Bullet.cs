using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {


    public float speed = 100;
     private Rigidbody rb;
    
    // Start is called before the first frame update
    void Start() {
         rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
       // transform.position = transform.position + (transform.forward * speed * Time.deltaTime);
        rb.velocity = (transform.forward * speed);
        
    }
}
