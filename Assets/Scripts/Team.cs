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

	public void AddMember(string username, GameObject newMember) {
		members.Add(username);
		//newMember.SetTeam(this);
		AddToBoard(newMember);
	}

	public void RemoveMember(string username) {
		//TODO: dead bod slingshot
		members.Remove(username);
	}

	private void AddToBoard(GameObject newMember) {
		//TODO: position player on board
	}
}