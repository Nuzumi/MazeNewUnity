using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum OptionsEnum { SoundsOn, SoundsVolume}

public class OptionsScript : MonoBehaviour {

    public bool SoundsOn { get; set; }
    public int SoundsVolume { get; set; }

    [SerializeField]
    private NativeEvent updateVolume;
    [SerializeField]
    private Toggle soundsOn;
    [SerializeField]
    private Slider soundsVolume;
    [SerializeField]
    private Text soundsVolumeText;

    private void Start()
    {
        soundsOn.isOn = PlayerPrefs.GetInt(OptionsEnum.SoundsOn.ToString()) == 1 ? true : false;
        soundsVolume.value = PlayerPrefs.GetInt(OptionsEnum.SoundsVolume.ToString());
        soundsVolumeText.text = soundsVolume.value.ToString();
    }

    public void SetSoundOn()
    {
        SoundsOn = soundsOn.isOn;
        if (!SoundsOn)
        {
            soundsVolume.value = 0;
            SetSoundVolume();
        }

        OnLeveOptions();
    }

    public void SetSoundVolume()
    {
        SoundsVolume = (int)soundsVolume.value;
        soundsVolumeText.text = SoundsVolume.ToString();
        if(SoundsVolume > 0)
        {
            soundsOn.isOn = true;
            SetSoundOn();
        }
        else
        {
            if (SoundsOn)
            {
                soundsOn.isOn = false;
                SetSoundOn();
            }
        }

        OnLeveOptions();
    }

    public void OnLeveOptions()
    {
        PlayerPrefs.SetInt(OptionsEnum.SoundsOn.ToString(), SoundsOn ? 1 : 0);
        PlayerPrefs.SetInt(OptionsEnum.SoundsVolume.ToString(), SoundsVolume);
        updateVolume.Invoke();
    }
}
