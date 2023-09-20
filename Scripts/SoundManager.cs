using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioSource source;
    public AudioClip[] sounds;
    [SerializeField] private Slider slider;
    public void Start()
    {
        slider.value = 0.5f;
    }
    public void PlaySound(int index)
    {
        source.volume = slider.value;
        source.PlayOneShot(sounds[index]);
    }
}
