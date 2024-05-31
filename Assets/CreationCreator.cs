using TMPro;
using UnityEngine;

public class CreationCreator : MonoBehaviour
{
    #region Singleton

    private static CreationCreator _instance;
    public static CreationCreator Instance { get { return _instance; } }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);

        }
        else
        {
            _instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    void Start(){UpdateStatDisplays();}

    #endregion

    private int statPointsLeft = 27;
    private int strength = 8,dexterity = 8,constitution = 8, intelligence = 8, wisdom = 8, charisma = 8;    
    [SerializeField]  TextMeshProUGUI pointsText;  
    
    public void BuyStatPoint(int statToBuy, bool sellPoint)
    {
        int[] stats = new int[] { strength, dexterity, constitution, intelligence, wisdom, charisma };
    
        if (statToBuy < 0 || statToBuy >= stats.Length)
        {
            return; // or throw an exception, depending on requirements
        }
    
        if (!sellPoint && ScoreIncreaseCost(stats[statToBuy]))
        {
            stats[statToBuy]++;
        }
        else if (sellPoint && ScoreDecreaseCost(stats[statToBuy]))
        {
            stats[statToBuy]--;
        }
    
        strength = stats[0];
        dexterity = stats[1];
        constitution = stats[2];
        intelligence = stats[3];
        wisdom = stats[4];
        charisma = stats[5];
    
        UpdateStatDisplays();
        pointsText.text = $"{statPointsLeft}/27";
    }

    [SerializeField] StatDisplay[] statDisplays;
    public CharacterRace race;
    private void UpdateStatDisplays()
    {
        int prcStrength = strength + race.strBonus,
        prcDexterity = dexterity + race.dexBonus,
        prcConstitution = constitution + race.conBonus,
        prcIntelligence = intelligence + race.intBonus,
        prcWisdom = wisdom + race.wisBonus,
        prcCharisma = charisma + race.chrBonus;



        statDisplays[0].UpdateStatDisplay(prcStrength,ModFromScore(prcStrength));
        statDisplays[1].UpdateStatDisplay(prcDexterity,ModFromScore(prcDexterity));
        statDisplays[2].UpdateStatDisplay(prcConstitution,ModFromScore(prcConstitution));
        statDisplays[3].UpdateStatDisplay(prcIntelligence,ModFromScore(prcIntelligence));
        statDisplays[4].UpdateStatDisplay(prcWisdom,ModFromScore(prcWisdom));
        statDisplays[5].UpdateStatDisplay(prcCharisma,ModFromScore(prcCharisma));
    }

    private bool ScoreIncreaseCost(int currentValue)
    {
        if (currentValue < 8 || currentValue >= 15)
        {
            return false;
        }
        else if (currentValue >= 8 && currentValue < 13)
        {
            statPointsLeft--;
            return true;
        }
        else
        {
            statPointsLeft -= 2;
            return true;
        }
    }

    private bool ScoreDecreaseCost(int currentValue)
    {
        if (currentValue <= 8 || currentValue > 15)
        {
        return false;
        }
        else if (currentValue > 8 && currentValue <= 13)
        {
        statPointsLeft++;
        return true;
        }
        else
        {
        statPointsLeft += 2;
        return true;
        }
    }

    private bool HasPointsLeft(int points)
    {
        if(statPointsLeft >= points)
        {
            statPointsLeft -= points;
            return true;
        }
        return false;
    }

    private int ModFromScore(int score)
    {
        int scoreMod = Mathf.FloorToInt((score-10)/2);
        return scoreMod;
    }

    public void WriteCharacterToFile()
    {
        
    }
}