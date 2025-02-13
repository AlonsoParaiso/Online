using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5;
    public Vector3 dir;
    private Rigidbody rb;
    private float currentTime = 0;
    public float damage =.1f;

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
            currentTime = 0;
            speed = 0;
            GetComponent<PunPoolObject>().readyToUse = true;
        }
    }

    private void FixedUpdate()
    {
        
        rb.velocity = dir * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerManager>())
        {
            PlayerManager pM = collision.gameObject.GetComponent<PlayerManager>();
            pM.Health -= damage;
            gameObject.SetActive(false);
        }
    }
}
