using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Music Phrase", menuName = "Music Phrase")]
public class MusicPhrase : ScriptableObject
{
    [SerializeField] private MusicPhrase[] _phrasesAfter;
    [SerializeField] private int _measures;
    [SerializeField] private MusicPhraseInstrumentGroup[] _instrumentGroups;

    public int measures => _measures;

    public MusicPhrase GetNextPhrase()
    {
        return _phrasesAfter[Random.Range(0, _phrasesAfter.Length)];
    }

    public void Play(AudioSource audioSource)
    {
        foreach (MusicPhraseInstrumentGroup instrumentGroup in _instrumentGroups)
        {
            instrumentGroup.Play(audioSource);
        }
    }
}
