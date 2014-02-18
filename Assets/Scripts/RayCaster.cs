using UnityEngine;
using System.Collections;

public class RayCaster : MonoBehaviour {
	Transform rayOrigin;
	RaycastHit hit;
	GameObject currentSelection;
	MeshRenderer lastMeshSelected;
	BirdhouseInvestigator investigator;
	HydraDeckCamera hdCam;

	void Awake() {
		GameObject[] mainCameras = GameObject.FindGameObjectsWithTag("MainCamera");
		rayOrigin = mainCameras[0].transform;
		investigator = GameObject.FindGameObjectWithTag("Player").GetComponent<BirdhouseInvestigator>();
		hdCam = GetComponent<HydraDeckCamera>();
	}

	// Use this for initialization
	void Start () {
		GetComponent<OVRMainMenu>().ToggleCrossHair();

	}
	
	// Update is called once per frame
	void Update () {
		Debug.DrawRay(rayOrigin.position, rayOrigin.forward*50, Color.red);

		if(hdCam.State == HydraDeckCamera.CameraState.Enabled) {
			checkForRayMenuCollisions();	
		}
	}
	
	void checkForRayMenuCollisions() {
		if(Physics.Raycast(rayOrigin.position, rayOrigin.forward, out hit, 1)) {
			if(hit.collider.gameObject.tag == "Searchable") {
				GameObject go = hit.collider.gameObject;
				if(SixenseInput.Controllers[1].Trigger > 0 && go != currentSelection) {
					currentSelection = go;
					StartCoroutine("revealHiddenObject", go);
				}
			}
		}	
	}
	
	IEnumerator revealHiddenObject(GameObject go) {
		Transform sphere = go.transform.Find("Sphere");
		sphere.renderer.enabled = true;
	
		// Used to track which monuments have been revisited
		PillarData pData = go.transform.parent.GetComponent<PillarData>();
		if(!pData.visited) {
			pData.visited = true;	
			investigator.numRevisitsInARow = 0;
		} else {
			investigator.numRevisitsInARow++;
			investigator.hasRevisted = true;
		}
		
		// Count number of visits and number of red balls found
		investigator.incrementNumVisits();	
		if(sphere.renderer.material.color == Color.red) {
			investigator.incrementBallsFound();
			if(!investigator.hasRevisted) {
				investigator.numTargetsFoundBeforeRevisit++;	
			}
		}
		
		// Make birdhouse transparent
		Color c = go.renderer.material.color;
		c.a = 0.5f;
		go.renderer.material.color = c;
		
		// Wait for 3 seconds
		yield return new WaitForSeconds(3.0f);
		
		// Make birdhouse opaque again
		c.a = 1.0f;
		go.renderer.material.color = c;
	
		// Change red ball to blue
		if(sphere.renderer.material.color == Color.red) {
			changeBallColor(go.transform.Find("Sphere"), Color.blue);
		}
		sphere.renderer.enabled = false;
		currentSelection = null;
	}
	
	void changeBallColor(Transform ball, Color c) {
		ball.renderer.material.color = c;
	}
	
}
