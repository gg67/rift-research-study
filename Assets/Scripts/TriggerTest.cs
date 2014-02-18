using UnityEngine;
using System.Collections;

public class TriggerTest : MonoBehaviour {
	bool holdingObject = false;
	GameObject heldObject, touchedObject;
	Color objectColor;

	void Update() {
		// Pick up object
		if(SixenseInput.Controllers[1].Trigger > 0 && !holdingObject) {
			pickUpTouchedObject();	
		}

		// Put down object
		if(SixenseInput.Controllers[1].Trigger == 0 && holdingObject) {
			releaseHeldObject();	
		}
	}
	
	void OnTriggerEnter(Collider col) {
		if(col.gameObject.tag == "Box" && !touchedObject) {
			touchedObject = col.gameObject;	
			if(!holdingObject) {
				Color c = touchedObject.renderer.material.color;
				objectColor = c;
				c.a = 0.5f;
				touchedObject.renderer.material.color = c;
			}
		}
	}
	
	void OnTriggerStay(Collider col) {
		if(col.gameObject.tag == "Box" && !touchedObject) {
			touchedObject = col.gameObject;	
			if(!holdingObject) {
				Color c = touchedObject.renderer.material.color;
				objectColor = c;
				c.a = 0.5f;
				touchedObject.renderer.material.color = c;
			}
		}
	}
	
    void OnTriggerExit(Collider col) {
		if(col.gameObject.tag == "Box") {
			if(touchedObject != null) {
				if(!holdingObject) {
					touchedObject.renderer.material.color = objectColor;
				}
				touchedObject = null;
			} else {
				Debug.Log ("Trying to access color of NULL object");
			}
		}
    }
	
	void pickUpTouchedObject() {
		if(touchedObject != null) {
				heldObject = touchedObject;
				heldObject.transform.parent = transform;		
				holdingObject = true;
		} else {
			Debug.Log ("Trying to pick up NULL object");	
		}
	}
	
	void releaseHeldObject() {
		if(heldObject != null) {
			heldObject.transform.parent = null;
			heldObject.renderer.material.color = objectColor;
			holdingObject = false;
		} else {
			Debug.Log ("Trying to put down NULL object");	
		}
	}
}
