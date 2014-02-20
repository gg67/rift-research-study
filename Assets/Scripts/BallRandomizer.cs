using UnityEngine;
using System.Collections;

public class BallRandomizer : MonoBehaviour {
	GameObject[] pillars;
	
	public void RandomizeBalls() {
		pillars = GameObject.FindGameObjectsWithTag("Pillar");
		RandomizeBuiltinArray(pillars);
		for(int i=0; i<pillars.Length/2; ++i) {
			Transform sphere = pillars[i].transform.Find("outhouse1").Find ("Sphere");
			sphere.renderer.material.color = Color.blue;
			sphere.renderer.enabled = false;
		}
		
		for(int i=pillars.Length/2; i<pillars.Length; ++i) {
			Transform sphere = pillars[i].transform.Find("outhouse1").Find ("Sphere");
			sphere.renderer.material.color = Color.red;
			sphere.renderer.enabled = false;
		}	
	}
	
	static void RandomizeBuiltinArray(Object[] arr) {
	    for (var i = arr.Length - 1; i > 0; i--) {
	        var r = Random.Range(0,i);
	        var tmp = arr[i];
	        arr[i] = arr[r];
	        arr[r] = tmp;
	    }
	}
}
