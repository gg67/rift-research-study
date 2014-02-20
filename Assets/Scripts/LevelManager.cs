using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

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
		if(Input.GetKey(KeyCode.Alpha0))
		{
			Application.LoadLevel("Playground");
		}
		else if(Input.GetKey(KeyCode.Alpha1))
		{
			Application.LoadLevel("Boxsort");
		}
		else if(Input.GetKey(KeyCode.Alpha2))
		{
			Application.LoadLevel("Search1");
		}
		else if(Input.GetKey(KeyCode.Alpha3))
		{
			Application.LoadLevel("Search2");
		}
	
	}
}
