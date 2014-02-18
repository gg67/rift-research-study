using UnityEngine;
using System.Collections;

public class BoxRandomizer : MonoBehaviour {
	public GameObject[] boxes;
	public Material[] materials;
	
	// Use this for initialization
	void Start () {
		RandomizeBuiltinArray(boxes);
		for(int i=0; i<materials.Length; ++i) {
			boxes[i].renderer.material = materials[i];	
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
