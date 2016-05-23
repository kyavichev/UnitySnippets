using UnityEngine;
using System.Collections;

public class AcceleratingObject : MonoBehaviour 
{
	[Range(-5,5)]
	public float acceleration = 0.0f;
	
	public Vector3 forward = new Vector3 ( 1, 0, 0 );
	public float currentSpeed;


	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		this.currentSpeed += this.acceleration * Time.deltaTime;
		this.transform.position += this.currentSpeed * this.forward;
	}
}
