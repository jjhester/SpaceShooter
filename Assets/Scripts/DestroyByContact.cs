using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {
   public GameObject explosion;
   public GameObject playerExplosion;
   private GameController gameController;
   public int scoreValue;

   void Start () {
	 //get gameController reference
	 GameObject gameControllerObject = GameObject.FindWithTag("GameController");
	 if (gameControllerObject != null) {
	    gameController = gameControllerObject.GetComponent<GameController>();
	 } else {
	    Debug.Log("Cannot find 'GameController' script");
	 }
   }
  
   void OnTriggerEnter ( Collider other ) {
    if (other.tag != "Boundary" && other.tag != "Asteroid" && other.tag != "Enemy" && other.tag != "EnemyBolt") {
	   Debug.Log(this.name + " - " + other.name);
        Instantiate(explosion, transform.position, transform.rotation);
        if (other.tag == "Player") {
        	Instantiate(playerExplosion, transform.position, transform.rotation);
		gameController.GameOver();
       }
	   gameController.AddScore(scoreValue);
        Destroy(other.gameObject);
        Destroy(gameObject);
    } else return;
  }
}
