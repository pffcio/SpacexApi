using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class listOfShips : MonoBehaviour
{
    public List<string> shipList;
    public GameObject browserControler;

    private void Start()
    {
        browserControler = GameObject.Find("Capsule list");
    }
    public void updateListOfShips()
    {
        var browserC = browserControler.GetComponent<browesController>();
        if(shipList.Count>0)
             browserC.showPopup(shipList);
    }
}
