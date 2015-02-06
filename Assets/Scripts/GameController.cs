using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
   public GameObject[] hazards;
   public GameObject[] enemies;
   public Vector3 spawnValues;
   
   public int hazardCount;
   public int enemyCount;
   public float spawnWait;
   public float startWait;
   public float waveWait;
   public float waveSpeed;

   public GUIText scoreText, restartText, gameOverText, nextWaveText;
   private int score, waveCount;
   private bool gameOver, restart;
   private Scroller backgroundScroller;

   void Start () {
	 //get backgroundScroller reference
	 GameObject backgroundScrollerObject = GameObject.FindWithTag("Background");
	 if (backgroundScrollerObject != null) {
	    backgroundScroller = backgroundScrollerObject.GetComponent<Scroller>();
	 } else {
	    Debug.Log("Cannot find 'BackgroundScroller' script");
	 }
	 backgroundScroller.StartScroll(-waveSpeed);
	 score = 0;
	 waveCount = 0;
	 gameOverText.text = "";
	 restartText.text = "";
	 nextWaveText.text = "";
	 gameOver = false;
	 restart = false;
	 UpdateScore();
	 StartCoroutine(SpawnWaves());

   }
   void Update () {
	 if (restart && Input.GetButton("Fire1")) {
	    Application.LoadLevel(Application.loadedLevel);
	 }
   }
   IEnumerator SpawnWaves () {
	 yield return new WaitForSeconds(startWait);
	 while (!gameOver) {
	    if (waveCount > 0) {
		  audio.Play();
	    }
	    NextWave();
	    for (int i = 0; i < enemyCount * waveCount; i++) {
		  Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
		  Quaternion spawnRotation = Quaternion.AngleAxis(180, Vector3.down);
		  GameObject enemy = Instantiate(enemies [Random.Range(0, enemies.Length - 1)], spawnPosition, spawnRotation) as GameObject;
		  enemy.GetComponent<Mover>().speed = -1;
		  yield return new WaitForSeconds(spawnWait);
		  nextWaveText.text = "";
	    }    
	    
	    for (int i = 0; i < hazardCount * waveCount; i++) {
		  Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
		  Quaternion spawnRotation = Quaternion.identity;
		  GameObject asteroid = Instantiate(hazards [Random.Range(0, hazards.Length - 1)], spawnPosition, spawnRotation) as GameObject;
		  asteroid.GetComponent<Mover>().speed = Random.Range(-waveSpeed * waveCount, -waveSpeed);
		  yield return new WaitForSeconds(spawnWait);
	    }
	    yield return new WaitForSeconds(waveWait);
	    
	 }
	 restartText.text = "Tap to restart";
	 restart = true;
   }
   void NextWave () {
	 waveCount++;
	 nextWaveText.text = "Wave " + waveCount;
   }
   public void AddScore ( int newScoreValue ) {
	 score += newScoreValue;
	 UpdateScore();
   }
	public void GameOver() {
		nextWaveText.text = "";
		gameOverText.text = "Game over,\n man.";
		gameOver = true;
	}
   void UpdateScore () {
	 scoreText.text = score.ToString("0000");
   }
}
