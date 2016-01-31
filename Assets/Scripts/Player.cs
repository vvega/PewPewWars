using UnityEngine;

public class Player : MonoBehaviour {
    [Header("Wizardly Params")]
    public int startingHealth;
    public float walkSpeed;

    [Header("In-Game Vars")]
    public string username;
	public Team team;

    public Vector3 targetLocation;
    private int remainingHealth;

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

    void Update()
    {
        int currentDirection = GetWalkDirection();
        if (Vector3.Distance(transform.position, targetLocation) > 0.001f)
        {
            Vector3 direction = (targetLocation - transform.position).normalized;
            transform.position += direction * Time.deltaTime * walkSpeed;

            if (currentDirection != 1 && direction.y < 0)
                SetWalkDirection(1);
            else if (currentDirection != 2 && direction.x > 0)
                SetWalkDirection(2);
            else if (currentDirection != 3 && direction.y > 0)
                SetWalkDirection(3);
            else
                SetWalkDirection(4);
        }
        else if (currentDirection != 0)
        {
            SetWalkDirection(0);
        }
    }

    int GetWalkDirection()
    {
        return GetComponent<Animator>().GetInteger("walkDirection");
    }

    void SetWalkDirection(int dir)
    {
        GetComponent<Animator>().SetInteger("walkDirection", dir);
    }
}
