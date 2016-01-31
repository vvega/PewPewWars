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

    [Header("Sounds")]
    public AudioClip[] death;

    [Header("Templates")]
    public GameObject deadTemplate;

    // Use this for initialization
    void Start () {
        remainingHealth = startingHealth;
	}

    public void DealDamage(int damage)
    {
        remainingHealth = Mathf.Max(remainingHealth - damage, 0);
        if (remainingHealth == 0)
        {
            Die();
        }
    }

    [ContextMenu("Die")]
    void Die()
    {
        gameObject.SetActive(false);
        AudioSource.PlayClipAtPoint(death[Random.Range(0, death.Length)], Vector3.zero);

        Instantiate(deadTemplate).transform.position = transform.position;
        Invoke("Respawn", 10.0f);
    }

    void Respawn()
    {
        gameObject.SetActive(true);
        transform.position = team.spawnPoint.position;
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
        if (Vector3.Distance(transform.position, targetLocation) > 0.001f)
        {
            Vector3 direction = (targetLocation - transform.position).normalized;
            transform.position += direction * Time.deltaTime * walkSpeed;

            if (direction.y < 0)
                SetWalkDirection(1);
            else if (direction.x > 0)
                SetWalkDirection(2);
            else if (direction.y > 0)
                SetWalkDirection(3);
            else
                SetWalkDirection(4);
        }
        else
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
