using UnityEngine;
using System.Collections;

public class BirdhouseInvestigator : MonoBehaviour {
	public int numBallsFound = 0;
	private int totalBalls = 8;
	public int numVisits = 0;
	public int numRevisitsInARow = 0;
	public int numTargetsFoundBeforeRevisit = 0;
	public bool hasRevisted = false;
	Vector3 previousPosition;
	Quaternion previousRotation; 
	float turnAmount = -360f; // HACK. For some reason always starts at 360 degrees.
	float distanceTravelled = 0.0f;
	
	float startTime;

	GameObject menu;
	TextMesh textMesh;

	bool completedTask = false;
	bool hasSetInitalPosAndRot = false;
	
	HydraDeckCamera hdCam;
	
	void Awake() {
		menu = GameObject.FindGameObjectWithTag("Menu");
		textMesh = menu.transform.Find("text").GetComponent<TextMesh>();
		hdCam = GetComponent<HydraDeckCamera>();
	}

	// Use this for initialization
	void Start () {
		
		textMesh.text = numBallsFound.ToString() + "/" + totalBalls.ToString();
	
	}
	
	// Update is called once per frame
	void Update () {
		if(hdCam.State == HydraDeckCamera.CameraState.Enabled) {
			if(!hasSetInitalPosAndRot) {
				startTime = Time.time;
				previousPosition = hdCam.BodyPosition;	
				previousRotation = hdCam.FullBodyRotation;
				hasSetInitalPosAndRot = true;
			}
			
			distanceTravelled += Vector3.Distance(hdCam.BodyPosition, previousPosition);
			previousPosition = hdCam.BodyPosition;
		
			turnAmount += Quaternion.Angle(hdCam.FullBodyRotation, previousRotation);
			previousRotation = hdCam.FullBodyRotation;
		}
		
		print(distanceTravelled);
		
		if(numBallsFound == 8 && !completedTask) {
			completedTask = true;
			float timeTaken = Time.time - startTime;
			ggLog("Total number of visits: " + numVisits);
			ggLog("Time taken to complete: " + timeTaken.ToString());
			ggLog("Number of targets before revist: " + numTargetsFoundBeforeRevisit);
			ggLog("Number of revisits: " + (numVisits - numBallsFound));
			ggLog("Total Distance Travelled: " + distanceTravelled);
			ggLog("Total Turn Amount: " + turnAmount);
		}
	}
	
	public void incrementBallsFound() {
		numBallsFound++;			
		textMesh.text = numBallsFound.ToString() + "/" + totalBalls.ToString();
	}
	
	public void incrementNumVisits() {
		numVisits++;	
	}
	
	public static void ggLog(string s) {
		Debug.Log(	"******************************************\n" +
					s + "\n" +
					"******************************************");	
	}
		
	
	

	

}
