using UnityEngine;
using System.Collections;

public class DestroyByDistance : MonoBehaviour {

   // Use this for initialization
   void Start () {
	
   }
	
   // Update is called once per frame
   void Update () {
	 if (this.transform.position.z < -20) {
	    Destroy(this.gameObject);
	 }
   }
}
