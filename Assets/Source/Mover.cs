using UnityEngine;
using System.Collections;
using Bears;

public class Mover : MonoBehaviour 
{
	public VelocityDistancePlotter velocityDistancePlotter;

	public float impulseSpeed = 0.01f;
	public float currentVelocity { protected set; get; }


	// Use this for initialization
	void Start () 
	{
	
	}

	// Update is called once per frame
	void Update ()
	{
		Vector3 destination = new Vector3 ( this.velocityDistancePlotter.distance, 0, 0 );
		//float currentDistance = Vector3.Distance ( this.transform.position, destination );
		float currentDistance = Vector3.Distance ( Vector3.zero, this.transform.localPosition );
		if ( currentDistance < this.velocityDistancePlotter.speedUpDistance )
		{
			float lerpPercentage = currentDistance / this.velocityDistancePlotter.speedUpDistance;

			if ( this.velocityDistancePlotter.useEasing )
			{
				lerpPercentage = Easing.EaseOut ( lerpPercentage, EasingType.Quadratic );
			}

			this.currentVelocity = Mathf.Lerp ( 0, this.velocityDistancePlotter.maxVelocity, lerpPercentage );
			this.currentVelocity = Mathf.Max ( this.currentVelocity, this.impulseSpeed );
		}
		else if ( currentDistance > (this.velocityDistancePlotter.distance - this.velocityDistancePlotter.slowDownDistance) )
		{
			float lerpPercentage = (currentDistance - (this.velocityDistancePlotter.distance - this.velocityDistancePlotter.slowDownDistance)) / this.velocityDistancePlotter.slowDownDistance;

			if ( this.velocityDistancePlotter.useEasing )
			{
				lerpPercentage = Easing.EaseIn ( lerpPercentage, EasingType.Cubic );
			}

			this.currentVelocity = Mathf.Lerp ( this.velocityDistancePlotter.maxVelocity, 0, lerpPercentage );
		}
		else
		{
			this.currentVelocity = this.velocityDistancePlotter.maxVelocity;
		}

		Vector3 direction = new Vector3 ( 1, 0, 0 );

		this.transform.localPosition += direction * this.currentVelocity * Time.deltaTime;
	}
}
