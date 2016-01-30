using UnityEngine;
using System.Collections.Generic;

public class Team {

	private Dictionary<string, GameObject> members = new Dictionary<string, GameObject>();
	private GameObject castle;
	private int health;

	public Team(GameObject castle, int health) {
		castle = castle;
		health = health;
	}

	public void AddMember(string username, GameObject newMember) {
		members.Add(username, newMember);
		//newMember.SetTeam(this);
		AddToBoard(newMember);
	}

	public void RemoveMember(string username) {
		//TODO: dead bod slingshot
		members.Remove(username);
	}

	public int GetNumMembers() {
		return members.Count;
	}

	public Dictionary<string, GameObject> getMembers() {
		return members;
	}

	private void AddToBoard(GameObject newMember) {
		//TODO: position player on board
	}
}