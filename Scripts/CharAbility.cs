using UnityEngine;

public abstract class CharAbility : MonoBehaviour {
    [SerializeField]
    private float cooldown;
    private float cooldownTimer;
    private float skillUpTime;

    protected float skillDmgMultiplier = 1.5f;
    protected const int maxLvl = 3;

    [SerializeField]
    private SkillUI skillUI;

    protected virtual void Start()
    {
        skillUI = GameObject.FindGameObjectWithTag("skillUI").GetComponent<SkillUI>();
    }

    private void FixedUpdate()
    {
        if(!isSkillActive())
        {
            skillUI.updateTimer(cooldownTimer - Time.time);
            skillUI.toggleUI(true);
        }
        else
        {
            skillUI.toggleUI(false);
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

    public abstract void modifyDmgMultiplier(int _playerLvl);
    
    public void modifySkillDmgMul(int _playerLvl)
    {
        if(_playerLvl < maxLvl)
        {
            skillDmgMultiplier *= Mathf.Exp( (_playerLvl - 1) / 2 );
        }
    }

    public SkillUI getSkillUI() { return skillUI; }
}
