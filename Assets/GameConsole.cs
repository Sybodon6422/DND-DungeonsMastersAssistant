using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameConsole : MonoBehaviour
{
    #region Singleton

    private static GameConsole _instance;
    public static GameConsole Instance { get { return _instance; } }
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

        textObjects = new List<GameObject>();
    }

    #endregion
    [SerializeField] private int maxTexts = 12;
    public GameObject textFab;
    public Transform textHolder;
    private List<GameObject> textObjects;
    public void PrintToConsole(string textToPrint)
    {
        var go = Instantiate(textFab,textHolder);
        go.GetComponent<TextMeshProUGUI>().text = textToPrint;
        textObjects.Add(go);
        if(textObjects.Count > maxTexts){
            Destroy(textObjects[0]);
            textObjects.RemoveAt(0);
        }
    }
}
