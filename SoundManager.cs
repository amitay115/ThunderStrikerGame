using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager main;
    public AudioClip _music;
    public AudioClip Click;
    public AudioClip MissionComp;
    public AudioSource Effectsource;
    public AudioSource UISource;
    // Start is called before the first frame update

    void Awake()
    {
        main = this;
    }
    private void OnEnable()
    {
        main = this;
    }
    void Start()
    {
        //GetComponent<AudioSource>().clip = _music;
        GetComponent<AudioSource>().Pause();
    }

    public void PlaySimpleClip(AudioClip clip)
    {
        Effectsource.PlayOneShot(clip);

    }
    public void PauseSimpleClip()
    {
        Effectsource.Pause();

    }
    public void ClickButton()
    {
        UISource.PlayOneShot(Click);
    }
    public void CompleteMission()
    {
        UISource.PlayOneShot(MissionComp);
    }
    public void Music(bool p)
    {
        if (p)
            GetComponent<AudioSource>().UnPause();
        if (!p)
            GetComponent<AudioSource>().Pause();
    }
}
