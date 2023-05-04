using UnityEngine;
using TMPro;

public class StatDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreNum, modNum;
    [SerializeField] private int statRefNum;
    public void UpdateStatDisplay(int score, int mod)
    {
        scoreNum.text = score.ToString();
        modNum.text = mod.ToString();
    }

    public void AttemptPointBuy()
    {
        CreationCreator.Instance.BuyStatPoint(statRefNum,false);
    }

    public void AttemptPointSell()
    {
        CreationCreator.Instance.BuyStatPoint(statRefNum,true);
    }
}