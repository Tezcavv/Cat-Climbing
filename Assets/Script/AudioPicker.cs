using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AudioSource))]
public class AudioPicker : MonoBehaviour
{
    [SerializeField] private List<AudioClip> clipList;
    private AudioSource myAudioSource;
    private AudioClip lastAudioclipPlayed;


    private void Awake() {
        myAudioSource= GetComponent<AudioSource>();
    }

    public void PlaySound(bool canRepeatSameSound) {

        AudioClip toBePlayed;
        if (!canRepeatSameSound) {
           toBePlayed= getDifferentSound();
        } else {
            toBePlayed = clipList[Random.Range(0, clipList.Count)];
        }

        lastAudioclipPlayed= toBePlayed;
        myAudioSource.clip = toBePlayed;
        myAudioSource.Play();
        

        

    }

    private AudioClip getDifferentSound() {

        if(lastAudioclipPlayed == null || !clipList.Contains(lastAudioclipPlayed)) {
            return clipList[Random.Range(0, clipList.Count)];
        }

        bool found = false;
        AudioClip result = null;

        while (!found) {
            result = clipList[Random.Range(0, clipList.Count)];
            if (!result.Equals(lastAudioclipPlayed)) {
                return result;
            }
        }

        return result;
    }
}
