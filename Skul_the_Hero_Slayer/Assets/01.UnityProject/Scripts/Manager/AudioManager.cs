using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource bgAudio;
    public AudioClip titleSound;
    public AudioClip castleSound;
    public AudioClip stageSound;
    public AudioClip bossBgSound;
    public bool isPlayAudio;
    private static AudioManager instance = null;
    public static AudioManager Instance
    {
        get
        {
            if (instance == null || instance == default)
            {
                return null;
            }
            return instance;
        }
    } //Instance 프로퍼티

    //싱글톤 패턴 적용
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            InitAudioManager();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this)
            {
                Destroy(this.gameObject);
            }
        }
    } //Awake

    public void InitAudioManager()
    {
        bgAudio = gameObject.GetComponentMust<AudioSource>();
        isPlayAudio = true;
    } //InitAudioManager

}
