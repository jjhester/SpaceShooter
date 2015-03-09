using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;

public class GameController : MonoBehaviour {
   public bool showAds;
   public AudioClip[] backgroundMusic;
   public GameObject[] hazards;
   public GameObject[] enemies;
   public Vector3 spawnValues;
   
   public int hazardCount;
   public int enemyCount;
   public float spawnWait;
   public float startWait;
   public float waveWait;
   public float waveSpeed;
   

   public GUIText scoreText, highText, nextWaveText, musicText;
   public GameObject menu;
   private int score, highScore, waveCount;
   private bool gameOver;
   private byte[] enemyDistribution;
   private Scroller backgroundScroller;
   private AudioSource backgroundMusicPlayer;
   private AdController adController;


   void Start () {
	 enemyDistribution = new byte[]{0,0,0,1};
	 if (!PlayerPrefs.HasKey("HighScore")) {
	    PlayerPrefs.SetInt("HighScore", 800);
	 }
	 //Debug.Log("HighScore: " + PlayerPrefs.GetInt("HighScore"));
	 highScore = PlayerPrefs.GetInt("HighScore");
	 if (showAds) {
	    this.adController = GetComponent<AdController>();
	    this.adController.RequestBanner();
	    this.adController.RequestInterstitial();
	 }
	 //get backgroundScroller reference
	 GameObject backgroundScrollerObject = GameObject.FindWithTag("Background");
	 if (backgroundScrollerObject != null) {
	    backgroundScroller = backgroundScrollerObject.GetComponent<Scroller>();
	    backgroundMusicPlayer = backgroundScrollerObject.GetComponent<AudioSource>();
	 } else {
	    Debug.Log("Cannot find 'BackgroundScroller' script");
	 }
	 NewBackgroundMusic();
	 backgroundMusicPlayer.Play();
	 backgroundScroller.StartScroll(-waveSpeed);
	 score = 0;
	 waveCount = 0;
	 nextWaveText.text = "";
	 gameOver = false;
	 menu.SetActive(false);
	 UpdateScoreDisplay();
	 StartCoroutine(SpawnWaves());
   }
   void Update () {
	 //Nothing
   }
   void ShowAdBanner () {
	 if (this.adController != null) {
	    adController.ShowBannerAd();
	 }
   }
   void HideAdBanner () {
	 if (this.adController != null) {
	    adController.HideBannerAd();
	 }
   }
   void ShowInterstitial () {
	 if (this.adController != null) {
	    StartCoroutine(adController.ShowInterstitial(1));
	 }
   }
   public void Restart () {
	 HideAdBanner();
	 Application.LoadLevel(Application.loadedLevel);
   }
//   public void Quit () {
////	 Application.Quit(); //Not working
////	 var pt = System.Diagnostics.Process.GetCurrentProcess().Threads;
////	 foreach (var p in pt) {
////	    p.Dispose();
////	 }
////	 System.Diagnostics.Process.GetCurrentProcess().Kill(); 
//   }
////   public void Pause () {
////	 menu.SetActive(true);
////	 ShowInterstitial();
////   }
////   public void Unpause () {
////	 menu.SetActive(false);
////   }
   public void QuitGame () {
	 //Debug.Log("Quitting game...");
	 Application.Quit();
   }
  
   void NewBackgroundMusic () {
	 AudioClip bgMusic = backgroundMusic [Random.Range(0, backgroundMusic.Length)];
	 if (bgMusic == null) {
	    bgMusic = backgroundMusic [1];
	 } //default
	 backgroundMusicPlayer.clip = bgMusic;
	 musicText.text = backgroundMusicPlayer.clip.name + " by Durvin";
   }
   IEnumerator SpawnWaves () {
	 yield return new WaitForSeconds(startWait);
	 while (!gameOver) {
	    NextWave();
	    for (int i = 0; i < hazardCount * waveCount/2; i++) {
		  SpawnAsteriod();
		  yield return new WaitForSeconds(spawnWait);
		  if (i == 2) { // only after third asteroid
			nextWaveText.text = "";
		  } 
	    }
	    
	    if (waveCount > 1) {
		  for (int i = 0; i < enemyCount * (waveCount - 1); i++) {
			SpawnEnemy();
			yield return new WaitForSeconds(spawnWait);
		  }
	    }
	    
	    for (int i = 0; i < hazardCount * waveCount/2; i++) {
		  SpawnAsteriod();
		  yield return new WaitForSeconds(spawnWait);
	    }
	    yield return new WaitForSeconds(waveWait);
	 }
   }
   void SpawnEnemy () {
	 Vector3 spawnPosition = GetOpenPosition();
	 Quaternion spawnRotation = Quaternion.Euler(0.0f, 180, 0.0f);
	 GameObject enemy = Instantiate(enemies [enemyDistribution [Random.Range(0, 4)]], spawnPosition, spawnRotation) as GameObject;
	 enemy.GetComponent<EnemyController>().speedFactor = Random.Range(waveSpeed + 1, waveSpeed * waveCount + 1);
   }
   void SpawnAsteriod () {
	 Vector3 spawnPosition = GetOpenPosition();
	 Quaternion spawnRotation = Quaternion.identity;
	 GameObject asteroid = Instantiate(hazards [Random.Range(0, hazards.Length)], spawnPosition, spawnRotation) as GameObject;
	 asteroid.GetComponent<Mover>().speed = Random.Range(-waveSpeed * waveCount, -waveSpeed);
   }
   Vector3 GetOpenPosition () {
	 Vector3 position;
	 do {
	    position = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
	 } while (Physics.CheckSphere(position, 2f));
	 return position;
   }
   void NextWave () {
	 if (waveCount > 0) {
	    HideAdBanner();
	    GetComponent<AudioSource>().Play();
	 }
	 waveCount++;
	 nextWaveText.text = "Wave " + waveCount;
	 
   }
   public void AddScore ( int newScoreValue ) {
	if (!gameOver) {
		 score += newScoreValue;
		 if (score > highScore) highScore = score;
		 	UpdateScoreDisplay();
	}
   }
	public void GameOver() {
		ShowAdBanner();
		ShowInterstitial();
		nextWaveText.text = "";
		gameOver = true;
		menu.SetActive(true);
		PlayerPrefs.SetInt("HighScore", highScore);
		PlayerPrefs.Save();
	}
   void UpdateScoreDisplay () {
	 highText.text = highScore.ToString("0000");
	 scoreText.text = score.ToString("0000");
   }
}
