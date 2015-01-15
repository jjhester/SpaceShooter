using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
   public GameObject hazard;
   public Vector3 spawnValues;
   
   public int hazardCount;
   public float spawnWait;
   public float startWait;
   public float waveWait;

   public GUIText scoreText, restartText, gameOverText;
   public TextMesh nextWaveText;
   private int score, waveCount;
   private bool gameOver, restart;

   void Start () {
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
	    nextWave();
	    
	    
	    for (int i = 0; i < hazardCount * waveCount; i++) {
	    
		  Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
		  Quaternion spawnRotation = Quaternion.identity;
		  GameObject asteroid = Instantiate(hazard, spawnPosition, spawnRotation) as GameObject;
		  asteroid.GetComponent<Mover>().speed *= ((waveCount + 1) / 2);
		  yield return new WaitForSeconds(spawnWait / (waveCount));
	    }
	    nextWaveText.text = "";
	    yield return new WaitForSeconds(waveWait);
	    
	 }
	 restartText.text = "Press FIRE to restart";
	 restart = true;
   }
   void nextWave () {
	 waveCount++;
	 nextWaveText.text = "Wave " + waveCount;
   }
   public void AddScore ( int newScoreValue ) {
	 score += newScoreValue;
	 UpdateScore();
   }
	public void GameOver() {
		gameOverText.text = "Game over, man. Game over!";
		gameOver = true;
	}
   void UpdateScore () {
	 scoreText.text = score.ToString("0000");
   }
}
