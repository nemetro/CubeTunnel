using UnityEngine;
using System.Collections;

public class MoveLight: MonoBehaviour {
	
	public GameObject player;
	public bool stop = false;
	
	void Update () {
		Vector3 newpos = new Vector3(0, 0, 1);

		//Vector3 newpos = (player.transform.up + player.transform.forward) * 1.1f;
		newpos.z = newpos.z + player.transform.position.z;
		if(!stop)
			this.transform.position = newpos;
	}
}
