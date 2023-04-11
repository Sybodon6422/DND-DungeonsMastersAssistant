using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceHudController : MonoBehaviour
{
    [SerializeField] Transform healthbar;
    public void UpdateHealthBar(int curHealth, int MaxHealth)
    {
        healthbar.localScale = new Vector3((float)curHealth/(float)MaxHealth,1,1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
