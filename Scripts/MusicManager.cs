using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour {

    [SerializeField]
    private List<AudioClip> songList;

    [SerializeField]
    private AudioSource musicPlayer;
    private int menuMusicIdx = 0;
    private int gameMusicIdx = 1;

    private static MusicManager instance = null;
    
    // Singleton assuring only one instance of a music manager exists
    public static MusicManager Instance
    {
        get { return instance; }
    }

    public void toggleMenuToGameMusic(bool _playGameMusic)
    {
        //musicPlayer.Stop();
        if(_playGameMusic)
        {
            musicPlayer.clip = songList[gameMusicIdx];
        } else {
            musicPlayer.clip = songList[menuMusicIdx];
        }
        musicPlayer.Play();
    }

    public void setVolume(float _volume)
    {
        musicPlayer.volume = _volume;
    }

    public MusicManager getMusicManager()
    {
        return instance;
    }

    void Awake()
    {
        // Saves the first instance of music manager
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
            return;
        } else {
            instance = this;
            toggleMenuToGameMusic(false);
        }
        DontDestroyOnLoad(this.gameObject);
    }

}
