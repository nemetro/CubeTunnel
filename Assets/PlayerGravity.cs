using UnityEngine;
using System.Collections;

public class PlayerGravity : MonoBehaviour {
	
	public enum gravity {left, right, up, down};
	public float zspeed;
	public float mdist;
	public MoveCamera mainC;
	public MoveLight mainL;
	public bool stop = false;
	public float g, gstart;
	private gravity floor = gravity.down;
	
	void Start(){
		gstart = g;
		//print (this.transform.rigidbody.centerOfMass);
		transform.rigidbody.centerOfMass = new Vector3(0, -0.1f, 0);
	}
	
	void Update(){
		
		if(Input.GetKey(KeyCode.R)){
			g = gstart;
			Physics.gravity = new Vector3(0, -g, 0);
			transform.rigidbody.velocity = new Vector3(0, 0, zspeed);
			Application.LoadLevel("_CubeTunnel");
		}

		if(stop)
			return;

		if((int)this.transform.position.z%5 == 0){
			zspeed += 0.001f;
			g += 0.02f;
		}
		
		if(Input.GetKey(KeyCode.A) && floor != gravity.left){
			Physics.gravity = new Vector3(-g, 0 , 0);
			transform.rigidbody.velocity = new Vector3(0, 0, zspeed);
			floor = gravity.left;
		}
		if(Input.GetKey(KeyCode.W) && floor != gravity.up){
			Physics.gravity = new Vector3(0, g, 0);
			transform.rigidbody.velocity = new Vector3(0, 0, zspeed);
			floor = gravity.up;
		}
		if(Input.GetKey(KeyCode.D) && floor != gravity.right){
			Physics.gravity = new Vector3(g, 0 , 0);
			transform.rigidbody.velocity = new Vector3(0, 0, zspeed);
			floor = gravity.right;
		}
		if(Input.GetKey(KeyCode.S) && floor != gravity.down){
			Physics.gravity = new Vector3(0, -g, 0);
			transform.rigidbody.velocity = new Vector3(0, 0, zspeed);
			floor = gravity.down;
		}
		
		if(Mathf.Abs(this.transform.position.x) > 5 || Mathf.Abs(this.transform.position.y) > 5){
			mainC.stop = true;
			mainL.stop = true;
			stop = true;
		}
	}
	
	void FixedUpdate () {
		
		if(stop) return;
		
		Vector3 newpos = new Vector3(transform.position.x, transform.position.y, transform.position.z+zspeed);
		
		if (Input.GetKey (KeyCode.LeftArrow)) 
		{
			newpos.x -= mdist;
		} 
		if (Input.GetKey (KeyCode.RightArrow))
		{
			newpos.x += mdist;
		} 
		if (Input.GetKey (KeyCode.UpArrow)) 
		{
			newpos.y += mdist;
		}
		if(Input.GetKey(KeyCode.DownArrow))
		{
			newpos.y -= mdist;
		}
		
		this.transform.rigidbody.MovePosition(newpos);
	}
	
	void OnTriggerEnter(Collider other){
		if(other.gameObject.name == "Guard"){
			mainC.stop = true;
			mainL.stop = true;
			stop = true;
		}
		else if(other.gameObject.tag == "Obstacle"){
			switch(other.gameObject.name){
			case "DeathBlock(Clone)":
				mainC.stop = true;
				mainL.stop = true;
				stop = true;
				break;
			case "Points(Clone)":
				mainC.additional += 10;
				Destroy (other.gameObject);
				break;
			case "Slow(Clone)":
				zspeed *= 0.5f;
				g *= 0.1f;
				Destroy (other.gameObject);
				break;
			default:
				print (other.gameObject.name);
				break;
			}
		}
	}
	
}