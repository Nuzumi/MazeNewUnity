using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioOptionsScript : MonoBehaviour {

    [SerializeField]
    private NativeEvent updateVolume;

    private AudioSource audioSource;

    private void OnEnable()
    {
        updateVolume.AddListener(UpdateVolume);
    }

    private void OnDisable()
    {
        updateVolume.RemoveListener(UpdateVolume);
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        UpdateVolume();
    }

    private void UpdateVolume()
    {
        audioSource.volume = PlayerPrefs.GetInt(OptionsEnum.SoundsVolume.ToString()) / 100f;
    }
}
