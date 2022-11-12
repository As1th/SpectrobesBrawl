using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TipChanger : MonoBehaviour
{
    public TextMeshProUGUI text;
    int i =0;
    // Start is called before the first frame update
    public void Start()
    {
       
    }

    public void OnEnable()
    {
        text = GetComponent<TextMeshProUGUI>();
        i = Random.Range(0, 7);
        changeText(i);
    }
    public void changeText(int i)
    {
       
        switch (i)
        {
            default:
            case 0:
                text.text = "TIP: YOU ARE INVINCIBLE WHILE DASHING OR USING YOUR CH ATTACK!";
                break;
            case 1:
                text.text = "TIP: YOUR DASH AND ATTACKS ARE SUBJECT TO A BRIEF COOLDOWN.";
                break;
            case 2:
                text.text = "TIP: TIME YOUR NORMAL ATTACKS PROPERLY TO DEFLECT ENEMY PROJECTILES BACK AT THEM!";
                break;
            case 3:
                text.text = "TIP: THE EVOLVED FORM SPIKANOR IS COMPLETELY IMMUNE TO DAMAGE!";
                break;
            case 4:
                text.text = "TIP: DASH OFTEN TO DODGE ENEMY ATTACKS!";
                break;
            case 5:
                text.text = "TIP: LONG-RANGE OR DASH-ATTACK KRAWL ARE MORE FRAGILE THAN CLOSE-RANGE KRAWL.";
                break;
            case 6:
                text.text = "TIP: A SINGLE WAVE IS MADE UP OF ABOUT 10 KRAWL SPAWNING A FEW AT A TIME.";
                break;


        }
       
    }

    public void incrementText()
    {
        i++;
        if (i == 7)
        { i =0; }
        changeText(i);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
