using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] public Image totalHealthbar;
    [SerializeField] private Image currentHealthbar;
    // Start is called before the first frame update
    void Start()
    {
        //totalHealthbar.fillAmount = playerHealth.curHealth / 10; 
    }

    void Update()
    {
        setTotalHealthbar();
        currentHealthbar.fillAmount = playerHealth.curHealth / 10;
    }
    private void setTotalHealthbar()
    {
        if (totalHealthbar.fillAmount * 10 < playerHealth.startingHealth)
        {
            print("zaletel");
            totalHealthbar.fillAmount = playerHealth.curHealth / 10;
        }
    }
}
