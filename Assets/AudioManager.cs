using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioManager : MonoBehaviour
{

    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioSource coinSource;
    [SerializeField] private AudioPicker swipeAudioPicker;

    public UnityEvent OnCoinPicked = new UnityEvent();
    public UnityEvent OnDodgeSwipe = new UnityEvent();


    private void Awake() {

        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        OnCoinPicked.AddListener(PlayCoinSound);
        OnDodgeSwipe.AddListener(PlaySwipeSound);
    }

    private void PlaySwipeSound() {
        swipeAudioPicker.PlaySound(false);
    }

    private void PlayCoinSound() {
        coinSource.Play();
    }


}
