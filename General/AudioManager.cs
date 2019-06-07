using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;

    //public AudioMixerGroup mixerGroup;

    public Sound[] sounds;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        Play("Tema");

        /*foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;

			//s.source.outputAudioMixerGroup = mixerGroup;
		}*/
    }

    public void Play(string sound, float tiempo=0)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);
        if (s == null)
        {
            Debug.Log("Sonido " + s + " no existe");
            return;
        }
        s.source = gameObject.AddComponent<AudioSource>();
        s.source.clip = s.clip;
        s.source.loop = s.loop;
        s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
        s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

        s.source.Play();
        if (s.source.loop == false)
        {
            StartCoroutine(quitarAudio(s.source));
        }
        else if (tiempo != 0)
        {
            StartCoroutine(quitarAudio(s.source,tiempo));
        }
    }

    IEnumerator quitarAudio(AudioSource a, float time=0)
    {
        if (time == 0)
        {
            yield return new WaitForSeconds(a.clip.length);
        }
        else
        {
            yield return new WaitForSeconds(time);
        }
        Destroy(a);
    }

    public void pausarSonido(bool pausa=true)
    {
        foreach(AudioSource a in GetComponents<AudioSource>())
        {
            if (pausa)
            {
                a.Pause();
            }
            else if(pausa==false)
            {
                a.UnPause();
            }
        }
    }

    public void limpiar()
    {
        foreach (AudioSource a in GetComponents<AudioSource>())
        {
            foreach(Sound s in sounds)
            {
                if (a != null)
                {
                    if (s.clip == a.clip)
                    {
                        if (s.name != "Tema")
                        {
                            Destroy(a);
                        }
                    }
                }
            }
        }
    }
}
