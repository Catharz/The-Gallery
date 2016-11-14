using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class CollectionsAPI : MonoBehaviour
{
	[System.Serializable]
	private struct CollectionList
	{
		public Collection[] data;
	}

	[System.Serializable]
	private struct Collection
	{
		public string name;
		public string display_name;
		public string image_url;
		public string state;
	}

	private CollectionList collections;
	private GameObject[] surfaces;
	private bool collectionsLoaded = false;
	private int currentTexture = -1;

	// Use this for initialization
	void Start ()
	{
		surfaces = GameObject.FindGameObjectsWithTag ("Canvas");
		StartCoroutine (GetCollections ());
	}

	void OnMouseDown ()
	{
		if (collectionsLoaded) {
			CycleTextures ();
		}
	}

	void LoadCollections (string text)
	{
		collections = JsonUtility.FromJson<CollectionList> (text);
		CycleTextures ();
		collectionsLoaded = true;
	}

	IEnumerator GetCollections ()
	{
		UnityWebRequest www = UnityWebRequest.Get ("http://icarus.api.redbubble.com/v2/collections");
		yield return www.Send ();

		if (www.isError) {
			Debug.Log (www.error);
		} else {
			LoadCollections (www.downloadHandler.text);
		}
	}

	void CycleTextures ()
	{
		for (int i = 0; i < surfaces.Length; i++) {
			GameObject surface = surfaces [i];
			currentTexture++;
			if (currentTexture >= collections.data.Length) {
				currentTexture = 0;
			}
			string textureUrl = collections.data [currentTexture].image_url;
			textureUrl = textureUrl.Replace ("240x240", "512x512");
			StartCoroutine (ShowTexture (surface, textureUrl));
		}
	}

	IEnumerator ShowTexture (GameObject surface, string textureUrl)
	{
		UnityWebRequest www = UnityWebRequest.GetTexture (textureUrl);
		yield return www.Send ();

		if (www.isError) {
			Debug.Log (www.error);
		} else {
			Texture tex = ((DownloadHandlerTexture)www.downloadHandler).texture;
			surface.gameObject.GetComponent<Renderer> ().materials [0].mainTexture = tex;
			Debug.Log ("Loaded texture: " + textureUrl);
		}
	}
}
