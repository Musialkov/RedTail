using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace FoxRevenge.Audio
{
    
    [CreateAssetMenu(fileName = "SoundsRandomizer", menuName = "FoxRevenge/SoundsRandomizer", order = 0)]
    public class SoundsRandomizer : ScriptableObject 
    {
        [SerializeField] private List<AudioClip> sounds;
        [RangedFloat(0f, 2f)]
        [SerializeField]  private RangedFloat pitchValues;
        [RangedFloat(0f, 1f)]
        [SerializeField]  private RangedFloat volumeValues;

        public AudioInfo GetRandomAucioCLip()
        {
            AudioClip clip = sounds.ElementAt(Random.Range(0, sounds.Count));

            AudioInfo audioInfo = new AudioInfo();
            audioInfo.clip = clip;
            audioInfo.pitch = pitchValues.GetRandomValue();
            audioInfo.volume = volumeValues.GetRandomValue();

            return audioInfo;
        }

        public struct AudioInfo
        {
            public AudioClip clip;
            public float pitch;
            public float volume;
        }
    }
}