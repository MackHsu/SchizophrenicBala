using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    public int itemId;
    public int number;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.E) && collision.tag == "Player")
        {
            bool found = false;
            foreach (Item item in GameManager.save.inventory)
            {
                if (item.id == itemId)
                {
                    item.count += number;
                    found = true;
                    break;
                }
            }
            if (!found) GameManager.save.inventory.Add(new Item(itemId, number));
            Destroy(gameObject);
        }
    }
}
