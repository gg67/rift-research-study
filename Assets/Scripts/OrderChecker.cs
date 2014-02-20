using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OrderChecker : MonoBehaviour {
	public Transform startMarker;
	public Transform endMarker;
	public float speed = 1.0f;
	private float startTime;
	private float journeyLength;
	public float smooth = 5.0f;

	public List<string> sortedBoxes;
	public List<string> boxes;

	private Timer timer;

	void Awake()
	{
		timer = GameObject.FindGameObjectWithTag("Timer").GetComponent<Timer>();
	}

	void Start()
	{
		startMarker.position = transform.position;
		startTime = Time.time;
		journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
	}

	void Update()
	{
		float distCovered = (Time.time - startTime) * speed;
		float fracJourney = distCovered / journeyLength;
		transform.position = Vector3.Lerp(startMarker.position, endMarker.position, fracJourney);

		if(BoxesSorted())
		{
			timer.StopTimer();
			BirdhouseInvestigator.ggLog("Box Sorting Time: " + timer.time);
			Application.LoadLevel("Playground");
		}

		if(fracJourney >= 1)
			Reset();
	}

	void Reset()
	{
		transform.position = startMarker.position;
		startTime = Time.time;
		boxes.Clear();
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Box")
		{
			boxes.Add(other.renderer.material.name);
		}
	}

	bool BoxesSorted()
	{
		return CompareLists(sortedBoxes, boxes);
	}

	bool CompareLists(List<string> a, List<string> b)
	{
		if(a.Count != b.Count) return false;

		for(int i=0; i<a.Count; i++)
		{
			if(a[i] != b[i])
				return false;
		}

		return true;
	}
}
