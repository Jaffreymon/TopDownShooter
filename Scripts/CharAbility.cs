using UnityEngine;

public abstract class CharAbility : MonoBehaviour {
    [SerializeField]
    private float cooldown;
    private float cooldownTimer;
    private float skillUpTime;

    [SerializeField]
    private SkillUI skillUI;

    protected virtual void Start()
    {
        skillUI = GameObject.FindGameObjectWithTag("skillUI").GetComponent<SkillUI>();
    }

    private void Update()
    {
        if(!isSkillActive())
        {
            //Debug.Log("Curr Time: " + Time.time + "\t Available: " + cooldownTimer);
            skillUI.updateTimer(cooldownTimer - Time.time);
            skillUI.toggleUI(true);
        }
    }

    public abstract void activate();

    public float getCooldown() { return cooldown; }

    public void setCooldown(float _coolDown) { cooldown = _coolDown; }

    // Return bool of ability cooldown in effect
    public bool isSkillActive()
    {
        return (Time.time >= cooldownTimer);
    }

    public void assignCoolDown()
    {
        cooldownTimer = Time.time + cooldown;
    }

    public SkillUI getSkillUI() { return skillUI; }
}
