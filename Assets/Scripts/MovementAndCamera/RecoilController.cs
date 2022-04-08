using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoilController : MonoBehaviour
{
    public Quaternion startRotation;

    private Quaternion target;

    public float recoverySpeed;
    public float recoilMultiplier = 1f;

    // Start is called before the first frame update
    void Start()
    {
        startRotation = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localRotation = target;
        target = Quaternion.Lerp(target, startRotation, Time.deltaTime * recoverySpeed);
    }

    public void RecoilPunch(float r_X, float r_Y)
    {
        Vector3 recoil = new Vector3(-r_Y * recoilMultiplier, r_X * recoilMultiplier * getRandomSign());
        target *= Quaternion.Euler(recoil);
    }

    public float getRandomSign()
    {
        return Mathf.Sign(Random.value - 0.5f);
    }
}
