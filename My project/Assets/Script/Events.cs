using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Events : MonoBehaviour
{

    public UnityEvent<int, float> events;
    // Start is called before the first frame update
    void Start()
    {
        events.AddListener(explosion);
        events.Invoke(12, .9f);
    }

    // Update is called once per frame
    public void explosion(int e, float f)
    {
        Debug.Log(e + f);
    }
}
