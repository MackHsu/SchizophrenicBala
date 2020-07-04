using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save
{
    //存档类
    public int personalityId;  //当前角色人格
    public int scene;   //当前角色场景
    public Dictionary<string, bool> flags;     //剧情进度标志
    public List<Item> inventory;    //物品

    //public Save()
    //{
    //    personalityId = 1;
    //    scene = 1;
    //    flags = new Dictionary<string, bool>();
    //    inventory = new List<Item>();
    //}
}
