using UnityEngine;
using System.Collections;
using Bears;


public class WaypointMover : MonoBehaviour
{
	public Vector3[] points;

	public float minSpeed = 0;
	public float maxSpeed = 0;
	public float impulseSpeed = 0.01f;
	public float speedUpPercentage = 0.10f;
	public float slowDownPercentage = 0.20f;
	public float indexThreshold = 0.03f;

	public bool useEasing = false;

	public int currentIndex { protected set; get; }
	public int nextIndex { protected set; get; }

	public float currentSpeed { protected set; get; }


	// Use this for initialization
	void Start ()
	{
		this.currentIndex = 0;
		this.nextIndex = 1;
		this.transform.position = this.points[ this.currentIndex ];
	}
	
	// Update is called once per frame
	void Update () 
	{
		if ( Vector3.Distance ( this.transform.position, this.points[ this.nextIndex ] ) < this.indexThreshold )
		{
			this.currentIndex = this.nextIndex;
			this.nextIndex ++;
			this.nextIndex = this.nextIndex % this.points.Length;
		}

		CalculateSpeed();

		Vector3 direction = ( this.points [ this.nextIndex ] - this.points[ this.currentIndex ] ).normalized;
		this.transform.position += direction * this.currentSpeed * Time.deltaTime;
	}


	protected virtual void CalculateSpeed ()
	{
		float currentDistance = Vector3.Distance ( this.transform.position, this.points[ this.currentIndex ] );
		float totalDistance = Vector3.Distance ( this.points[ this.currentIndex ], this.points[ this.nextIndex ] );
		if ( currentDistance / totalDistance < this.speedUpPercentage )
		{
			float lerpPercentage = currentDistance / ( totalDistance * this.speedUpPercentage );

			if ( this.useEasing )
			{
				lerpPercentage = Easing.EaseOut ( lerpPercentage, EasingType.Quadratic );
			}

			this.currentSpeed = Mathf.Lerp ( this.minSpeed, this.maxSpeed, lerpPercentage );
			this.currentSpeed = Mathf.Max ( this.currentSpeed, this.impulseSpeed );
		}
		else if ( currentDistance / totalDistance > this.slowDownPercentage )
		{
			float slowDownDistance = totalDistance * this.slowDownPercentage;
			float lerpPercentage = (currentDistance - slowDownDistance) / (totalDistance - slowDownDistance);

			if ( this.useEasing )
			{
				lerpPercentage = Easing.EaseIn ( lerpPercentage, EasingType.Cubic );
			}
			
			this.currentSpeed = Mathf.Lerp ( this.maxSpeed, this.minSpeed, lerpPercentage );
		}
		else
		{
			this.currentSpeed = this.maxSpeed;
		}
	}
}
