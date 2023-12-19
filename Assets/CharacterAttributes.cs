using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttributes : MonoBehaviour
{
    public float hunger;
    public float tiredness;
    public float boredom;

    public float hungerMultiplier;
    public float boredomMultiplier;
    public float tirednessMultiplier;

    public float hungerRecoveryRate;
    public float boredomRecoveryRate;
    public float tirednessRecoveryRate;

    public float sleepMultiplier;

    public bool sleeping;
    public bool eating;
    public bool entertaining;

    float baseHungerMul;
    float baseBoredemMul;

    float hungerSleepMul;
    float boredomSleepMul;


    // Start is called before the first frame update
    void Start()
    {
        baseHungerMul = hungerMultiplier;
        baseBoredemMul = boredomMultiplier;

        hungerSleepMul = hungerMultiplier * sleepMultiplier;
        boredomSleepMul = boredomMultiplier * sleepMultiplier;
    }

    // Update is called once per frame
    void Update()
    {
        Clamp();
        Sleeping();
        Eating();
        Entertaining();
        UI();


        if (!eating)
        {
            hunger += Time.deltaTime * (hungerMultiplier/1000) * TimeManager.Instance.timeMultiplier;
        }

        if (!sleeping) 
        {
            tiredness += Time.deltaTime * (tirednessMultiplier/1000) * TimeManager.Instance.timeMultiplier;
        }

        if (!entertaining)
        {
            boredom += Time.deltaTime * (boredomMultiplier/1000) * TimeManager.Instance.timeMultiplier;
        }
        

        if(boredom <= 0)
        {
            entertaining = false;
        }

        if(hunger <= 0)
        {
            eating = false;
        }

        if(tiredness <= 0)
        {
            sleeping = false;
        }
        
        
    }

    void UI()
    {
        UIManager.Instance.boredomSlider.value = boredom / 100;
        UIManager.Instance.hungerSlider.value = hunger / 100;
        UIManager.Instance.tirednessSlider.value = tiredness / 100;
    }

    void Clamp()
    {
        hunger = Mathf.Clamp(hunger, 0, 100);
        boredom = Mathf.Clamp(boredom, 0, 100);
        tiredness = Mathf.Clamp(tiredness, 0, 100);
    }


    void Sleeping()
    {
        if (sleeping)
        {
            hungerMultiplier = hungerSleepMul;
            boredomMultiplier = boredomSleepMul;
            tiredness -= Time.deltaTime * (tirednessRecoveryRate / 1000) * TimeManager.Instance.timeMultiplier;
        }
        else
        {
            hungerMultiplier = baseHungerMul;
            boredomMultiplier = baseBoredemMul;
        }
    }

    void Eating()
    {
        if (eating)
        {
            hunger -= Time.deltaTime * (hungerRecoveryRate / 1000) * TimeManager.Instance.timeMultiplier;
        }
    }

    void Entertaining()
    {
        if (entertaining)
        {
            boredom -= Time.deltaTime * (boredomRecoveryRate / 1000) * TimeManager.Instance.timeMultiplier;
        }
    }
}
