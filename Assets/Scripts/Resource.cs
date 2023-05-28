using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resource : MonoBehaviour
{
    public Text goldScore;
    public int gold = 100;
    private void Update()
    {
        goldScore.text = gold.ToString();
    }
}
