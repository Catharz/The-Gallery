using UnityEngine;
using System.Collections;

public class LocalRotationArea : MonoBehaviour
{

	public float speed = 0.1f;
	public Transform targetAlignment;
	public Vector3 alignmentAdjustment = new Vector3 (0f, 0f, 0f);

	private GameObject player;

	// Use this for initialization
	void Start ()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void OnTriggerEnter ()
	{
		player.transform.rotation = Quaternion.Lerp (player.transform.rotation, targetAlignment.rotation, Time.time * speed);
		Debug.Log ("Player rotation now: " + player.transform.rotation.ToString ());
	}
}
