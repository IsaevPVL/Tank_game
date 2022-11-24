using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapon")]
public class Weapon : ScriptableObject
{
    public string weaponName;

    [Space, Header("Shooting")]
    public float damageRadius = 0.43f;
    public float shotDamage;
    public float fireRate = 600;
    public bool canHoldTrigger = true;
    public GameObject impactEffect;

    // [Space, Header("Spread")]
    // public Texture outerReticle;
    // public float outerReticleDefaultScale = 1;
    // public float spread = 0.017f;
    // public float crouchSpreadModifier = -0.01f;
    // public float walkSpreadModifier = 0.02f;
    // public float recoilSpreadModifier = 0.005f;
    // public float recoilSpreadReductionBase = 0.02f;
    // public float recoilSpreadReductionTimeMultiplier = 20f;
    // public float maxRecoilSpread = 0.05f;

    // [Space, Header("Ammo")]
    // public int currentAmmo;
    // public int maxAmmo;
    // public float reloadTime = 1f;
    // [HideInInspector] public bool isReloading;
}