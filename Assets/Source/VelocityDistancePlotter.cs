using UnityEngine;
using System.Collections;
using Bears;


public class VelocityDistancePlotter : MonoBehaviour 
{
	public float maxVelocity = 25;
	public float currentVelocity { protected set; get; }


	public float distance = 10f;
	public float speedUpDistance = 1;
	public float slowDownDistance = 1.5f;


	public LineRenderer lineRenderer;
	public int pointCount = 128;

	public bool useEasing = false;


	void Awake ()
	{
		//this.currentVelocity = this
	}

	// Use this for initialization
	void Start () 
	{
		this.lineRenderer = GetComponent < LineRenderer > ();

		CreateLine();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
	
	}


	public virtual void CreateLine ()
	{
		this.lineRenderer.SetVertexCount ( this.pointCount );

		Vector3 startPosition = Vector3.zero;
		Vector3 endPosition = new Vector3 ( this.distance, 0, 0 );
		Vector3 direction = new Vector3 ( this.distance / (float)this.pointCount, 0, 0 );

		for ( int i = 0; i < this.pointCount; i++ )
		{
			float percentage = (float)i / (float)this.pointCount;

			//Debug.Log ( "Current velocity: " + this.currentVelocity );



			float currentDistance = this.distance * percentage;
			if ( currentDistance < this.speedUpDistance )
			{
				float lerpPercentage = currentDistance / this.speedUpDistance;

				if ( this.useEasing )
				{
					lerpPercentage = Easing.EaseOut ( lerpPercentage, EasingType.Quadratic );
				}

				this.currentVelocity = Mathf.Lerp ( 0, this.maxVelocity, lerpPercentage );
			}
			else if ( currentDistance > (this.distance - this.slowDownDistance) )
			{
				float lerpPercentage = (currentDistance - (this.distance - this.slowDownDistance)) / this.slowDownDistance;

				if ( this.useEasing )
				{
					lerpPercentage = Easing.EaseIn ( lerpPercentage, EasingType.Cubic );
				}

				this.currentVelocity = Mathf.Lerp ( this.maxVelocity, 0, lerpPercentage );
			}
			else
			{
				this.currentVelocity = this.maxVelocity;
			}

			Vector3 currentPosition = startPosition + direction * i;
			Vector3 velocityOffset = new Vector3 ( 0, this.currentVelocity, 0 );
			
			this.lineRenderer.SetPosition ( i, currentPosition + velocityOffset );
		}
	}
}
