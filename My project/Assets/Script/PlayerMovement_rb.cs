using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovement_rb : MonoBehaviourPunCallbacks
{
    [Header("Fisicas")]
    public float speed, running, rotationSpeed, jumpForce, sphereRadius, acceleration /*, gravityScale*/;
    public string groundMask;
    private Rigidbody rb;
    private float x, z,mouseX; //inputs
    private float currentSpeed;
    private bool pressJump,pressRun;
    private Vector3 movementVector;

    [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
    public static GameObject LocalPlayerInstance;
    // Start is called before the first frame update

    private void Awake()
    {
        if (photonView.IsMine)
        {
            PlayerManager.LocalPlayerInstance = this.gameObject;
        }
        // #Critical
        // we flag as don't destroy on load so that instance survives level synchronization, thus giving a seamless experience when levels load.
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        CameraWork _cameraWork = this.gameObject.GetComponent<CameraWork>();

        if (_cameraWork != null)
        {
            if (photonView.IsMine)
            {
                _cameraWork.OnStartFollowing();
            }
        }
        else
        {
            Debug.LogError("<Color=Red><a>Missing</a></Color> CameraWork Component on playerPrefab.", this);
        }
        //gravityScale = -Mathf.Abs(gravityScale);

#if UNITY_5_4_OR_NEWER
        // Unity 5.4 has a new scene management. register a method to call CalledOnLevelWasLoaded.
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
#endif
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine && PhotonNetwork.IsConnected) 
        {
            return; 
        }
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

#if UNITY_5_4_OR_NEWER
    void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode loadingMode)
    {
        this.CalledOnLevelWasLoaded(scene.buildIndex);
    }
#endif

#if !UNITY_5_4_OR_NEWER
/// <summary>See CalledOnLevelWasLoaded. Outdated in Unity 5.4.</summary>
void OnLevelWasLoaded(int level)
{
    this.CalledOnLevelWasLoaded(level);
}
#endif

    void CalledOnLevelWasLoaded(int level)
    {
        // check if we are outside the Arena and if it's the case, spawn around the center of the arena in a safe zone
        if (!Physics.Raycast(transform.position, -Vector3.up, 5f))
        {
            transform.position = new Vector3(0f, 5f, 0f);
        }
    }

#if UNITY_5_4_OR_NEWER
    public override void OnDisable()
    {
        // Always call the base to remove callbacks
        base.OnDisable();
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }
#endif
}
