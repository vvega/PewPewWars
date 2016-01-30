using UnityEngine;
using System.Collections.Generic;

public class Team : MonoBehaviour {
	public List<string> members = new List<string>();
    public int startingHealth;
	private int healthLeft;

	void Start()
    {
        healthLeft = startingHealth;
    }

	public void AddMember(string username) {
		members.Add(username);
	}

	public void PlaceMember(GameObject newMember) {
		float xPosRange = this.gameObject.name == "Red Castle" ? Random.Range(-.6F, .5F) : Random.Range(-.5F, .6F);
		Vector3 newPos = new Vector3 (this.gameObject.transform.position.x + xPosRange,
			this.gameObject.transform.position.y + Random.Range(-.5F, -.7F), this.gameObject.transform.position.z - 1);
		newMember.transform.position = newPos;
	}

	public void RemoveMember(string username) {
		//TODO: dead bod slingshot
		members.Remove(username);
	}
}