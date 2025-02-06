using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public Vector3 dir;
    private Rigidbody rb;
    private float timeDestroy = 5;
    private float currentTime = 0;
    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody>();
        dir = transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= timeDestroy)
        {
            Destroy(this.gameObject);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = dir * speed * Time.deltaTime;
    }
}
