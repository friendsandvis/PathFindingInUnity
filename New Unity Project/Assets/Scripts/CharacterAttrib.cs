using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class CharacterAttrib : MonoBehaviour {

	public float totallife;
	public float attackpower;
	public float speed;
	public int obsticleradius;
	public int obsticleweight;

	public bool infight;
	public CharacterAttrib opponentattribs;
	public CharacterMeshEffects charactereffects;
	public ShortPathFeild pathfeild;
	public Grid grid;

	// Use this for initialization
	void Start () 
	{
		infight = false;
		charactereffects = GetComponentInChildren<CharacterMeshEffects> ();
		GameObject tem = GameObject.FindGameObjectWithTag ("BattleController");
		pathfeild = tem.GetComponent<ShortPathFeild> ();

		grid=pathfeild.initGrid ();
		for(int z=0;z<18;z++)
		grid.obsticles.Add (new Obsticle(20,z,0));
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		//Vector2 pos = new Vector2 (transform.position.x, transform.position.z);
		//pos = 8.0f*(pos + (new Vector2 (10.0f, 5.0f)));
		//int x=(int)Mathf.Floor(pos.x),z=(int)Mathf.Floor(pos.y);

		//pathfeild.addObsticle (x, z, obsticleweight);
	}

	void LateUpdate()
	{
		Vector2 pos = new Vector2 (transform.position.x, transform.position.z);
		pos = 2.0f*(pos + (new Vector2 (10.0f, 5.0f)));
		int x=(int)Mathf.Floor(pos.x),z=(int)Mathf.Floor(pos.y);
		bool pathblocked=pathfeild.addCharacterObsticle (x,z,obsticleradius,gameObject.GetInstanceID());

		if (pathblocked)
			this.gameObject.SetActive (false);
	}


}
