using UnityEngine;
using System.Collections;

public class MakeMap : MonoBehaviour {

	public GameObject top, bottom, left, right;
	public GameObject player;
	public GameObject ObsPref, PointsPref, SlowPref;
	public int z = 90;
	public int zObs = 110;
	
	// Update is called once per frame
	void Update () {

		if(player.transform.position.z < 50)
			return;

		if(player.transform.position.z < z)
			return;

		bool usedT = false, usedB = false, usedL = false, usedR = false;
		var number = Random.Range(1,4);
		int count = 0;
		while(count < number){
			var num = Random.Range(0,4);
			switch(num){
			case 0:
				if(!usedT){
					GameObject wall = Instantiate(top) as GameObject;
					wall.transform.position = new Vector3(wall.transform.position.x,
					                                      wall.transform.position.y,
					                                      wall.transform.position.z + z + 25);
					usedT = true;
					count++;
				}
				break;
			case 1:
				if(!usedB){
					GameObject wall = Instantiate(bottom) as GameObject;
					wall.transform.position = new Vector3(wall.transform.position.x,
					                                      wall.transform.position.y,
					                                      wall.transform.position.z + z + 25);
					usedB = true;
					count++;
				}
				break;
			case 2:
				if(!usedL){
					GameObject wall = Instantiate(left) as GameObject;
					wall.transform.position = new Vector3(wall.transform.position.x,
					                                      wall.transform.position.y,
					                                      wall.transform.position.z + z + 25);
					usedL = true;
					count++;
				}
				break;
			case 3:
				if(!usedR){
					GameObject wall = Instantiate(right) as GameObject;
					wall.transform.position = new Vector3(wall.transform.position.x,
					                                      wall.transform.position.y,
					                                      wall.transform.position.z + z + 25);
					usedR = true;
					count++;
				}
				break;
			default:
				print (num);
				break;
			}
		}
		z+=10;

		if(player.transform.position.z < zObs) return;

		var x = Random.Range(-5.0f,5.0f);
		var y = Random.Range(-5.0f,5.0f);

		GameObject obs = Instantiate(ObsPref, new Vector3(x, y, z + 25), Quaternion.Euler(45f,45f,0)) as GameObject;
		obs.transform.localScale = new Vector3(Random.Range(0.5f,2.0f), Random.Range(0.5f,2.0f), Random.Range(0.5f,2.0f));

		int item = Random.Range(0,3);
		x = Random.Range(-5.0f,5.0f);
		y = Random.Range(-5.0f,5.0f);

		if(item == 1){
			Instantiate(PointsPref, new Vector3(x, y, z + Random.Range(20.0f,35.0f)), Quaternion.Euler(45f,45f,0));
		}
		else if(item == 2){
			Instantiate(SlowPref, new Vector3(x, y, z + Random.Range(20.0f,35.0f)), Quaternion.Euler(45f,45f,0));
		}

		zObs += 75;
	
	}
}
