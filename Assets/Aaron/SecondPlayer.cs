using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondPlayer : MonoBehaviour
{

     [SerializeField] Rigidbody2D rb;

     [SerializeField] float downSpeed = 5;
     [SerializeField] float floatSpeed = 5;
     [SerializeField] float sideSpeed = 5;
     [SerializeField] float transitionRate = 0.5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float currentVertical = rb.velocity.y;
        
        float targetVertical = -downSpeed;
        if (IsInBubble())
        {
            targetVertical = floatSpeed;
        }
 
        currentVertical = Mathf.Lerp(currentVertical, targetVertical, Time.deltaTime * transitionRate);
        Vector2 velocity = new Vector2(0, currentVertical);
        
        if (Input.GetKey(KeyCode.A))
        {
            velocity += sideSpeed*Vector2.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            velocity += -sideSpeed*Vector2.left;
        }
        
        rb.velocity = velocity;
    }
    
    List<Collider2D> colliders = new List<Collider2D>();

    public bool IsInBubble()
    {
        foreach (var collider in colliders)
        {
            if (collider.tag == "Bubble")
            {
                return true;
            }
        }

        return false;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        colliders.Add(other);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        colliders.Remove(other);
    }
}
