using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowPlatform : MonoBehaviour
{
    
    bool _isInBubble = false;
    public void OnBubbleStay()
    {
        _isInBubble = true;
    }
    

    [SerializeField] float maxHeight = 5;
    float minHeight = 0;
    [SerializeField] float growthSpeed = 0.5f;
    
    float growthProgress = 0;

    private void Start()
    {
        minHeight = transform.position.y;
        growthProgress = minHeight;
    }

    private void Update()
    {
        if (_isInBubble) {
            growthProgress += Time.deltaTime * growthSpeed;
        } else {
            growthProgress -= Time.deltaTime * growthSpeed;
        }

        growthProgress = Mathf.Clamp(growthProgress, minHeight, maxHeight);
        
        transform.position = new Vector3(transform.position.x, growthProgress, transform.position.z);
        
        _isInBubble = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + maxHeight *Vector3.up, .2f);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, .2f);
        Gizmos.DrawLine(transform.position, transform.position + maxHeight *Vector3.up);
    }
}
