using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    private AudioSource musicSource;
    private const string MUSIC_VOLUME = "MusicVolume";
    private float volume;

    private void Awake() {
        Instance = this;
        musicSource = GetComponent<AudioSource>();

        volume = PlayerPrefs.GetFloat(MUSIC_VOLUME, 0.3f);
        musicSource.volume = volume;
    }

    public void ChangeVolume() {
        volume += 0.1f;

        if (volume > 1f) {
            volume = 0f;
        }

        musicSource.volume = volume;
        PlayerPrefs.SetFloat(MUSIC_VOLUME, volume);
        PlayerPrefs.Save();
    }

    public float GetVolume() {
        return volume;
    }
}
