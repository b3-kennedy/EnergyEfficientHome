using UnityEngine;
using UnityEngine.UI;

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

    CharacterTemperature temp;

    public bool isExercising = false;

    public float happinessChange;

    [Header("Comfortable Bools")]
    public bool isCold = false;
    public bool isTired = false;
    public bool isBored = false;
    public bool isHungry = false;
    public bool flies = false;

    Image hungerFill;
    Image tirednessFill;
    Image boredomFill;
    Image happinessFill;

    public float timeWhenUnhappy;
    int dayWhenUnhappy;
    bool savedUnhappyTime = false;
    int unhappyHoursPassed;
    bool unhappy;


    // Start is called before the first frame update

    private void Awake()
    {
        TimeManager.Instance.hourPassed.AddListener(UnhappyHoursPassed);
    }

    void Start()
    {
        baseHungerMul = hungerMultiplier;
        baseBoredemMul = boredomMultiplier;

        hungerFill = UIManager.Instance.hungerSlider.transform.GetChild(1).GetChild(0).GetComponent<Image>();
        tirednessFill = UIManager.Instance.tirednessSlider.transform.GetChild(1).GetChild(0).GetComponent<Image>();
        boredomFill = UIManager.Instance.boredomSlider.transform.GetChild(1).GetChild(0).GetComponent<Image>();
        happinessFill = UIManager.Instance.happinessSlider.transform.GetChild(1).GetChild(0).GetComponent<Image>();

        hungerSleepMul = hungerMultiplier * sleepMultiplier;
        boredomSleepMul = boredomMultiplier * sleepMultiplier;
        temp = GetComponent<CharacterTemperature>();
        happiness = 50;
        boredom = 100;
    }

    // Update is called once per frame
    void Update()
    {
        Clamp();
        Sleeping();
        Eating();
        Entertaining();
        UI();

        if(!isCold && !isBored && !isHungry && !isTired && !flies)
        {
            temp.isComfortable = true;
        }
        else
        {
            temp.isComfortable= false;
        }

        happinessMultiplier = Mathf.Clamp(happinessMultiplier, 1, 5);

        if(LevelManager.Instance.spawnedFlies.Count > 0)
        {
            flies = true;
            happinessMultiplier += LevelManager.Instance.spawnedFlies.Count / 5;
        }
        else
        {
            flies = false;
        }

        if (!temp.isComfortable)
        {
            happiness -= Time.deltaTime * happinessMultiplier/2 * TimeManager.Instance.timeControlMultiplier;
        }
        else
        {
            happiness += Time.deltaTime * happinessMultiplier/2 * TimeManager.Instance.timeControlMultiplier;
        }


        if (!eating)
        {
            hunger -= Time.deltaTime * (hungerMultiplier / 2) * TimeManager.Instance.timeControlMultiplier;
        }

        if (!sleeping)
        {
            tiredness -= Time.deltaTime * (tirednessMultiplier / 2) * TimeManager.Instance.timeControlMultiplier;
        }

        if (!entertaining)
        {
            boredom -= Time.deltaTime * (boredomMultiplier / 2) * TimeManager.Instance.timeControlMultiplier;
        }

        if (hunger <= 25 && !displayHunger)
        {
            
            UIManager.Instance.DisplayNotification("You are hungry you should eat!");
            displayHunger = true;
        }

        if(happiness <= 25 && !displayHappiness)
        {
            
            UIManager.Instance.DisplayNotification("You are unhappy");
            displayHappiness = true;
        }

        if (tiredness <= 25 && !displayTiredness)
        {
            
            UIManager.Instance.DisplayNotification("You are tired you should sleep!");
            displayTiredness = true;
        }

        if (boredom <= 25 && !displayBoredom)
        {
            
            UIManager.Instance.DisplayNotification("You are bored!");
            displayBoredom = true;
        }

        if(boredom <= 0)
        {
            isBored = true;
            happinessMultiplier += happinessChange;
        }

        if(tiredness <= 0)
        {
            isTired = true;
            happinessMultiplier += happinessChange;
        }

        if(hunger <= 0)
        {
            isHungry = true;
            happinessMultiplier += happinessChange;
        }


        if (boredom >= 100)
        {
            isBored = false;
            happinessMultiplier -= happinessChange;
            entertaining = false;
            displayBoredom = false;
        }

        if (hunger >= 100)
        {
            isHungry = false;
            happinessMultiplier -= happinessChange;
            eating = false;
            displayHunger = false;
        }

        if(happiness > 25)
        {
            displayHappiness = false;
        }

        if (tiredness >= 100)
        {
            isTired = false;
            happinessMultiplier -= happinessChange;
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

        if (isExercising)
        {
            happiness += Time.deltaTime * (happinessMultiplier * 5 * TimeManager.Instance.timeControlMultiplier);
        }


        if(happiness <= 0)
        {
            if (!savedUnhappyTime)
            {
                timeWhenUnhappy = TimeManager.Instance.GetFloatTime(TimeManager.Instance.currentTime);
                dayWhenUnhappy = LevelManager.Instance.daysInLevel;
                savedUnhappyTime = true;
                unhappy = true;
            }

            if(unhappyHoursPassed >= 24)
            {
                ManageEndStates.Instance.UnhappyEnd();
                unhappyHoursPassed = 0;
            }

        }
        else if (happiness > 0)
        {
            unhappy = false;
            savedUnhappyTime = false;
        }
        
    }

    public void UnhappyHoursPassed()
    {
        if (unhappy)
        {
            unhappyHoursPassed++;
        }

    }

    void UI()
    {
        UIManager.Instance.boredomSlider.value = boredom / 100;
        boredomFill.color = Color.Lerp(Color.red, Color.green, boredom / 100);

        UIManager.Instance.hungerSlider.value = hunger / 100;
        hungerFill.color = Color.Lerp(Color.red, Color.green, hunger / 100);

        UIManager.Instance.tirednessSlider.value = tiredness / 100;
        tirednessFill.color = Color.Lerp(Color.red, Color.green, tiredness / 100);

        UIManager.Instance.happinessSlider.value = happiness / 100;
        happinessFill.color = Color.Lerp(Color.red, Color.green, happiness / 100);
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
            tiredness += 1f;
            if (tiredness >= 95)
            {
                isTired = false;
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
            hunger += Time.deltaTime * (hungerRecoveryRate / 2) * TimeManager.Instance.timeControlMultiplier;
            
        }
    }

    void Entertaining()
    {
        if (entertaining)
        {
            boredom += Time.deltaTime * (boredomRecoveryRate / 2) * TimeManager.Instance.timeControlMultiplier;
            happiness += Time.deltaTime * (happinessRecoveryRate / 2) * TimeManager.Instance.timeControlMultiplier;
        }
    }
    public void FadeIn()
    {
        fadeOut = false;
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
        fadeIn = false;
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