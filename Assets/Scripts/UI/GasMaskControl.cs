using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GasMaskControl : MonoBehaviour
{
    Player player;
    GasMask gm;
    public Sprite[] crosshairs;
    [SerializeField] Image crosshair;
    [SerializeField] TextMeshProUGUI cont_level;
    [SerializeField] TextMeshProUGUI cont_player;
    [SerializeField] TextMeshProUGUI filter_resource;
    [SerializeField] TextMeshProUGUI shells;
    [SerializeField] TextMeshProUGUI flamer;
    [SerializeField] TextMeshProUGUI filters;
    [SerializeField] TextMeshProUGUI current_ammo;
    bool[] updates;
    Ray ray;
    // Start is called before the first frame update
    void Start()
    {
        gm = GetComponent<GasMask>();
        player = GetComponentInParent<Player>();
        updates = new bool[6];
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = player.pc.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        if (Physics.Raycast(ray, out RaycastHit hit, player.pickupRange))
        {
            if (hit.transform.TryGetComponent(out IInteractable i))
                crosshair.sprite = crosshairs[1];
            else
                crosshair.sprite = crosshairs[0];
        }
        else
            crosshair.sprite = crosshairs[0];
    }

    public void UpdateValues(int which, string newValue, float overrider = -1)
    {
        switch(which)
        {
            case 0:
                StartCoroutine(UpdateText(Random.Range(0f, 0.5f), "CONT.LEVEL:", newValue, cont_level));
                break;
            case 1:
                StartCoroutine(UpdateText(Random.Range(0.1f, 0.5f), "USER.CONT:", newValue, cont_player));
                break;
            case 2:
                if (overrider >= 0)
                    StartCoroutine(UpdateText(overrider, "FILTR_RSRC:", newValue, filter_resource));
                else
                    StartCoroutine(UpdateText(Random.Range(0f, 0.5f), "FILTR_RSRC:", newValue, filter_resource));
                break;
            case 3:
                StartCoroutine(UpdateText(Random.Range(1f, 2f), "SHELLS:", newValue, shells));
                break;
            case 4:
                StartCoroutine(UpdateText(Random.Range(1f, 2f), "FILTR_RSRV:", newValue, filters));
                break;
            case 5:
                StartCoroutine(UpdateText(Random.Range(0f, 0.25f), "AMMO:", newValue, current_ammo));
                break;
            case 6:
                StartCoroutine(UpdateText(Random.Range(1f, 2f), "FLAMR.FUEL:", newValue, flamer));
                break;
        }
    }

    IEnumerator UpdateText(float duration, string template, string value, TextMeshProUGUI tmp)
    {
        float time = 0;
        while(time <= duration)
        {
            tmp.text = template + ".";
            yield return new WaitForSeconds(0.25f);
            tmp.text = template + "..";
            yield return new WaitForSeconds(0.25f);
            tmp.text = template + "...";
            yield return new WaitForSeconds(0.25f);
            time++;
            yield return null;
        }
        tmp.text = template + value;
    }
}
