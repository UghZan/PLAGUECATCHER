using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour, IInteractable
{
    public AudioClip pickupSound;
    public bool important;
    public string pickupName;
    public int itemID;
    protected int amount;
    public bool randomAmount = false;
    public int minAmount = 1;
    public int maxAmount = 2;

    protected bool pickedUp;
    protected Vector3 target;
    protected InfoControl ic;

    // Start is called before the first frame update
    void Start()
    {
        ic = FindObjectOfType<InfoControl>();
        target = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (pickedUp)
            transform.Translate((target - transform.position).normalized * Time.deltaTime * 20);
    }

    public void OnInteract(Player player)
    {
        ic.NotifyText(0, pickupName);
        if (important)
        {
            player.collects[itemID] = true;
        }
        else
        {
            if (randomAmount)
                amount = Random.Range(minAmount, maxAmount + 1);
            else
                amount = minAmount;
            player.AddItem(itemID, amount);
        }
        pickedUp = true;
        target = player.transform.position;
        player.PlayPickupSound(pickupSound);
        Destroy(gameObject, 0.25f);
    }
}
