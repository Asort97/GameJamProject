using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{   
    public static AudioManager instance;
    public AudioClip die;
    public AudioClip attack;
    private void Start()
    {
        if(AudioManager.instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    public void PlaySoundDie()
    {
        GetComponent<AudioSource>().PlayOneShot(die);
    }
    public void PlayAttack()
    {
        GetComponent<AudioSource>().PlayOneShot(attack);
    }
}
