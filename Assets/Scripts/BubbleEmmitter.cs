using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleEmmitter : MonoBehaviour
{
    [SerializeField] GameObject bubblePrefab;
    [SerializeField] float emmisionRate;
    [SerializeField] float emmisionRadius;

    private float _timer = 0;
    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= emmisionRate)
        {
            _timer = 0;
            Instantiate(bubblePrefab, transform.position, Quaternion.identity);
        }
    }
}
