using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalooAnimationStateControl : MonoBehaviour {

	private Rigidbody body;
	private Animator anime;
	private CharacterAttrib characterattribs;
	public Vector3 target;
	public bool friendlycollision;
	public Vector3 offsetvector;

	// Use this for initialization
	void Start () {
		friendlycollision = false;
		anime = GetComponent<Animator> ();
		body = GetComponent<Rigidbody> ();
		characterattribs = GetComponent<CharacterAttrib> ();

		//start motion
		anime.SetTrigger ("StartWalking");

		if (transform.rotation == Quaternion.Euler (0.0f, 90.0f, 0.0f)) 
		{
			target = new Vector3 (7.5f,0.0f,0.0f);
		}
		else
		{
			target = new Vector3 (-7.5f,0.0f,0.0f);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		if (!characterattribs.infight) {
			characterattribs.pathfeild.reinit (characterattribs.grid,gameObject.GetInstanceID());

			body.velocity = characterattribs.speed * Vector3.Normalize (target - transform.position);

			Vector2 pos = new Vector2 (transform.position.x, transform.position.z);
			pos = 2.0f*(pos + (new Vector2 (10.0f, 5.0f)));
			Vector2 velocity = characterattribs.pathfeild.getVector ((int)Mathf.Floor (pos.x), (int)Mathf.Floor (pos.y),characterattribs.grid);
			body.velocity = characterattribs.speed *new Vector3(velocity.x,0.0f,velocity.y);
		}
		/*
		//Vector3 velocitydelta = Vector3.Normalize (pointofcontact - transform.position) + characterattribs.speed * Vector3.Normalize (target - transform.position);
		//if(cosT==1.0f)
		Vector3 velocitydelta = offsetvector +body.velocity;

		body.velocity = characterattribs.speed*Vector3.Normalize(velocitydelta);

		friendlycollision = false;
		offsetvector =new Vector3(0.0f ,0.0f ,0.0f );
		*/
	}


	void OnTriggerEnter(Collider othercollider)
	{
		string tagname=gameObject.tag;
		int playernum = int.Parse (""+tagname[tagname.Length - 1]);
		playernum = playernum % 2 +1;
		string tagother = tagname.Substring (0,("Player").Length);
		tagother += playernum;

		if (othercollider.tag == tagother && !characterattribs.infight) {
			anime.SetTrigger ("StartAttack");
			body.velocity = new Vector3 (0.0f, 0.0f, 0.0f);
			characterattribs.infight = true;
			characterattribs.opponentattribs = othercollider.gameObject.GetComponent<CharacterAttrib> ();

		} 
	}

	void OnTriggerStay(Collider othercollider)
	{
		if (!characterattribs.infight && othercollider.tag == gameObject.tag) 
			offsetvector+=new Vector3(0.0f,0.0f,1.0f);
	}

	void attackEffect()
	{
		characterattribs.opponentattribs.totallife -= characterattribs.attackpower;
		characterattribs.opponentattribs.charactereffects.blinker.running = true;
	}
}
