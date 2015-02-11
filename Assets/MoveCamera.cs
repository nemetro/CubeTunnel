using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MoveCamera : MonoBehaviour {

	public GameObject player;
	public Vector3 startPos;
	public bool stop = false;
	public int score = 0;
	public int additional;
	public Canvas ui;
//	private float timer = 0;
	static public int HS = 0;

	void Start(){
		if(PlayerPrefs.HasKey("CubeTunnelHigh")){
			HS = PlayerPrefs.GetInt("CubeTunnelHigh");
		}
		PlayerPrefs.SetInt ("CubeTunnelHigh", HS);
	}

	void Update () {
		Vector3 newpos = new Vector3(startPos.x, startPos.y, player.transform.position.z+startPos.z);
		if(player.transform.position.z > 10f){
			Text instr = ui.transform.Find("Instructions").gameObject.GetComponent<Text>(); 
			instr.text = "Press\nA W S D\nto change gravity";
		}
		if(player.transform.position.z > 15f){
			Text instr = ui.transform.Find("Instructions").gameObject.GetComponent<Text>(); 
			instr.text = "";
		}

		if(!stop){
//			timer += Time.deltaTime;
			score = ((int)player.transform.position.z)/10 + additional;
			this.transform.position = newpos;
			Text scoreT = ui.transform.Find("Score").gameObject.GetComponent<Text>(); 
			scoreT.text = "Score: " + score;
			Text Hscore = ui.transform.Find("HighScore").gameObject.GetComponent<Text>(); 
			Hscore.text = "High Score: " + PlayerPrefs.GetInt("CubeTunnelHigh");
		}
		else{
			Text restart = ui.transform.Find("Restart").gameObject.GetComponent<Text>(); 
			restart.text = "Press 'R' to retry";
		}

		if(score > PlayerPrefs.GetInt("CubeTunnelHigh")){
			PlayerPrefs.SetInt("CubeTunnelHigh", score);
		}
	}
}
