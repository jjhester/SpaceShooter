using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
   public float speed;
   public float tilt;
   public Boundary boundary;
	
   public GameObject shot;
   public Transform shotSpawn;
	
   public float fireRate;
   private float nextFire = 0.0f;
   // Use this for initialization
   void Update () {
	 if (Time.time > nextFire) {
	    Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
	    audio.Play();
	    nextFire = Time.time + fireRate;
	 }
   }
	
   // Update is called once per frame
   void FixedUpdate () {
//		float moveHorizontal = Input.GetAxis("Mouse X");
//		float moveVertical = Input.GetAxis("Mouse Y");
//		
//		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
//		rigidbody.velocity = speed * movement;
//		
//		rigidbody.position = new Vector3(
//			Mathf.Clamp(rigidbody.position.x, boundary.xMin, boundary.xMax),
//			0.0f,
//			Mathf.Clamp(rigidbody.position.z, boundary.zMin, boundary.zMax)
//			);
//		rigidbody.rotation = Quaternion.Euler(0.0f, 0.0f, rigidbody.velocity.x * -tilt);
   }
}

