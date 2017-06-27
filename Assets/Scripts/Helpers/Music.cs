﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//using System.Diagnostics;

namespace Helper_Scripts
{
    public class Music : MonoBehaviour {

        public AudioClip CurrentSong;
        private static AudioSource _source1;
        public List<AudioClip> Songs;

        private static Music _instance = null;


        public static AudioSource Source
        {
            get
            {
                if (_source1 == null) { Source = _instance.GetComponent<AudioSource>(); }
                return _source1;
            }

            set
            {
                _source1 = value;
            }
        }

        void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }
            _instance = this;
            DontDestroyOnLoad(gameObject);
            // SceneManager.activeSceneChanged += SceneManager_activeSceneChanged1; ;
        }

        void OnEnable()
        {
       
            SceneManager.sceneLoaded += OnLevelFinishedLoading;
        }

        void OnDisable()
        {
            SceneManager.sceneLoaded -= OnLevelFinishedLoading;
        }

        void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
        {
            if (CurrentSong != Songs[SceneManager.GetActiveScene().buildIndex])
            {
                Source.Stop();
                CurrentSong = Songs[SceneManager.GetActiveScene().buildIndex];
                Source.loop = true;
                Source.clip = CurrentSong;
                if (PlayerPrefs.GetInt("Music") == 1)
                {
                    Source.Play();
                }
            }
        }

        void Start()
        {
            Source.Stop();
            Source = GetComponent<AudioSource>();
            CurrentSong = Songs[SceneManager.GetActiveScene().buildIndex];
            Source.loop = true;
            Source.clip = CurrentSong;
            Source.Play();
        }
	
        // Update is called once per frame

        public static void PlaySound(AudioClip sound, float volume=1)
        {
            Source.PlayOneShot(sound, volume);
        }

        public static void Stop()
        {
            Source.Stop();
        }

        public static void Play()
        {
            Source.Play();
        }

        public static void ChangeSong(int SongIndex)
        {
            if (SongIndex < 0)
            {
                SongIndex = _instance.Songs.Count + SongIndex;
            }
            Source.Stop();
           _instance.CurrentSong = _instance.Songs[SongIndex];
            Source.loop = true;
            Source.clip =_instance.CurrentSong;
            Source.Play();
        }


    }
}