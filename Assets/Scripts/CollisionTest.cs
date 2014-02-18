using UnityEngine;
using System.Collections;

public class CollisionTest : MonoBehaviour {
	bool holdingObject = false;
	GameObject heldObject, touchedObject;

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
	
	void OnCollisionEnter(Collision collision) {
		if(collision.gameObject.tag == "Box") {
			touchedObject = collision.gameObject;	
			if(!holdingObject) {
				touchedObject.renderer.material.color = Color.blue;
			}
		}
	}
	
    void OnCollisionExit(Collision collisionInfo) {
		if(collisionInfo.gameObject.tag == "Box") {
			if(touchedObject != null) {
				if(!holdingObject) {
					touchedObject.renderer.material.color = Color.red;
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
				heldObject.renderer.material.color = Color.green;
				Destroy(heldObject.rigidbody);
				holdingObject = true;
		} else {
			Debug.Log ("Trying to pick up NULL object");	
		}
	}
	
	void releaseHeldObject() {
		if(heldObject != null) {
			heldObject.transform.parent = null;
			heldObject.renderer.material.color = Color.red;
			heldObject.AddComponent<Rigidbody>();
			holdingObject = false;
		} else {
			Debug.Log ("Trying to put down NULL object");	
		}
	}
}
