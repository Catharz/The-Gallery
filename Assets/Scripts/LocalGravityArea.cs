using UnityEngine;
using System.Collections;

public class LocalGravityArea : MonoBehaviour
{

	public Vector3 gravity;
	public bool globalGravity;
	public bool debug = true;
	public GameObject player;

	private Rigidbody rb;
	private bool usingGlobalGravity;
	private Transform gravityCentre;

	GameObject Player ()
	{
		if (player == null)
			player = GameObject.FindGameObjectWithTag ("Player");
		return player;
	}

	Rigidbody  PlayerRb ()
	{
		if (rb == null)
			rb = player.GetComponent<Rigidbody> ();
		return rb;
	}

	bool UsingGlobalGravity ()
	{
		return PlayerRb ().useGravity;
	}

	Transform GravityCentre ()
	{
		if (gravityCentre == null)
			gravityCentre = GameObject.FindGameObjectWithTag ("Floor").transform;
		return gravityCentre;
	}

	void OnTriggerEnter ()
	{
		if (debug) {
			Debug.Log ("Enter LocalGravityArea");
		}
		if (globalGravity) {
			Physics.gravity = gravity;
			if (debug) {
				Debug.Log ("Global Gravity set to: " + gravity.ToString ());
			}
		} else {
			PlayerRb ().useGravity = false;
			Vector3 force = Direction () * -9.81f;
//			rb.AddRelativeForce (force);
			PlayerRb ().GetComponent<ConstantForce> ().relativeForce = force;
			if (debug) {
				Debug.Log ("Player relative force set to: " + PlayerRb ().GetComponent<ConstantForce> ().relativeForce.ToString ());
			}
		}
	}

	void OnTriggerExit ()
	{
		if (debug) {
			Debug.Log ("Exit LocalGravityArea");
		}
		PlayerRb ().useGravity = usingGlobalGravity;
//		rb.AddRelativeForce (-Direction ()); # Reverse the force direction?
	}

	// methods to help calculate position, etc.

	private Vector3 RelativePos ()
	{
		return GravityCentre ().position - Player ().transform.position;
	}

	private float Distance ()
	{
		return RelativePos ().magnitude;
	}

	private Vector3 Direction ()
	{
		return RelativePos () / Distance ();
	}

}
