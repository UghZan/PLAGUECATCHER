using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMovementController : MonoBehaviour
{
    public float maxSway;
    public float swayFactor;
    public float swayRotateFactor;
    public float swayDamp;
    public float swayRotateDamp;

    public float bobAmount;
    public float bobSpeed;

    public float breathAmount;
    public float breathSpeed;

    Vector3 position;
    private CameraMovement cm;
    private WeaponController wc;
    private RecoilController rc;

    // Use this for initialization
    void Start()
    {
        cm = GetComponentInParent<CameraMovement>();
        position = transform.localPosition;
        wc = GetComponentInChildren<WeaponController>();
        rc = GetComponentInParent<RecoilController>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (GlobalWeaponController.canUseWeapons)
        {
            Sway();
        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, position + Vector3.down * 0.33f, Time.fixedDeltaTime * 10);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(new Vector3(35, -35, 0)), Time.fixedDeltaTime * 10);
        }

        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            Walk();
        else
        {
            Breath();
        }
    }

    public void Sway()
    {
        float fX = Input.GetAxisRaw("Mouse X") * swayFactor;
        float fY = Input.GetAxisRaw("Mouse Y") * swayFactor;

        fX = Mathf.Clamp(fX, -maxSway, maxSway);
        fY = Mathf.Clamp(fY, -maxSway, maxSway);

        Vector3 final = new Vector3(position.x - fX, position.y - fY, position.z);
        Vector3 rotate = Vector3.up * fX * swayRotateFactor;
        transform.localPosition = Vector3.Lerp(transform.localPosition, final, swayDamp);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(rotate), swayRotateDamp);
    }

    public void Walk()
    {
        float xBob = 2 * Mathf.Sin(Time.time * bobSpeed + Mathf.PI / 2) * bobAmount;
        float yBob = Mathf.Sin(Time.time * 2 * bobSpeed) * bobAmount;

        Vector3 final = new Vector3(position.x + xBob, position.y + yBob, position.z);
        transform.localPosition = Vector3.Lerp(transform.localPosition, final, 0.1f*Time.deltaTime);
    }

    public void Breath()
    {
        float yBob = Mathf.Sin(Time.time * 2 * breathSpeed) * breathAmount;

        Vector3 final = new Vector3(position.x, position.y + yBob, position.z);
        transform.localPosition = Vector3.Lerp(transform.localPosition, final, 0.1f*Time.deltaTime);
    }

    public void Punch()
    {
        rc.RecoilPunch(wc.cameraRecoil_x, wc.cameraRecoil_y);
    }
}
