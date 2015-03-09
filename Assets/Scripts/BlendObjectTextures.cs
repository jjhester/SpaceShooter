using UnityEngine;
using System.Collections;

enum Direction {
   up,
   down
}

public class BlendObjectTextures : MonoBehaviour {

   public float delay, step;
   public bool blendTextures = true;
   private Direction blendDirection;
   private float blendAmount;
   void Start () {
	 StartCoroutine(BlendTextures());
   }
   void Update () {
	 //only update display on every frame
   }
   IEnumerator BlendTextures () {
	 while (blendTextures) {
	    //Debug.Log("Blend Amount - " + blendAmount);
	    yield return new WaitForSeconds(delay);
	    if (blendAmount <= 0) { //Blend from first into second texture
		  blendDirection = Direction.up; //The default
	    }
	    if (blendAmount >= 1) { //Blend from second into first texture
		  blendDirection = Direction.down;
	    }
	    if (blendDirection == Direction.up) {
		  blendAmount += step;  //Increment blend into second texture
	    } else {
		  blendAmount -= step;  //Decrement blend back into first texture
	    }
	    GetComponent<Renderer>().material.SetFloat("_Blend", blendAmount);
	 }
   }
}
