using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace FoxRevenge.UI
{
    public class OptionsInUI : MonoBehaviour
    {
        [SerializeField] AudioMixer backgroundMixer;
        [SerializeField] AudioMixer soundEffectMixer;
        [SerializeField] Slider backgroundVolumeSlider;
        [SerializeField] Slider soundEffectolumeSlider;

        private void OnEnable() 
        {
            float volume;
            backgroundMixer.GetFloat("Volume", out volume);
            backgroundVolumeSlider.value = volume;

            soundEffectMixer.GetFloat("Volume", out volume);
            soundEffectolumeSlider.value = volume;
        }

        public void SetBackgroundSoundVolume(float volume)
        {
            backgroundMixer.SetFloat("Volume", volume);
        }

        public void SetSoundEffectsVolume(float volume)
        {
            soundEffectMixer.SetFloat("Volume", volume);
        }
    }
}
