using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float maxTime, trowForce;

    private float currentTime;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= maxTime)
        {
            currentTime = 0;
            rb.velocity = Vector3.zero;
            GetComponent<PunPoolObject>().readyToUse = true;
            //gameObject.SetActive(false);
        }
    }

    public void ResetVelocity()
    {
        rb.velocity = Vector3.zero;
    }

    public void ApplyParabolicThrow(Transform ownerTransform)
    {
        rb.AddForce((ownerTransform.forward + Vector3.up) * trowForce);
    }

    private void OnDisable()
    {
        
       
    }

}
