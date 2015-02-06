using UnityEngine;
using System.Collections;

public class Scroller : MonoBehaviour {
   private float speed;
   public float tileSizeZ;
   private bool shouldScroll;
	
   private Vector3 startPosition;
	
   void Start () {
	 startPosition = transform.position;
   }
	
   void Update () {
	 if (shouldScroll) {
	    float newPosition = Mathf.Repeat(Time.time * speed, tileSizeZ);
	    transform.position = startPosition + Vector3.forward * newPosition;
	 }
   }
   public float StartScroll ( float speed ) {
	 this.speed = speed;
	 shouldScroll = true;
      return this.speed;
   }
   public void StopScroll () {
	 this.speed = 0.0f;
	 shouldScroll = false;
   }
}
