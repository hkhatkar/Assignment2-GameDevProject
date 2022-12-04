using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerPowerBar : MonoBehaviour
{
private Image content;
    public float MyMaxPowerValue { get; set; }
    public float currentValue;
    private float currentFill;
    UseShield shieldScript;
   // public float InitializedHealth;
    //NewPLATPlayerMovement playerscript;
    //Image UIFill, UIBackground;

    public float MyCurrentValue
    {//This function is responsible for setting players max health and varying current health
        get
        {//Return the current health value
            return currentValue;
        }
        set
        {
            if (value >= MyMaxPowerValue)
            {
                currentValue = MyMaxPowerValue;
                //UIFill.enabled = false;       //like stamina in PI, UI will only appear when sprint pressed
                //UIBackground.enabled = false;

            }//makes sure current value doesnt go above max value
            else if (value <= 0)
            {
                currentValue = 0;
               // playerscript.anim.SetBool("PLATSprinting", false);
                // playerscript.SprintCoolDown = true;
                StartCoroutine(coolDownTimer());
            }
            else
            {
                currentValue = value;
              //  UIFill.enabled = true;
               // UIBackground.enabled = true;
            }
            currentFill = currentValue / MyMaxPowerValue;
        }
    }
    private void Start()
    {
       // UIFill = gameObject.GetComponent<Image>();
     //  UIBackground = transform.parent.GetComponentInParent<Image>();
        shieldScript = GameObject.FindGameObjectWithTag("Player").GetComponent<UseShield>();
        //playerscript = GameObject.FindObjectOfType<NewPLATPlayerMovement>();
        StartCoroutine(RegeneratePower());
        content = GetComponent<Image>();
        Initialize(200f, 200f);
    }
    void Update()
    {

        content.fillAmount = currentFill;
        //The health bar will be constantly refilled as health changes
    }
    public void Initialize(float currentValue, float maxValue)
    {
        MyMaxPowerValue = maxValue;
        MyCurrentValue = currentValue;
        //initializes the current value and max value to be displayed in UI
    }

    IEnumerator RegeneratePower()
    {
        while (true) //loops forever
        {
            if (Input.GetKey(KeyCode.Mouse1) && shieldScript.disabledShield == false)//&& !playerscript.SprintCoolDown)
            {
                MyCurrentValue--;
                yield return new WaitForSeconds(0.02f);
            }
            else
            {
                MyCurrentValue++;
                yield return new WaitForSeconds(0.02f);
            }
        }
    }
    public IEnumerator coolDownTimer()
    {
        yield return new WaitForSeconds(5f);
       // playerscript.SprintCoolDown = false;
    }
}
