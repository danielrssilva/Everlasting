using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Passenger setting")]
public class Passenger : ScriptableObject
{
	public int farmer;
	public int student;
	public int engineer;
	public int unenployed;
	public int researcher;
}
