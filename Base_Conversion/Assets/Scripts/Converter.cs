using System;
using UnityEngine;
using UnityEngine.UI;

public class Converter : MonoBehaviour
{
    public InputField valueInput = null;
    public Dropdown options = null;
    public Text binaryText = null;
    public Text octalText = null;
    public Text decimalText = null;
    public Text hexText = null;

    string value = "";
    long numValue = 0;

    public void ConvertValue()
    {
        value = valueInput.text;

        if (value == "")
        {
            binaryText.text = "No value";
        }
        else
        {
            if (options.value == 0) {numValue = Convert.ToInt64(value, 2);}
            else if (options.value == 1) {numValue = Convert.ToInt64(value, 8);}
            else if (options.value == 2) {numValue = Convert.ToInt64(value, 10);}
            else {numValue = Convert.ToInt64(value, 16);}

            binaryText.text = Convert.ToString(numValue, 2);
            octalText.text = Convert.ToString(numValue, 8);
            decimalText.text = Convert.ToString(numValue, 10);
            hexText.text = Convert.ToString(numValue, 16);
        }
    }
}
