using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHandler : MonoBehaviour {
	List<CharacterAttrib> characters;
	// Use this for initialization
	void Start () {
		characters = new List<CharacterAttrib> ();
	}

	public void addCharacter(CharacterAttrib obj)
	{
		characters.Add (obj);
	}
	
	// Update is called once per frame
	void Update () {
		foreach(CharacterAttrib obj in characters)
		{
			
		}
	}
}
