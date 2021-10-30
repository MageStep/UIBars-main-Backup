// this should be on the canvas
// this controls all of the Screenspace UI in the level

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIController : MonoBehaviour
{

    [SerializeField]
    //instead of making this public, we can add it to the inspector with [SerializeField]
    private Slider healthSlider, manaSlider, xpSlider;            // we can make multiple variables on the same line!

    // getters and setters

    void Start()
    {

    }

    //setter function
    public void SetHealthSlider(int amount)
    {
        //make sure no one is sending bad data...
        if(amount < 0)
        {       
            healthSlider.value = 0;
        }
        if(amount > 100)
        {
            healthSlider.value = 100;
        }

        healthSlider.value = amount;
    }

    public void SetManaSlider(int amount)
    {
        //make sure no one is sending bad data...
        if(amount < 0)
        {       
            manaSlider.value = 0;
        }
        if(amount > 100)
        {
            manaSlider.value = 100;
        }

        manaSlider.value = amount;
    }

        public void SetXPSlider(int amount)
    {
        //make sure no one is sending bad data...
        if(amount < 0)
        {       
            xpSlider.value = 0;
        }
        if(amount > 100)
        {
            xpSlider.value = 100;
        }

        xpSlider.value = amount;
    }
}