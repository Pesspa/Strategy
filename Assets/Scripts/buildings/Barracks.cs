using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Barracks : Building
{
    public GameObject menuBarrack;
    public override void Select()
    {
        base.Select();
        menuBarrack.SetActive(true);
    }
    public override void Unselect()
    {
        base.Unselect();
        menuBarrack.SetActive(false);
    }
}
