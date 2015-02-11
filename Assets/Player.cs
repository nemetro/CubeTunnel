using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public enum bottom {left, right, up, down};
	public float zspeed;
	public float dist, mdist;
	public bottom floor = bottom.down;
	public bool onGround, lockL, lockR, lockU, lockD;
	public MoveCamera mainC;
	public MoveLight mainL;
	public bool stop = false;

	void Start(){
		lockL = false;
		lockR = false;
		lockU = false;
		lockD = false;
		print (Physics.gravity);
	}

	void Update(){

		if(stop)
			return;

//		if(this.transform.position.z > )

		if((int)this.transform.position.z%10 == 0)
			zspeed += .02f;

		if(Input.GetKey(KeyCode.R)){
			Application.LoadLevel("_CubeTunnel");
		}

		if(Input.GetKey(KeyCode.Alpha1)){
			//Rotate
			if(floor != bottom.left){
				if(floor == bottom.up || floor == bottom.down){
					onGround = lockL;
					lockL = false;
				}
				else
					onGround = false;
				
				print ("to left");
				floor = bottom.left;
			}
		}
		if(Input.GetKey(KeyCode.Alpha2)){
			//Rotate
			if(floor != bottom.up){
				if(floor == bottom.left || floor == bottom.down){
					onGround = lockU;
					lockU = false;
				}
				else
					onGround = false;
				floor = bottom.up;
			}
		}
		if(Input.GetKey(KeyCode.Alpha3)){
			//Rotate
			if(floor != bottom.right){
				if(floor == bottom.up || floor == bottom.down){
					onGround = lockR;
					lockU = false;
				}
				else
					onGround = false;
				print ("to right");
				floor = bottom.right;
			}
		}
		if(Input.GetKey(KeyCode.Alpha4)){
			//Rotate
			if(floor != bottom.down){
				if(floor == bottom.left || floor == bottom.down){
					onGround = lockD;
					lockD = false;
				}
				else
					onGround = false;
				floor = bottom.down;
			}
		}

		if(Mathf.Abs(this.transform.position.x) > 10 || Mathf.Abs(this.transform.position.y) > 10){
			mainC.stop = true;
			mainL.stop = true;
			stop = true;
		}
	}

	void FixedUpdate () {

		if(stop) return;

		Vector3 newpos = new Vector3(transform.position.x, transform.position.y, transform.position.z+zspeed);

		if (Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.A)) 
		{
			if((floor == bottom.down || floor == bottom.up) && !lockL){
				newpos.x -= mdist;
			}
		} 
		if (Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.D)) 
		{
			if((floor == bottom.down || floor == bottom.up) && !lockR){
				newpos.x += mdist;
			}
		} 
		if (Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.W)) 
		{
			if((floor == bottom.left || floor == bottom.right && !lockD) && !lockU){
				newpos.y += mdist;
			}
		}
		if(Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)){
			if((floor == bottom.left || floor == bottom.right && !lockD) && !lockD){
				newpos.y -= mdist;
			}
		}
		if(!onGround){
			switch(floor){
			case bottom.left:
				newpos.x -= dist;
				if(newpos.y > 0)newpos.y -= 0.01f;
				if(newpos.y < 0)newpos.y += 0.01f;
				break;
			case bottom.right:
				newpos.x += dist;
				if(newpos.y > 0)newpos.y -= 0.01f;
				if(newpos.y < 0)newpos.y += 0.01f;
				break;
			case bottom.up:
				newpos.y += dist;
				if(newpos.x > 0)newpos.x -= 0.01f;
				if(newpos.x < 0)newpos.x += 0.01f;
				break;
			case bottom.down:
				newpos.y -= dist;
				if(newpos.x > 0)newpos.x -= 0.01f;
				if(newpos.x < 0)newpos.x += 0.01f;
				break;
			default: break;
			}
		}
		transform.position = newpos;
	}

	void OnTriggerEnter(Collider other){
		

		switch(floor){
		case bottom.left:
			if(other.gameObject.tag == "LeftWall"){
				onGround = true;
			}
			else if(other.gameObject.tag == "TopWall"){
				lockU = true;
			}
			else if(other.gameObject.tag == "BottomWall"){
				lockD = true;
			}
			break;
		case bottom.right:
			if(other.gameObject.tag == "RightWall"){
				onGround = true;
			}
			else if(other.gameObject.tag == "TopWall"){
				lockU = true;
			}
			else if(other.gameObject.tag == "BottomWall"){
				lockD = true;
			}
			break;
		case bottom.up:
			if(other.gameObject.tag == "TopWall"){
				onGround = true;
			}
			else if(other.gameObject.tag == "LeftWall"){
				lockL = true;
			}
			else if(other.gameObject.tag == "RightWall"){
				lockR = true;
			}
			break;
		case bottom.down:
			if(other.gameObject.tag == "BottomWall"){
				onGround = true;
			}
			else if(other.gameObject.tag == "LeftWall"){
				lockL = true;
			}
			else if(other.gameObject.tag == "RightWall"){
				lockR = true;
			}
			break;
		default: break;
		}
		//transform.position = newpos;
	}
	
	void OnTriggerExit(Collider other){
		switch(floor){
		case bottom.left:
			if(other.gameObject.tag == "LeftWall"){
				onGround = false;
			}
			else if(other.gameObject.tag == "TopWall"){
				lockU = false;
			}
			else if(other.gameObject.tag == "BottomWall"){
				lockD = false;
			}
			break;
		case bottom.right:
			if(other.gameObject.tag == "RightWall"){
				onGround = false;
			}
			else if(other.gameObject.tag == "TopWall"){
				lockU = false;
			}
			else if(other.gameObject.tag == "BottomWall"){
				lockD = false;
			}
			break;
		case bottom.up:
			if(other.gameObject.tag == "TopWall"){
				onGround = false;
			}
			else if(other.gameObject.tag == "LeftWall"){
				lockL = false;
			}
			else if(other.gameObject.tag == "RightWall"){
				lockR = false;
			}
			break;
		case bottom.down:
			if(other.gameObject.tag == "BottomWall"){
				onGround = false;
			}
			else if(other.gameObject.tag == "LeftWall"){
				lockL = false;
			}
			else if(other.gameObject.tag == "RightWall"){
				lockR = false;
			}
			break;
		default: break;
		}
	}

}
