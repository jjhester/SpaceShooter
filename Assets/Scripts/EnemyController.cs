using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
   public float speedFactor;
	
   public GameObject shot;
   public Transform shotSpawn;
   public float tilt;
   public int rotationRange;
   public bool changesDirection;
	
   public float fireRate;
   private float nextFire = 0.0f;
   private float targetRotation;
   private float slide, slideMoves, slideDuration;
   private int[] slideDistribution;

   // Use this for initialization
   void Update () {
	 if (Time.time > nextFire) {
	    GameObject shotInstance = Instantiate(shot, shotSpawn.position, shotSpawn.rotation) as GameObject;
	    shotInstance.GetComponent<Mover>().speed = 2 * speedFactor;
	    GetComponent<AudioSource>().Play();
	    nextFire = Time.time + fireRate;
	 }
	 Move();
   }
   void Start () {
	 //rotateDirection();
	 slideDistribution = new int[]{-1,1};
	 targetRotation = Random.Range(180 - rotationRange, 180 + rotationRange);
	 //rigidbody.angularVelocity = Random.insideUnitSphere * 10;
	 SetNewDirection();
   }
   void Move () {
	 //transform.Rotate(Vector3.up, Time.deltaTime);
	 GetComponent<Rigidbody>().velocity = transform.forward * speedFactor;
	 GetComponent<Rigidbody>().rotation = Quaternion.Euler(0.0f, targetRotation, GetComponent<Rigidbody>().velocity.x * -tilt);
	 
	 if (changesDirection) {
	    targetRotation += slide;
	    if (slideMoves >= slideDuration) {
		  SetNewDirection();
	    }
	    slideMoves++;
	 }
   }
   void SetNewDirection () {
	 this.slide = slideDistribution [Random.Range(0, 2)];
	 this.slideDuration = Random.Range(rotationRange, rotationRange * 2);
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

