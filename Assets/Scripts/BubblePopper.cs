using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubblePopper : MonoBehaviour
{
    [SerializeField] GameObject bubbleRoot;

    private float health = .5f;
    
    public void OnCollide()
    {
        health -= Time.deltaTime;
        if (health <= 0)
            Destroy(bubbleRoot);
        
    }
}
