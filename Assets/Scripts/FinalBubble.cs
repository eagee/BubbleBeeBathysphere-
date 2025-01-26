using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBubble : MonoBehaviour
{
    private bool _BathySecured;
    private bool _DiverSecured;
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        BathController bathy = other.GetComponent<BathController>();
        if (bathy != null)
        {
            bathy.gameObject.SetActive(false);
            _BathySecured = true;
        }
        
        UmbrellaPlayer umbrellaPlayer = other.GetComponentInParent<UmbrellaPlayer>();
        if (umbrellaPlayer != null)
        {
            umbrellaPlayer.gameObject.SetActive(true);
            _DiverSecured = true;
        }


        if (_BathySecured && _DiverSecured)
        {
            OnCompleted();
        }
    }

    public void OnCompleted()
    {
        
        
    }
}
