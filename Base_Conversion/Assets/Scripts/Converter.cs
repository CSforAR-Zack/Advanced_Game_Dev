using System;
using UnityEngine;
using UnityEngine.UI;

public class Converter : MonoBehaviour
{
    public InputField valueInput = null;
    public Dropdown valueBase = null;
    public Text binaryText = null;
    public Text octalText = null;
    public Text decimalText = null;
    public Text hexidecimalText = null; 
    
    string value = "";
    long numValue = 0;

    public void ConvertValue()
    {
        value = valueInput.text;

        if (value == "") 
        {
            binaryText.text = "No Value";
        }
        else
        {
            if (valueBase.value == 0) {numValue = Convert.ToInt64(value, 2);}
            else if (valueBase.value == 1) {numValue = Convert.ToInt64(value, 8);}
            else if (valueBase.value == 2) {numValue = Convert.ToInt64(value, 10);}
            else if (valueBase.value == 3) {numValue = Convert.ToInt64(value, 16);}
            
            binaryText.text = Convert.ToString(numValue, 2);
            octalText.text = Convert.ToString(numValue, 8);
            decimalText.text = Convert.ToString(numValue, 10);
            hexidecimalText.text = Convert.ToString(numValue, 16);

        }
    }
}
