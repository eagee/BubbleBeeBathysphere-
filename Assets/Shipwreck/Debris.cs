using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _turnRate;
    [SerializeField] private float _turnSmoothTime;

    private float _turnVelocity;

    private void FixedUpdate()
    {
        _rigidbody.angularVelocity = Mathf.SmoothDamp(_rigidbody.angularVelocity, _turnRate, ref _turnVelocity, _turnSmoothTime);
    }
}
