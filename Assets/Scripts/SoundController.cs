using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundController : MonoBehaviour
{
    public AudioClip bgm_Title;
    public AudioClip bgm_Stage;
    public AudioClip bgm_Result;
    public AudioClip bgm_GameClear;
    public AudioClip bgm_GameOver;

    AudioSource audio;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audio = GetComponent<AudioSource>();

        //åªç›ÉVÅ[ÉìèÓïÒÇÃéÊìæ
        string currentSceneName = SceneManager.GetActiveScene().name;

        if(currentSceneName == "Title")
        {
            PlayBGM(bgm_Title);
        }
        else if(currentSceneName == "Result")
        {
            PlayBGM(bgm_Result);
        }
        else
        {
            PlayBGM(bgm_Stage);
        }
    }

    void PlayBGM(AudioClip clip)
    {
        audio.clip = clip;
        audio.loop = true;
        audio.Play();
    }

    
}
