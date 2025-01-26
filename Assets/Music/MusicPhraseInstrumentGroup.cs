using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MusicPhraseInstrumentGroup
{
    [SerializeField] private InstrumentGroup _instrumentGroup;
    [SerializeField] private AudioClip[] _audioClips;

    public void Play(AudioSource audioSource)
    {
        foreach (AudioClip clip in _audioClips)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
