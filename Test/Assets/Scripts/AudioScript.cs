using UnityEngine;

public class AudioScript : MonoBehaviour
{
    public AudioClip SuccessfulClip;
    public AudioClip FailClip;
    public AudioSource Source;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlaySuccessful()
    {
        if(Source.clip != SuccessfulClip)
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
