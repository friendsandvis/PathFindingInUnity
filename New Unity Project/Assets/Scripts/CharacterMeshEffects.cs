using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blinker
{
	public bool running;
	public int loop;
	public float currenttime,totaltime;
	public bool emit;

	public void reset()
	{
		running = false;
		loop = 2;
		currenttime = 0.0f;
		totaltime = 0.1f;
		emit = false;
	}

	public void update()
	{
		if (running) {
			if(!emit)
				currenttime += Time.deltaTime;
			else
				currenttime -= Time.deltaTime;

			if (currenttime >= totaltime) {
				currenttime = totaltime;
				emit = !emit;
				loop--;
			} else if (currenttime <= 0.0) {
				currenttime = 0.0f;
				emit = !emit;
				loop--;
			}

			if (loop <= 0)
				reset ();
		}
	}
}

public class CharacterMeshEffects : MonoBehaviour {

	public Blinker blinker;
	public SkinnedMeshRenderer characterrenderer;
	private Shader baseshader;

	// Use this for initialization
	void Start () {
		blinker = new Blinker ();
		blinker.reset ();

		characterrenderer=gameObject.GetComponent<SkinnedMeshRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		blinker.update ();

		characterrenderer.material.SetColor ("_EmissionColor", new Color (0.5f*(blinker.currenttime/blinker.totaltime),0.0f, 0.0f));	
	}
}
