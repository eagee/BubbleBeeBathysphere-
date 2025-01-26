using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private MusicPhrase _startPhrase;
    [SerializeField] private int _timeSignature;
    [SerializeField] private float _bpm; 
    [SerializeField] private AudioSource _audioSource;
    
    private MusicPhrase _currentPhrase;
    private float _currentTime;

    private float _measureDuration;

    private void Start()
    {
        _measureDuration = _timeSignature / _bpm;
        
        _currentPhrase = _startPhrase;
        _currentTime = _currentPhrase.measures * _measureDuration;
    }

    private void Update()
    {
        _currentTime -= Time.deltaTime;
        if (_currentTime <= 0)
        {
            _currentPhrase = _currentPhrase.GetNextPhrase();
            _currentTime = _currentPhrase.measures * _measureDuration;

            _currentPhrase.Play(_audioSource);
        }
    }
}
