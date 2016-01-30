using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    public int startingHealth;
    private int remainingHealth;

	public string username;
	public Team team;

	// Use this for initialization
	void Start () {
        remainingHealth = startingHealth;
	}

    void DealDamage(int damage)
    {
        remainingHealth = Mathf.Max(remainingHealth - damage, 0);
        if (remainingHealth == 0)
        {
            // TODO: Temporarily kill character
        }
    }

	public void setTeam(Team team) {
		this.team = team;
		team.AddMember(username);
	}

	public Team getTeam() {
		return team;
	}
}
