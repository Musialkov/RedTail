using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using FoxRevenge.Saving;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace FoxRevenge.UI
{
    public class OptionsInUI : MonoBehaviour
    {
        [SerializeField] AudioMixer backgroundMixer;
        [SerializeField] AudioMixer soundEffectMixer;
        [SerializeField] CinemachineFreeLook freeLookCinemachine;
        [SerializeField] Slider backgroundVolumeSlider;
        [SerializeField] Slider soundEffectolumeSlider;
        [SerializeField] Slider mouseSensitivitySlider;

        private void OnEnable() 
        {
            RestoreState();
        }

        public void SetBackgroundSoundVolume(float volume)
        {
            backgroundMixer.SetFloat("Volume", volume);
            SavingSystem.SaveFloat(SavingKeys.BACKGROUND_VOLUME, volume);
        }

        public void SetSoundEffectsVolume(float volume)
        {
            soundEffectMixer.SetFloat("Volume", volume);
            SavingSystem.SaveFloat(SavingKeys.EFFECTS_VOLUME, volume);
        }

        public void SetCameraSensitivity(float sensitivity)
        {
            if(freeLookCinemachine)
            {
                freeLookCinemachine.m_XAxis.m_MaxSpeed = sensitivity;
                freeLookCinemachine.m_YAxis.m_MaxSpeed = sensitivity/100;
            }

            SavingSystem.SaveFloat(SavingKeys.MOUSE_SENSITIVITY, sensitivity);
        }

        public void RestoreState()
        {
            float volume = SavingSystem.ReadFloat(SavingKeys.BACKGROUND_VOLUME);
            if(volume == 0) backgroundMixer.GetFloat("Volume", out volume);
            backgroundVolumeSlider.value = volume;

            volume = SavingSystem.ReadFloat(SavingKeys.EFFECTS_VOLUME);
            if(volume == 0) soundEffectMixer.GetFloat("Volume", out volume);
            soundEffectolumeSlider.value = volume;

            float sensitivity = SavingSystem.ReadFloat(SavingKeys.MOUSE_SENSITIVITY);
            mouseSensitivitySlider.value = sensitivity;
            if(freeLookCinemachine)
            {
                freeLookCinemachine.m_XAxis.m_MaxSpeed = sensitivity;
                freeLookCinemachine.m_YAxis.m_MaxSpeed = sensitivity/100;
            }
        }
    }
}
