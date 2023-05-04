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
        switch (statToBuy)
        {
            case 0:
            {
                if(!sellPoint)
                {
                    if(ScoreIncreaseCost(strength)) { strength++; }
                }else{
                    if(ScoreDecreaseCost(strength)) { strength--; }
                }
                break;
            }
            case 1:
            {
                if(!sellPoint)
                {
                    if(ScoreIncreaseCost(dexterity)) { dexterity++; }
                }else{
                    if(ScoreDecreaseCost(dexterity)) { dexterity--; }
                }
                break;
            }
            case 2:
            {
                if(!sellPoint)
                {
                    if(ScoreIncreaseCost(constitution)) { constitution++; }
                }else{
                    if(ScoreDecreaseCost(constitution)) { constitution--; }
                }
                break;
            }
            case 3:
            {
                if(!sellPoint)
                {
                    if(ScoreIncreaseCost(intelligence)) { intelligence++; }
                }else{
                    if(ScoreDecreaseCost(intelligence)) { intelligence--; }
                }
                break;
            }
            case 4:
            {
                if(!sellPoint)
                {
                    if(ScoreIncreaseCost(wisdom)) { wisdom++; }
                }else{
                    if(ScoreDecreaseCost(wisdom)) { wisdom--; }
                }

                break;
            }
            case 5:
            {
                if(!sellPoint)
                {
                    if(ScoreIncreaseCost(charisma)) { charisma++; }
                }else{
                    if(ScoreDecreaseCost(charisma)) { charisma--; }
                }
                break;
            }
        }
        UpdateStatDisplays();
        pointsText.text = new string(statPointsLeft.ToString() + "/27");
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
        if(currentValue == 8)
        {
            return HasPointsLeft(1);
        }
        else if(currentValue == 9)
        {
            return HasPointsLeft(1);
        }
        else if(currentValue == 10)
        {
            return HasPointsLeft(1);
        }
        else if(currentValue == 11)
        {
            return HasPointsLeft(1);
        }
        else if(currentValue == 12)
        {
            return HasPointsLeft(1);
        }
        else if(currentValue == 13)
        {
            return HasPointsLeft(2);
        }
        else if(currentValue == 14)
        {
            return HasPointsLeft(2);
        }
        else if(currentValue >=15)
        {
            return false;
        }

        return false;
    }

    private bool ScoreDecreaseCost(int currentValue)
    {
        if(currentValue == 8)
        {
            return false;
        }
        else if(currentValue == 9)
        {
            statPointsLeft += 1;
            return true;
        }
        else if(currentValue == 10)
        {
            statPointsLeft += 1;
            return true;
        }
        else if(currentValue == 11)
        {
            statPointsLeft += 1;
            return true;
        }
        else if(currentValue == 12)
        {
            statPointsLeft += 1;
            return true;
        }
        else if(currentValue == 13)
        {
            statPointsLeft += 2;
            return true;
        }
        else if(currentValue == 14)
        {
            statPointsLeft += 2;
            return true;
        }
        else if(currentValue ==15)
        {
            statPointsLeft += 2;
            return true;
        }
        return false;
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
        if(score == 1){return -5;}
        else if(score <= 3){return -4;}
        else if(score <= 5){return -3;}
        else if(score <= 7){return -2;}
        else if(score <= 9){return -1;}
        else if(score <= 11){return 0;}
        else if(score <= 13){return 1;}
        else if(score <= 15){return 2;}
        else if(score <= 17){return 3;}
        else if(score <= 19){return 4;}
        else if(score <= 21){return 5;}
        else if(score <= 23){return 6;}
        else if(score <= 25){return 7;}
        else if(score <= 27){return 8;}
        else if(score <= 29){return 9;}
        else if(score <= 30){return 10;}
        else if (score > 31){return 11;}
    return -5;
    }
}