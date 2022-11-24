using UnityEngine;

public class Reticle : MonoBehaviour
{
    public Transform reticleTarget;
    public Transform player;

    public Transform reticleTopRight;
    public Transform reticleBottomRight;
    public Transform reticleBottomLeft;
    public Transform reticleTopLeft;


    public bool isRotating;
    public float rotationRate;
    Vector3 defaultOffset;
    Vector3 lastPlayerPosition;
    Vector3 newPosition;
    // float differenceAngle;
    // float currentAngle;

    void Awake()
    {
        transform.position = reticleTarget.position;
        lastPlayerPosition = player.position;

    }

    void LateUpdate()
    {
        // transform.position = player.position + defaultOffset; //WRONG!!!
        transform.position += player.position - lastPlayerPosition;
        lastPlayerPosition = player.position;

        if (transform.position != reticleTarget.position)
        {
            transform.position = Vector3.Slerp(transform.position, reticleTarget.position, rotationRate * Time.deltaTime);
            isRotating = true;
        }
        else
        {
            isRotating = false;
        }

        // differenceAngle = Vector3.Angle(transform.position - player.position, reticleTarget.position - player.position);
        // currentAngle = Mathf.MoveTowardsAngle(currentAngle, differenceAngle, rotationRate * Time.deltaTime);
        // transform.RotateAround(player.position, Vector3.up, currentAngle);
    }

    public void SetRadius(float newRadius)
    {
        newPosition = reticleTopRight.localPosition;

        newPosition.x = newPosition.z = Mathf.Sqrt(newRadius * newRadius / 2);
        reticleTopRight.localPosition = newPosition;

        newPosition.z *= -1;
        reticleBottomRight.localPosition = newPosition;

        newPosition.x *= -1;
        reticleBottomLeft.localPosition = newPosition;

        newPosition.z *= -1;
        reticleTopLeft.localPosition = newPosition;


        // foreach (Transform segment in this.GetComponentsInChildren<Transform>())
        // {
        //     Vector3 newPosition = segment.localPosition;
        //     newPosition.x *= radiusMultiplier;
        //     newPosition.z *= radiusMultiplier;
        //     segment.localPosition = newPosition;
        // }
    }
}