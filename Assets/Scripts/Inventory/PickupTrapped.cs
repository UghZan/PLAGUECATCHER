using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupTrapped : Pickup
{
    public GameObject what;
    public void OnInteract(Player player)
    {
        what.SetActive(true);
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
