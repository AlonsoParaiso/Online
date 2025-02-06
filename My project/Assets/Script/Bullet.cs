using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5;
    public Vector3 dir;
    private Rigidbody rb;
    private float currentTime = 0;

    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
       if(currentTime >= 3)
        {
            gameObject.SetActive(false);
            currentTime = 0;
            speed = 0;
        }
    }

    private void FixedUpdate()
    {
        
        rb.velocity = dir * speed;
    }
}
