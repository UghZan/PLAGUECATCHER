using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject playerBody;
    public Camera camera;

    public float normalFOV = 75;
    public float fovChangeSpeed = 2f;

    [Header("Tilting")]
    public float tiltCoefficient = 15f;
    public float tiltSpeed = 2f;

    [Header("Bobbing")]
    public float bobSpeed = 4f;
    public float bobAmount = 0.5f;
    public float bobMul = 2f;

    public Vector3 tiltDirection;

    [Header("Sprintng Multiplier")]
    public float sprintBobSpeedMultiplier = 1.5f;
    public float sprintBobAmountMultiplier = 1.5f;
    public float sprintFOV = 90;

    public bool isSprinting;

    PlayerController pc;

    Vector3 startPosition;
    Quaternion startRotation;
    float speed = 0;
    float fov;
    public bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.localPosition;
        startRotation = transform.localRotation;
        pc = playerBody.GetComponent<PlayerController>();
        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            Vector3 tiltVector = Quaternion.AngleAxis(90, Vector3.up) * (Vector3.up + tiltDirection.normalized * tiltCoefficient);
            Quaternion tiltRot = Quaternion.Euler(tiltVector);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, tiltRot, Time.deltaTime * tiltSpeed);

            speed = tiltDirection.normalized.magnitude;
            FOVControl();
            CameraBob();
        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, Time.deltaTime * 5f);
        }
    }

    void FOVControl()
    {
        if (isSprinting)
            fov = Mathf.Lerp(fov, sprintFOV, Time.deltaTime * fovChangeSpeed);
        else
            fov = Mathf.Lerp(fov, normalFOV, Time.deltaTime * fovChangeSpeed);
        camera.fieldOfView = fov;
    }

    void CameraBob()
    {
        float speedMul, strMul;
        if (isSprinting)
        {
            speedMul = sprintBobSpeedMultiplier;
            strMul = sprintBobAmountMultiplier;
        }
        else
        {
            strMul = 1;
            speedMul = 1;
        }
        float x = bobAmount * strMul * speed * Mathf.Sin(bobSpeed * speedMul * Time.time + Mathf.PI/4);
        float y = bobAmount * strMul * speed * Mathf.Sin(2 * bobSpeed * speedMul * Time.time);
        transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(startPosition.x + x, startPosition.y + y, startPosition.z), Time.deltaTime * bobMul);
    }


}
