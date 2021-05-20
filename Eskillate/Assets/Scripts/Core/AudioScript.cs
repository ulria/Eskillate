using UnityEngine;

namespace Core
{
    public class AudioScript : MonoBehaviour
    {
        public AudioClip SuccessfulClip;
        public AudioClip FailClip;
        public AudioSource Source;

        // Start is called before the first frame update
        void Start()
        {
            var volumeRatio = PlayerPrefs.GetFloat("VolumeRatio", 1.0f);
            Source.volume *= volumeRatio;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void PlaySuccessful()
        {
            if (Source.clip != SuccessfulClip)
            {
                Source.clip = SuccessfulClip;
            }
            Source.Play();
        }

        public void PlayFail()
        {
            if (Source.clip != FailClip)
            {
                Source.clip = FailClip;
            }
            Source.Play();
        }
    }
}