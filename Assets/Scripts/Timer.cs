using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {
	float startTime;
	float stopTime;

	public float time { get { return (stopTime - startTime); } }

	public void StartTimer()
	{
		startTime = Time.time;

	}

	public void StopTimer()
	{
		stopTime = Time.time;
	}
	
}
