using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterObjects
{
	public GameObject balloo;
	public GameObject Lalli;
	public GameObject Bagera;

	public GameObject getCharacter(char ch)
	{
		switch (ch) {
		case 'l':
			return Lalli;
		case 'b':
			return Bagera;
		default:
			return balloo;
		}
	}
}
	
public class Controller : MonoBehaviour {
	private Vector3[] spawnpointposition;
	public CharacterObjects characters;
	public char charactertospawn;

	// Use this for initialization
	void Start () {
		charactertospawn = 'b';

		spawnpointposition = new Vector3[6];

		for (int z = 0; z < spawnpointposition.Length; z++) {
			spawnpointposition[z]= (GameObject.Find ("SpawnPoint"+(z+1))).transform.position;
		}

	}
	

	void Update () {
		if (Input.GetKeyDown (KeyCode.Q)) {
			Quaternion rotateby90=Quaternion.Euler(0.0f,90.0f,0.0f);
			GameObject player=Instantiate(characters.getCharacter(charactertospawn),spawnpointposition[0],rotateby90);
			player.tag="Player1";
		}
		else if (Input.GetKeyDown (KeyCode.E)) {
			Quaternion rotatebynegitive90=Quaternion.Euler(0.0f,-90.0f,0.0f);
			GameObject player=Instantiate(characters.getCharacter(charactertospawn),spawnpointposition[1],rotatebynegitive90);
			player.tag="Player2";
		}
		else if (Input.GetKeyDown (KeyCode.A)) {
			Quaternion rotateby90=Quaternion.Euler(0.0f,90.0f,0.0f);
			GameObject player=Instantiate(characters.getCharacter(charactertospawn),spawnpointposition[2],rotateby90);
			player.tag="Player1";
		}
		else if (Input.GetKeyDown (KeyCode.D)) {
			Quaternion rotatebynegitive90=Quaternion.Euler(0.0f,-90.0f,0.0f);
			GameObject player=Instantiate(characters.getCharacter(charactertospawn),spawnpointposition[3],rotatebynegitive90);
			player.tag="Player2";
		}
		else if (Input.GetKeyDown (KeyCode.Z)) {
			Quaternion rotateby90=Quaternion.Euler(0.0f,90.0f,0.0f);
			GameObject player=Instantiate(characters.getCharacter(charactertospawn),spawnpointposition[4],rotateby90);
			player.tag="Player1";
		}
		else if (Input.GetKeyDown (KeyCode.C)) {
			Quaternion rotatebynegitive90=Quaternion.Euler(0.0f,-90.0f,0.0f);
			GameObject player=Instantiate(characters.getCharacter(charactertospawn),spawnpointposition[5],rotatebynegitive90);
			player.tag="Player2";
		}
	}
}
