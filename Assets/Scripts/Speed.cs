using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Speed setting")]
public class Speed : ScriptableObject
{
	public float initial;
	public float current;

	public Speed()
	{
		initial = 100;
		current = initial;
	}
}