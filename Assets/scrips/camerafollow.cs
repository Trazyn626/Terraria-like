using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerafollow : MonoBehaviour
{
    public Transform target;
    public float smoothing;
    void Start()
    {
        
    }

    void LateUpdate()
    {
        if (target != null)
        {
            if (transform.position != target.position)
            {
                Vector3 targetpos = target.position;
                transform.position = Vector3.Lerp(transform.position, targetpos, smoothing);
            }
        
        }
    }
    void Update()
    {
        
    }
}
