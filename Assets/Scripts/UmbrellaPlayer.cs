using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UmbrellaPlayer : MonoBehaviour
{
    [SerializeField] DiverSpriteManager diverSpriteManager;

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
        
        
        float currentHorizontal = rb.velocity.x;
        float targetHorizontal = 0;
        if (Input.GetKey(KeyCode.J))
        {
            targetHorizontal = -sideSpeed;
        }
        if (Input.GetKey(KeyCode.L))
        {
            targetHorizontal = sideSpeed;
        }
        currentHorizontal = Mathf.Lerp(currentHorizontal, targetHorizontal, Time.deltaTime * transitionRate);
        velocity += targetHorizontal*Vector2.right;
        
        UpdateSprite(currentVertical, -currentHorizontal);
        
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

    public void UpdateSprite(float verticalspeed, float horizontalspeed)
    {
        int speedZone = 0;
        if (verticalspeed > floatSpeed * .8f || verticalspeed < -downSpeed * .8f)
        {
            speedZone = 3;
        }
        else if( verticalspeed > floatSpeed * .4f || verticalspeed < -downSpeed * .4f)
        {
            speedZone = 2;
        }else if ( verticalspeed > floatSpeed * .1f || verticalspeed < -downSpeed * .1f)
        {
            speedZone = 1;
        }
        else
        {
            speedZone = 0;
        }
        
        diverSpriteManager.SetSprite(speedZone);
        
        diverSpriteManager.SetUmbrellaRotation((horizontalspeed/sideSpeed) * ((float)speedZone/3.0f));
        
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
