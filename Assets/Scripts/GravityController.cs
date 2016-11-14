using UnityEngine;
using System.Collections;

public class GravityController : MonoBehaviour
{

	public bool invertGravity;
	public float gravityMultiplier = 1f;
	public Transform gravityCentre;
	public GameObject player;

	public bool debug = false;

	public bool applyForceToPlayer = true;
	public bool adjustGravity = false;
	public bool adjustRotation = false;

	public float rotationSmoothing = 1f;
	public float rotationPeriod = 0.0f;
	public float rotateXAdjustment = 0f;
	public float rotateYAdjustment = -1f;
	public float rotateZAdjustment = 0f;

	private float nextRotationAdjustment;

	void Start ()
	{
		if (player == null) {
			player = GameObject.FindGameObjectWithTag ("Player");
		}
		if (gravityCentre == null) {
			gravityCentre = GameObject.FindGameObjectWithTag ("Floor").transform;
		}

		if (debug) {
			Debug.Log ("Starting Gravity: " + Physics.gravity.ToString ());
			Debug.Log ("Starting Rotation: " + player.transform.rotation.ToString ());
			Debug.Log ("Starting Direction: " + Direction ().ToString ());
		}
	}

	void FixedUpdate ()
	{
		if (applyForceToPlayer) {
			ApplyForce ();
		}
		if (adjustGravity) {
			AdjustGravity ();
		}
		if (adjustRotation) {
			if (Time.time > nextRotationAdjustment) {
				nextRotationAdjustment = Time.time + rotationPeriod;
				AdjustRotation ();
			}
		}
	}

	private Vector3 RelativePos ()
	{
		return gravityCentre.position - player.transform.position;
	}

	private float Distance ()
	{
		return RelativePos ().magnitude;
	}

	private Vector3 Direction ()
	{
		return RelativePos () / Distance ();
	}

	private Vector3 GravityWell ()
	{
		var gravityWell = Direction () * gravityMultiplier;
		if (invertGravity) {
			gravityWell = gravityWell * -1f;
		}
		return gravityWell;
	}

	private void ApplyForce ()
	{
		Vector3 force = Direction () * -9.81f;
		Rigidbody rb = player.GetComponent<Rigidbody> ();
		rb.GetComponent<ConstantForce> ().relativeForce = force;
	}

	private void AdjustGravity ()
	{
		Vector3 newGravity = GravityWell ();
		if (newGravity != Physics.gravity) {
			Physics.gravity = newGravity;
			if (debug)
				Debug.Log ("Gravity now: " + Physics.gravity.ToString ());
		}
	}

	private  void AdjustRotation ()
	{
		if (debug) {
			Debug.Log ("Player Rotation: " + player.transform.rotation);
			Debug.Log ("Direction: " + Direction ().ToString () + ", transform up: " + player.transform.up.ToString () + ", transform forward: " + player.transform.forward.ToString ());
		}

		var oldPos = player.transform.position;
		var direction = Direction ();
		player.transform.up = direction.normalized;
//		player.transform.position = new Vector3 (oldPos.x, oldPos.y + player.transform.up.y, oldPos.z + player.transform.forward.z);

		var newForwards = new Vector3 (player.transform.rotation.x + rotateXAdjustment, Direction ().y + rotateYAdjustment, player.transform.rotation.z);

		if (player.transform.forward != newForwards) {
//			player.transform.rotation = Quaternion.Lerp (
//				player.transform.rotation,
//				Quaternion.LookRotation (newForwards),
//				Time.time * rotationSmoothing * Time.deltaTime);

//			player.transform.up = Direction ();
//			var newForwards = new Vector3 (Direction ().x + rotateXAdjustment, Direction ().y + rotateYAdjustment, Direction ().z + rotateZAdjustment);
//			if (myForwards.y > 360f) {
//				myForwards.y = myForwards.y - 360f;
//			}


//			player.transform.rotation = Quaternion.Lerp (
//				player.transform.rotation,
//				Quaternion.LookRotation (Direction ()),
//				Time.time * 1f);

//		player.transform.rotation = Quaternion.LookRotation (RelativePos ());
			if (debug)
				Debug.Log ("Rotation now: " + player.transform.rotation.ToString ());
		}

	}
}
