using System;
using UnityEngine;
using UnityEngine.UI;

public class Calculate : MonoBehaviour
{
    public InputField num1InputField = null;
    public InputField num2InputField = null;
    public Dropdown operatorSelect = null;
    public Dropdown num1BaseSelect = null;
    public Dropdown num2BaseSelect = null;
    public Text outputText = null;

    public void PerformCalculation()
    {
        float num1 = GoToDecimal(num1InputField.text, num1BaseSelect.value);
        float num2 = GoToDecimal(num2InputField.text, num2BaseSelect.value);

        float tempValue = 0f;

        if (operatorSelect.value == 0)
        {
            tempValue = num1 + num2;
        }
        else if (operatorSelect.value == 1)
        {
            tempValue = num1 - num2;
        }
        else if (operatorSelect.value == 2)
        {
            tempValue = num1 * num2;
        }
        else
        {
            tempValue = num1 / num2;
        }

        outputText.text = Convert.ToString(tempValue);
    }

    private float GoToDecimal(string numText, int numBase)
    {
        long num = 0;

        switch(numBase)
        {
            case 0:
                num = Convert.ToInt64(numText, 2);
                break;
            case 1:
                num = Convert.ToInt64(numText, 8);
                break;
            case 2:
                num = Convert.ToInt64(numText, 10);
                break;
            default:
                num = Convert.ToInt64(numText, 16);
                break;
        }
        return (float)num;
    }
}
