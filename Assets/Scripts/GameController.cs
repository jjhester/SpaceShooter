using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
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
   private Scroller backgroundScroller;
   private AudioSource backgroundMusicPlayer;

   void Start () {
	 if (!PlayerPrefs.HasKey("HighScore")) {
	    PlayerPrefs.SetInt("HighScore", 800);
	 }
	 Debug.Log(PlayerPrefs.GetInt("HighScore"));
	 highScore = PlayerPrefs.GetInt("HighScore");
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
	 UpdateScore();
	 StartCoroutine(SpawnWaves());

   }
   void Update () {
	 //Nothing
   }
   public void Restart () {
	 NewBackgroundMusic();
	 Application.LoadLevel(Application.loadedLevel);
   }
   public void Quit () {
	 Application.Quit();
   }
   void NewBackgroundMusic () {
	 backgroundMusicPlayer.clip = backgroundMusic [Random.Range(0, backgroundMusic.Length - 1)];
	 musicText.text = backgroundMusicPlayer.clip.name + " by Durvin";
   }
   IEnumerator SpawnWaves () {
	 yield return new WaitForSeconds(startWait);
	 while (!gameOver) {
	    NextWave();
	    for (int i = 0; i < hazardCount * waveCount/2; i++) {
		  SpawnAsteriod();
		  yield return new WaitForSeconds(spawnWait);
	    }
	    nextWaveText.text = "";
	    if (waveCount > 1) {
			
		  for (int i = 0; i < enemyCount * waveCount; i++) {
			SpawnEnemy(Random.Range(10, waveCount * 5));
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
   void SpawnEnemy ( float waveSpeed ) {
	 Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
	 Quaternion spawnRotation = Quaternion.AngleAxis(Random.Range(170, 190), Vector3.down);
	 GameObject enemy = Instantiate(enemies [Random.Range(0, enemies.Length - 1)], spawnPosition, spawnRotation) as GameObject;
	 enemy.GetComponent<Mover>().speed = waveSpeed;
   }
   void SpawnAsteriod () {
	 Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
	 Quaternion spawnRotation = Quaternion.identity;
	 GameObject asteroid = Instantiate(hazards [Random.Range(0, hazards.Length - 1)], spawnPosition, spawnRotation) as GameObject;
	 asteroid.GetComponent<Mover>().speed = Random.Range(-waveSpeed * waveCount, -waveSpeed);
   }
   void NextWave () {
		if (waveCount > 0) {
			audio.Play();
		}
	 waveCount++;
	 nextWaveText.text = "Wave " + waveCount;
   }
   public void AddScore ( int newScoreValue ) {
	 score += newScoreValue;
	 if (score > highScore) highScore = score;
	 UpdateScore();
   }
	public void GameOver() {
		nextWaveText.text = "";
		gameOver = true;
		menu.SetActive(true);
		PlayerPrefs.SetInt("HighScore", highScore);
		PlayerPrefs.Save();
	}
   void UpdateScore () {
	 highText.text = highScore.ToString("0000");
	 scoreText.text = score.ToString("0000");
   }
}
