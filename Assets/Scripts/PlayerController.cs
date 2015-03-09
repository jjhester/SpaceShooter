using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary {
   public float xMin, xMax, zMin, zMax;
}


public class PlayerController : MonoBehaviour {
   public float speed, movementThreshold;
   public float tilt;
   public Boundary boundary;

   public GameObject shot;
   public Transform shotSpawn;

   public float fireRate;
   private float nextFire = 0.0f;

   void Update () {
	 if (Input.GetButton("Fire1") && Time.time > nextFire) {
	    Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
	    GetComponent<AudioSource>().Play();
	    nextFire = Time.time + fireRate;
	 }
   }
   void FixedUpdate () {
	 float moveHorizontal = 0, moveVertical = 0;
	 #if UNITY_EDITOR
	 moveHorizontal = Input.GetAxis("Mouse X");
	 moveVertical = Input.GetAxis("Mouse Y");
	 #elif UNITY_ANDROID
		Touch touch;
		if (Input.touchCount > 0 && (touch = Input.GetTouch(0)).phase == TouchPhase.Moved) {
			Vector2 touchDelta = touch.deltaPosition;
			moveHorizontal = touchDelta.x;
			moveVertical = touchDelta.y;
		}
	 #elif UNITY_IPHONE
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) {
			Vector2 touchDelta = Input.GetTouch(0).deltaPosition;
			moveHorizontal = touchDelta.x;
			moveVertical = touchDelta.y;
		}
	 #endif
	 Vector3 currentPosition = GetComponent<Rigidbody>().position;
	 Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
	 if (Vector3.Distance(currentPosition, movement) > movementThreshold) {
	    GetComponent<Rigidbody>().velocity = Mathf.Abs(speed - movementThreshold) * movement;
	    GetComponent<Rigidbody>().position = new Vector3(
		Mathf.Clamp(GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax),
		0.0f,
		Mathf.Clamp(GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
	    );
	    GetComponent<Rigidbody>().rotation = Quaternion.Euler(0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);
	 }

   }
}
