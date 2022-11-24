using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum SpeedLevel { Stop, Slow, Average, Full }

public sealed class PlayerControl : MonoBehaviour
{
    CharacterController controller;
    public GameObject body;
    public GameObject tracks;
    public GameObject reticle;
    public Weapon equipedWeapon;

    //Input
    bool up, right, down, left, isFiring, directionLocked;

    //Movement
    public float speedChangeRate = 5f;
    public float tracksAlignementRate = 5f;
    int currentSpeedLevel = 1;
    public float currentSpeed;
    // float lastSpeed;
    // float speedChangeTime;
    public Vector2[] speedLevels; // x - speed, y - body rotation speed
    [HideInInspector] public int maxLevel = 4;
    Vector3 movementDirection;
    Vector3 lastMovementDirection;
    int rotationDirection;


    //Shooting
    public LayerMask shootable;
    Weapon currentWeapon;
    float shotDelay;
    float nextShotTime;
    Collider[] hits = new Collider[10];

    //Interactions
    IPlayerInteractable currentInteractable;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void Start()
    {
        ChangeWeapon(equipedWeapon);
    }


    void Update()
    {
        ChangeSpeed();

        RotateAndMove();
        if (isFiring) Fire();
    }

    void RotateAndMove()
    {
        rotationDirection = 0;
        if (right) rotationDirection += 1;
        if (left) rotationDirection -= 1;

        body.transform.Rotate(transform.up, rotationDirection * speedLevels[currentSpeedLevel].y * Time.deltaTime);
        if (!directionLocked)
        {
            tracks.transform.rotation = Quaternion.Slerp(tracks.transform.rotation, body.transform.rotation, tracksAlignementRate * Time.deltaTime);
        }

        movementDirection = directionLocked ? lastMovementDirection : body.transform.forward;
        controller.SimpleMove(movementDirection * currentSpeed); // * Time.deltaTime
    }

    void ChangeSpeed()
    {
        if (currentSpeed == speedLevels[currentSpeedLevel].x) return;

        // currentSpeed = Mathf.Lerp(lastSpeed, speedLevels[currentSpeedLevel].x, speedChangeTime / speedChangeRate);
        currentSpeed = Mathf.MoveTowards(currentSpeed, speedLevels[currentSpeedLevel].x, speedChangeRate * Time.deltaTime);
        // speedChangeTime += Time.deltaTime;
    }

    public void Fire()
    {
        if (Time.time < nextShotTime) return;

        GameObject hitTmpact = Instantiate(currentWeapon.impactEffect, reticle.transform.position, Quaternion.identity);
        Destroy(hitTmpact, .5f);

        for (int i = 0; i < Physics.OverlapSphereNonAlloc(reticle.transform.position, currentWeapon.damageRadius, hits, shootable); i++)
        {
            print(hits[i].name);
        }

        nextShotTime = Time.time + shotDelay;
    }

    // void OnDrawGizmos() {
    //     Gizmos.DrawSphere(reticle.transform.position, currentWeapon.damageRadius );
    // }

    public void ChangeWeapon(Weapon newWeapon)
    {
        if (newWeapon == currentWeapon) return;

        currentWeapon = newWeapon;
        shotDelay = 1f / (currentWeapon.fireRate / 60f);

        reticle.GetComponent<Reticle>().SetRadius(currentWeapon.damageRadius);
    }

    #region InputHandling

    public void Interact()
    {
        currentInteractable?.OnPlayerInteraction();
    }

    public void LockDirection()
    {
        directionLocked = !directionLocked;
        lastMovementDirection = body.transform.forward;

        // tracks.transform.rotation = body.transform.rotation;
    }
    // public void UpPressed()
    // {
    //     up = true;
    // }
    // public void UpNotPressed()
    // {
    //     up = false;
    // }

    // public void DownPressed()
    // {
    //     down = true;
    // }
    // public void DownNotPressed()
    // {
    //     down = false;
    // }

    public void RightPressed()
    {
        right = true;
    }
    public void RightNotPressed()
    {
        right = false;
    }

    public void LeftPressed()
    {
        left = true;
    }
    public void LeftNotPressed()
    {
        left = false;
    }

    public void FirePressed()
    {
        isFiring = true;
    }
    public void FireNotPressed()
    {
        isFiring = false;
    }


    public void RaiseSpeed()
    {
        // if (currentSpeed != speedStep * 3) currentSpeed += speedStep;
        if (currentSpeedLevel < maxLevel)
        {
            currentSpeedLevel++;
            // lastSpeed = currentSpeed;
            // speedChangeTime = 0f;
        }
    }

    public void LowerSpeed()
    {
        // if (currentSpeed > -speedStep) currentSpeed -= speedStep;
        if (currentSpeedLevel > 0)
        {
            currentSpeedLevel--;
            // lastSpeed = currentSpeed;
            // speedChangeTime = 0f;
        }
    }

    public void FullStop()
    {
        currentSpeed = speedLevels[1].x;
        currentSpeedLevel = 1;
        // speedChangeTime = 0f;
    }



    #endregion

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out currentInteractable))
        {
            print(currentInteractable);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out currentInteractable))
        {
            currentInteractable = null;
            print(currentInteractable);
        }
    }
}