using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Speed modifiers")]
public class SpeedModifiers : ScriptableObject
{
	public float track;
	public float engine;
	public float wheight;
	public float destinations;
	private float initial;

	public SpeedModifiers()
	{
		track = 0;
		engine = 0;
		wheight = 0;
		destinations = 0;
	}
}