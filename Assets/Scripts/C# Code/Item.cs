using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public int id;
    public int count;
    public Item(int id,int count)
    {
        this.id = id;
        this.count = count;
    }
    public Item() { }
}
