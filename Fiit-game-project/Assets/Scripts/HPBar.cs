using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HPPlayerMover : MonoBehaviour
{
    [SerializeField] public Image hpFiil;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var player = GameObject.Find("Player");
        var Hp = player.GetComponent<health>().hp;
        OnHealthChanged(1 - 0.20f*Hp);
    }

    private void OnHealthChanged(float valueAsPercantage)
    {
        hpFiil.fillAmount = valueAsPercantage;
    }
}
    