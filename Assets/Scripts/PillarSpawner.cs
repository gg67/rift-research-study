using UnityEngine;
using System.Collections;

public class PillarSpawner : MonoBehaviour {
	public int trial = 1;
	public const int numPillars = 16;
	public float pillar_y = 1.3f;
	public GameObject pillarPrefab;
	BallRandomizer ballRandomizer;
	Vector2[] poissonDist1 = {	new Vector2(-0.4453674f, 0.6719415f), 		new Vector2(-0.3651899f, 0.1271557f), 	new Vector2(-0.05150136f, 0.9337401f), 	new Vector2 (-0.8322809f, 0.5303273f),
								new Vector2(0.1512863f, 0.5089048f), 		new Vector2(0.2441591f, 0.09209002f), 	new Vector2(0.5560829f, 0.3702246f), 	new Vector2 (0.5338598f, 0.8020244f),
								new Vector2(-0.8913006f, -0.04517709f), 	new Vector2(-0.4065563f, -0.6495742f),	new Vector2(0.6990244f, -0.2753913f), 	new Vector2 (-0.1923878f, -0.273474f),
								new Vector2(-0.0005278484f, -0.9408209f), 	new Vector2(0.2225635f, -0.3394499f), 	new Vector2(0.9564956f, 0.09382996f), 	new Vector2 (0.3833419f, -0.8046775f)
							 };
	
	Vector2[] poissonDist2 = {	new Vector2(-0.3510278f, 0.6162274f), 	new Vector2(-0.9012225f, 0.1643414f), 	new Vector2(-0.1336731f, 0.2049118f), 	new Vector2 (0.04858748f, 0.7452067f),
								new Vector2(0.3557966f, 0.2317625f), 	new Vector2(-0.4963266f, -0.392992f), 	new Vector2(-0.1192183f, -0.6787869f), 	new Vector2 (-0.9032859f, -0.3209043f),
								new Vector2(-0.4921699f, -0.8694036f),	new Vector2(0.4220399f, -0.6944501f), 	new Vector2(0.2404169f, -0.2442553f), 	new Vector2 (0.7890362f, -0.1700012f),
								new Vector2(0.8582896f, 0.4501379f), 	new Vector2(0.4483585f, 0.770358f), 	new Vector2(-0.5106214f, 0.0668392f), 	new Vector2 (0.1424307f, -0.9815171f)
							 };
	
	void Awake() {
		ballRandomizer = GetComponent<BallRandomizer>();
	}
	
	// Use this for initialization
	void Start () {
		Vector2[] poissonDist;	
		if(trial==1)
			poissonDist = poissonDist1;
		else
			poissonDist = poissonDist2;
		
		for(int i=0; i<numPillars; ++i){
			float x = poissonDist[i].x *4;	
			float z = poissonDist[i].y *4;
			Instantiate(pillarPrefab, new Vector3(x,pillar_y,z), new Quaternion());
		}
		
		ballRandomizer.RandomizeBalls();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
