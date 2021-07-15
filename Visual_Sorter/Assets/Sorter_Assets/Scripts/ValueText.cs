using UnityEngine;
using UnityEngine.UI;

public class ValueText : MonoBehaviour{
    public void Setup()
{
        Text valueText = this.transform.GetComponentInChildren<Text>();
        valueText.text = this.transform.localScale.y.ToString();
    }
}
