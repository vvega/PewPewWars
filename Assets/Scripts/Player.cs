using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public string username;
	public Team team;

	// Use this for initialization
	void Start () {
	}

	public void setTeam(Team team) {
		this.team = team;
		team.AddMember(username);
	}

	public Team getTeam() {
		return team;
	}
	
	// Update is called once per frame
	void Update () {
	}
}
