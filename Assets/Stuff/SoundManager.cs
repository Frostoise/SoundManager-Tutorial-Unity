
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    /*
    public AudioMixer myMixer; // Directly changes AudioMixer volume values.

    public void MusicVolume(float newValue)
    {
        myMixer.SetFloat("MusicVolume", Mathf.Log10(newValue) * 30); //IMPORTANT NOTE: Your slider min value should not be zero using this method. It will cause a exception.
    }

    public void SFXVolume(float newValue)
    {
        myMixer.SetFloat("SFXVolume", Mathf.Log10(newValue) * 30);
    }
    */

    public GameObject optionsCanvas; //Canvas that controls sound.
    
    private void OnEnable() //This method is called when this script component is enabled.
    {
        DontDestroyOnLoad(gameObject); //DontDestroyOnLoad allows the SoundManager to be carried across scenes.
        DontDestroyOnLoad(optionsCanvas);

        SceneManager.sceneLoaded += SceneManager_sceneLoaded; //inserted sceneLoaded Event
    }

    AudioSource[] audioSources; //List of all audio sources in scene. Can be a local variable instead if the user doesnt wish to store it.

    List<AudioSource> music = new List<AudioSource>(); //Music sources.
    List<AudioSource> sfx = new List<AudioSource>(); //SFX sources.

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1) //Called everytime a new scene is loaded.
    {
        Debug.Log("New scene loaded.");

        audioSources = FindObjectsOfType<AudioSource>(); //Gets all AudioSources in scene.

        music.Clear(); //Clear previous audio sources.
        sfx.Clear();

        foreach (AudioSource audio in audioSources) //Add new audio sources to their respective list.
        {
            switch (audio.outputAudioMixerGroup.name)
            {
                case "Music":
                    music.Add(audio);
                    break;
                case "SFX":
                    sfx.Add(audio);
                    break;
                default:
                    break;
            }
        }

        ChangeMusicVolume(musicVolume); //These methods are called so our previous volume changes are carried over to the new scene.
        ChangeSFXVolume(sfxVolume);
    }

    static float musicVolume = 100; //These store volume settings across scenes. Note: Static values WILL always be kept the same across scenes.
    static float sfxVolume = 100;

    public void ChangeMusicVolume(float newValue) //Change all music volumes individually.
    {
        foreach (AudioSource audio in music)
        {
            if (audio.isPlaying)
            {

                audio.volume = newValue / 100; //The divided number should be the same with the Sliders max value.
                musicVolume = newValue / 100;

                if(newValue <= 0)
                {
                    audio.Stop();
                }
            }
            else
            {
                audio.Play();
            }
        }
    }

    public void ChangeSFXVolume(float newValue) //Change all sfx volumes individually.
    {
        foreach (AudioSource audio in sfx)
        {
            if (audio.isPlaying)
            {
                audio.volume = newValue / 100; //The divided number should be the same with the Sliders max value.
                sfxVolume = newValue / 100;

                if (newValue <= 0)
                {
                    audio.Stop();
                }
            }
            else
            {
                audio.Play();
            }
        }
    }

    public void ChangeScene() //Changes between Scene01 and Scene02
    {
        Scene current = SceneManager.GetActiveScene();

        switch (current.buildIndex)
        {
            case 0:
                SceneManager.LoadSceneAsync(1);
                break;
            case 1:
                SceneManager.LoadSceneAsync(0);
                break;
            default:
                break;
        }
    }
}
