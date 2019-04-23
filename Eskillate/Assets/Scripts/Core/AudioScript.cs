using UnityEngine;

namespace Core
{
    public class AudioScript : MonoBehaviour
    {
        public AudioClip SuccessfulClip;
        public AudioClip FailClip;
        public AudioSource Source;
        public float VolumeRatio;

        // Start is called before the first frame update
        void Start()
        {
            Source.volume *= VolumeRatio;
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