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
        _measureDuration = _timeSignature * 60f / _bpm;

        SetCurrentPhrase(_startPhrase);
    }

    private void Update()
    {
        _currentTime -= Time.deltaTime;
        if (_currentTime <= 0)
        {
            SetCurrentPhrase(_currentPhrase.GetNextPhrase());
        }
    }

    private void SetCurrentPhrase(MusicPhrase phrase)
    {
        _currentPhrase = phrase;
        _currentTime = _currentPhrase.measures * _measureDuration;
        _currentPhrase.Play(_audioSource);
    }

}
