using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionsManager : MonoBehaviour
{
    private int beerCount = 0;

    public void PressTabForBartender(string tabPressed) {
        if (tabPressed == "o")
            Debug.Log(tabPressed);
        else
            Debug.Log(tabPressed);

    }

    public void DrinkBeer() {
        beerCount++;
        if(beerCount > 3) {
            Debug.Log("YOU VOMMITED");
            beerCount = 0;
        }
    }
}
