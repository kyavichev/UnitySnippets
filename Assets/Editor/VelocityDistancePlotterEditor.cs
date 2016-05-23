using UnityEngine;
using UnityEditor;
using System.Collections;


[CustomEditor(typeof(VelocityDistancePlotter))]
public class VelocityDistancePlotterEditor : Editor 
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		
		EditorGUILayout.BeginHorizontal();
		if ( GUILayout.Button ( "Generate", GUILayout.MaxWidth( 100 ) ) )
		{
			GenerateNew();
		}
		EditorGUILayout.EndHorizontal();
	}

	protected virtual void GenerateNew ()
	{
		(this.target as VelocityDistancePlotter).CreateLine();
	}
}
