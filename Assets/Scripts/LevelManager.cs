using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour 
{
	void Awake()
	{
		DontDestroyOnLoad(gameObject);
	}

	void Start()
	{
		if(GameObject.FindGameObjectsWithTag("LevelManager").Length > 1)
		{
			Destroy(gameObject);
		}
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.Alpha1))
		{
			BirdhouseInvestigator.ggLog("BoxSort 1");
			Application.LoadLevel("Boxsort");
		}
		else if(Input.GetKey(KeyCode.Alpha2))
		{
			BirdhouseInvestigator.ggLog("BoxSort 2");
			Application.LoadLevel("Boxsort");
		}
		else if(Input.GetKey(KeyCode.Alpha3))
		{			
			BirdhouseInvestigator.ggLog("BoxSort 3");
			Application.LoadLevel("Boxsort");
		}
		else if(Input.GetKey(KeyCode.Alpha4))
		{
			BirdhouseInvestigator.ggLog("Search 1");
			Application.LoadLevel("Search1");
		}
		else if(Input.GetKey(KeyCode.Alpha5))
		{
			BirdhouseInvestigator.ggLog("Search2");
			Application.LoadLevel("Search2");
		}
		else if(Input.GetKey(KeyCode.Alpha6))
		{
			BirdhouseInvestigator.ggLog("Search3");
			Application.LoadLevel("Search3");
		}
	
	}
}
