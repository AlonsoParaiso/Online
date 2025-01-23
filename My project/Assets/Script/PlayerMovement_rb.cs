using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovement_rb : MonoBehaviour
{
    [Header("Fisicas")]
    public float speed, running, rotationSpeed, jumpForce, sphereRadius, acceleration /*, gravityScale*/;
    public string groundMask;
    private Rigidbody rb;
    private float x, z,mouseX; //inputs
    private float currentSpeed;
    private bool pressJump,pressRun;
    private Vector3 movementVector;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //gravityScale = -Mathf.Abs(gravityScale);
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxisRaw("Horizontal"); //GetAxisRaw sirve para teclado y mando
        z = Input.GetAxisRaw("Vertical");
        pressRun = Input.GetKey(KeyCode.LeftShift);
        mouseX = Input.GetAxisRaw("Mouse X");
        RotatePlayer();
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            pressJump = true;
        }
        InterpolateSpeed();
    }

    void RotatePlayer()
    {
        Vector3 rotacion = new Vector3(0, mouseX, 0) * rotationSpeed * Time.deltaTime;
        transform.Rotate(rotacion);
    }

    private void FixedUpdate()
    {
        ApplySpeed(pressRun);
        ApplyJump();
    }

    void ApplySpeed(bool shifhtPress)
    {
        movementVector = (transform.forward * currentSpeed * z) + (transform.right * currentSpeed * x) + // transform.forward ir para alante y para atras, transform.right ir para derecha he izquierda
            new Vector3(0, rb.velocity.y, 0);
        rb.velocity = movementVector;
        /*+ (transform.up * gravityScale)*/
        // rb.AddForce(transform.up * gravityScale);//gravedad realista
    }

    void ApplyJump()
    {
        if (pressJump)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(transform.up * jumpForce);
            pressJump = false;
        }
    }

    void InterpolateSpeed()
    {
        if (pressRun && (x != 0 || z != 0))
        {
            currentSpeed = Mathf.Lerp(currentSpeed, running, acceleration * Time.deltaTime);
        }
        else if (x != 0 || z != 0)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, speed, acceleration * Time.deltaTime);
        }
        else
        {
            currentSpeed = Mathf.Lerp(currentSpeed, 0, acceleration * Time.deltaTime);
        }
    }

    private bool IsGrounded()
    {
        
        Collider[] colliders = Physics.OverlapSphere(new Vector3(transform.position.x, transform.position.y - transform.localScale.y / 2, transform.position.z), sphereRadius);
        
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject.layer == LayerMask.NameToLayer(groundMask))
            {
                return true;
            }
        }
        return false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(new Vector3(transform.position.x, transform.position.y - transform.localScale.y / 2, transform.position.z), sphereRadius);
    }

    public float GetCurrentSpeed()
    {
        return currentSpeed;
    }
}
