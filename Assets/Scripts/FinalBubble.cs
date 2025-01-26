using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        if (umbrellaPlayer != null && !umbrellaPlayer.IsPlant)
        {
            umbrellaPlayer.gameObject.SetActive(false);
            _DiverSecured = true;
        }


        if (_BathySecured && _DiverSecured)
        {
            OnCompleted();
            _RunningAnimation = true;
            _timer = 0;
        }
    }

    private bool _RunningAnimation;
    
    public void OnCompleted()
    {
        Camera.main.GetComponent<SmoothCameraFollow>().target = this.transform;
    }

    private float _timer;
    private float movestart = 3;
    private float popTime = 10;
    private float loadTime = 15;
    private void Update()
    {

        if (_RunningAnimation)
        {
            _timer += Time.deltaTime;

            if (_timer >= movestart)
            {
                transform.position += Vector3.up * Time.deltaTime;
            }

            if (_timer >= popTime)
            {
                GetComponent<SpriteRenderer>().enabled = false;
            }

            if (_timer >= loadTime)
            {
                OnAnimationComplete();
            }
        }
        
    }

    public void OnAnimationComplete()
    {
        SceneManager.LoadScene("StartScene");
    }
}
