using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    public AudioClip[] music;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Awake()
    {
        SetUpSingleton();
        audioSource = gameObject.GetComponent<AudioSource>();
    }
    private void SetUpSingleton()
    {
        if (FindObjectsOfType(GetType()).Length > 1) //get type refers to the type of the scripted object
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale!= 1f){ Pause(); }
        if ((SceneManager.GetActiveScene().name == "Scene 2-1" || SceneManager.GetActiveScene().name == "Scene 2-2" || SceneManager.GetActiveScene().name == "Scene 2-3") && audioSource.clip != music[1]) { audioSource.clip = music[1]; audioSource.Play(); }
        else if ((SceneManager.GetActiveScene().name == "Scene 3" || SceneManager.GetActiveScene().name == "Scene 4") && audioSource.clip != music[2]) { audioSource.clip = music[2]; audioSource.Play(); }
        else if ((SceneManager.GetActiveScene().name == "Start Scene" || SceneManager.GetActiveScene().name == "Scene 1") && audioSource.clip != music[3]) { audioSource.clip = music[3]; audioSource.Play(); }
        else if ((SceneManager.GetActiveScene().name == "Scene 5") && audioSource.clip != music[0]) { audioSource.clip = music[0]; audioSource.Play(); }
    }
    public void Pause()
    {
        GetComponent<AudioSource>().Pause();
    }
    public void Play()
    {
        GetComponent<AudioSource>().Play();
    }
}
