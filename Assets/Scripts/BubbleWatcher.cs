using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class BubbleWatcher : MonoBehaviour
{
    [SerializeField] private UnityEvent OnBubbleStay;
    
    
    List<Collider2D> colliders = new List<Collider2D>();

    private void Update()
    {
        if (IsInBubble())
        {
            OnBubbleStay.Invoke();
        }
    }

    public bool IsInBubble()
    {
        foreach (var collider in colliders)
        {
            if (collider.tag == "Bubble")
            {
                BubblePopper popper = collider.GetComponent<BubblePopper>();
                if (popper != null)
                    popper.OnCollide();
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
