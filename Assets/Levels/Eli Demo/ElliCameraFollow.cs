using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElliCameraFollow : MonoBehaviour
{
    public float smoothing;
    public float turnSmoothing;
    public Transform player; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, player.position, smoothing * 10);
        transform.rotation = Quaternion.Slerp(transform.rotation, player.rotation, turnSmoothing * 10);
        transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y, 0));
    }
}
