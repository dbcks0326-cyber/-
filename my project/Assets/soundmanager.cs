using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;

public class Soundmanager : MonoBehaviour
{
    public static Soundmanager Instance;

    public AudioClip clipBGM;
    public AudioClip clipFx;


    public AudioClip clip;

    AudioSource audioSourceFx;
    AudioSource audioSourceBGM;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;

        audioSourceFx = gameObject.AddComponent<AudioSource>();
        audioSourceBGM = gameObject.AddComponent<AudioSource>();
        audioSourceFx.clip = clip;
    }

    // Update is called once per frame
    void Update()
    {
        audioSourceFx.PlayOneShot(clip);
    }

    public void PlaySoundFx()
    {
        audioSourceFx.PlayOneShot(clipFx);
    }

    public void PlayBGM()
    {
        audioSourceBGM.clip = clipBGM;
        audioSourceBGM.loop = true;
        audioSourceBGM.Play();
    }

    public void OnOffBGM(bool isOn)
    {
        if (isOn)
        {
            audioSourceBGM.volume = 1;
        }
        else
        {
            audioSourceBGM.volume = 0;
        }
    }

    public void OnOffFx(bool isOn)
    {
        if (isOn)
        {
            audioSourceFx.volume = 1;
        }
        else
        {
            audioSourceFx.volume = 0;
        }
    }
    public void ChangeBGMVolume(float volume)
    {
        audioSourceBGM.volume = volume;
    }

    public void ChangeFxVolume(float volume)
    {
        audioSourceFx.volume = volume;
    }
}
