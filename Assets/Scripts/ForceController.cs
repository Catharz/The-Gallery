using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ForceController : MonoBehaviour
{
	public Transform gravityCentre;
	public List <ConstantForce> forcables;
	public bool invertForce = false;
	public float forceAmount = 9.81f;
	public float distanceMultiplier = 100f;

	// Use this for initialization
	void Start ()
	{
		var list = FindObjectsOfType (typeof(ConstantForce)) as ConstantForce[];
		forcables.AddRange (list);
		if (forcables.Count == 0) {
			forcables.Add (GameObject.FindGameObjectWithTag ("Player").GetComponent<ConstantForce> ());
		}
		if (gravityCentre == null) {
			gravityCentre = GameObject.FindGameObjectWithTag ("Floor").transform;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		foreach (ConstantForce forcable in forcables) {
			var relativePos = gravityCentre.position - forcable.transform.position;
			var distance = relativePos.magnitude;
			var direction = relativePos / distance;
			Vector3 force;
			if (invertForce) {
				force = direction * (-9.81f / (distance / distanceMultiplier));
			} else {
				force = direction * (9.81f / (distance / distanceMultiplier));
			}
			Rigidbody rb = forcable.GetComponent<Rigidbody> ();
			rb.GetComponent<ConstantForce> ().force = force;
		}
	}
}
