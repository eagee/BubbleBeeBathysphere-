using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleColumn : MonoBehaviour
{
    [SerializeField] private float Force = 2;
    private void OnTriggerStay2D(Collider2D other)
    {
        BubbleWatcher watcher = other.GetComponent<BubbleWatcher>();
        if (watcher != null)
        {
            watcher.OnCollumnStay(transform.up * Force);
        }
            
    }
}

