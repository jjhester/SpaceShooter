using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadController : MonoBehaviour {
   public GameObject backgroundObject;
   public AudioClip backgroundMusic;
   void Start () {
	 if (backgroundObject != null) {
	    AudioSource backgroundMusicPlayer = backgroundObject.GetComponent<AudioSource>();
	    backgroundMusicPlayer.clip = backgroundMusic;
	    DontDestroyOnLoad(backgroundMusicPlayer.clip);
	    backgroundMusicPlayer.Play();
	 }
   }
   // Update is called once per frame
   void Update () {

   }
   public void LoadMainScene () {
	 Debug.Log("Loading scene...");
	 Application.LoadLevel(1);
   }
}