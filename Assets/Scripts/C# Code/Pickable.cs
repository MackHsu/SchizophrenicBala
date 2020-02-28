using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    public int itemId;
    public int number;
    bool playerEnter = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") playerEnter = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "player") playerEnter = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerEnter)
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
