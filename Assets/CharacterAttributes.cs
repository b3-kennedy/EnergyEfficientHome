using UnityEngine;

public class CharacterAttributes : MonoBehaviour
{
    public float hunger;
    public float tiredness;
    public float boredom;
    public float happiness;

    public float hungerMultiplier;
    public float boredomMultiplier;
    public float tirednessMultiplier;
    public float happinessMultiplier;

    public float hungerRecoveryRate;
    public float boredomRecoveryRate;
    public float tirednessRecoveryRate;
    public float happinessRecoveryRate;

    public float sleepMultiplier;

    public bool sleeping;
    public bool eating;
    public bool entertaining;

    float baseHungerMul;
    float baseBoredemMul;

    float hungerSleepMul;
    float boredomSleepMul;

    bool displayTiredness;
    bool displayBoredom;
    bool displayHunger;
    bool displayHappiness;

    public CanvasGroup canvasGroup;
    public bool fadeOut = false;
    public bool fadeIn = false;
    public float timeToFade;



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
            hunger += Time.deltaTime * (hungerMultiplier / 1000) * TimeManager.Instance.timeMultiplier;
        }

        if (!sleeping)
        {
            tiredness += Time.deltaTime * (tirednessMultiplier / 1000) * TimeManager.Instance.timeMultiplier;
        }

        if (!entertaining)
        {
            boredom += Time.deltaTime * (boredomMultiplier / 1000) * TimeManager.Instance.timeMultiplier;
        }

        if (hunger >= 95 && !displayHunger)
        {
            GetComponent<CharacterTemperature>().isComfortable = false;
            UIManager.Instance.DisplayNotification("You are hungry you should eat!");
            displayHunger = true;
        }

        if (tiredness >= 95 && !displayTiredness)
        {
            GetComponent<CharacterTemperature>().isComfortable = false;
            UIManager.Instance.DisplayNotification("You are tired you should sleep!");
            displayTiredness = true;
        }

        if (boredom >= 95 && !displayBoredom)
        {
            GetComponent<CharacterTemperature>().isComfortable = false;
            UIManager.Instance.DisplayNotification("You are bored!");
            displayBoredom = true;
        }


        if (boredom <= 0)
        {
            entertaining = false;
            displayBoredom = false;
        }

        if (hunger <= 0)
        {
            eating = false;
            displayHunger = false;
        }

        if (tiredness <= 0)
        {
            sleeping = false;
            displayTiredness = false;
        }
        if (fadeIn)
        {
            if (canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= timeToFade * Time.deltaTime;
            }
            if (canvasGroup.alpha == 0)
            {
                fadeIn = false;
            }
        }
        if (fadeOut)
        {

            if (canvasGroup.alpha < 1)
            {
                canvasGroup.alpha += timeToFade * Time.deltaTime;
            }
            if (canvasGroup.alpha >= 1)
            {
                fadeOut = false;
            }
        }


    }

    void UI()
    {
        UIManager.Instance.boredomSlider.value = boredom / 100;
        UIManager.Instance.hungerSlider.value = hunger / 100;
        UIManager.Instance.tirednessSlider.value = tiredness / 100;
        UIManager.Instance.happinessSlider.value = happiness / 100;
    }

    void Clamp()
    {
        hunger = Mathf.Clamp(hunger, 0, 100);
        boredom = Mathf.Clamp(boredom, 0, 100);
        tiredness = Mathf.Clamp(tiredness, 0, 100);
        happiness = Mathf.Clamp(happiness, 0, 100);
    }

    public GameObject[] uiElementsToHideOnSleep;
    void Sleeping()
    {
        if (sleeping)
        {
            tiredness -= 0.5f;
            if (tiredness <= 0)
            {
                sleeping = false;
                StartCoroutine(TimeManager.Instance.SkipToNextDay());

                FadeIn();
            }


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
            happiness += Time.deltaTime * (happinessRecoveryRate / 1000) * TimeManager.Instance.timeMultiplier;
        }
    }
    public void FadeIn()
    {
        fadeIn = true;
        if (uiElementsToHideOnSleep != null)
        {
            foreach (var element in uiElementsToHideOnSleep)
            {
                element.SetActive(true);
            }
        }

    }
    public void FadeOut()
    {
        fadeOut = true;
        if (uiElementsToHideOnSleep != null)
        {
            foreach (var element in uiElementsToHideOnSleep)
            {
                element.SetActive(false);
            }
        }


    }
}
