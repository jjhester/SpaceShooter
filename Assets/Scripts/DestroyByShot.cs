using UnityEngine;
using System.Collections;

public class DestroyByShot : MonoBehaviour {
	
   public GameObject playerExplosion;
   private GameController gameController;
	
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
			if (other.tag == "Player") {
				Instantiate(playerExplosion, transform.position, transform.rotation);
				gameController.GameOver();
			
			Destroy(other.gameObject);
			Destroy(gameObject);
		} else return;
	}
}

