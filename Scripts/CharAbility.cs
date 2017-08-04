using UnityEngine;

public abstract class CharAbility : MonoBehaviour {
    [SerializeField]
    private float cooldown;
    private float cooldownTimer;
    private float skillUpTime;

    public abstract void activate();

    public float getCooldown() { return cooldown; }

    public void setCooldown(float _coolDown) { cooldown = _coolDown; }

    // Return bool of ability cooldown in effect
    public bool isCooldownOver()
    {
        //Debug.Log("Curr Time: " + Time.time + "\t Available: " + cooldownTimer);
        return (Time.time > cooldownTimer);
    }

    public void assignCoolDown()
    {
        cooldownTimer = Time.time + cooldown;
    }
}
