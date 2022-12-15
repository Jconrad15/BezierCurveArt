using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SliderDisplay : MonoBehaviour
{
    private TextMeshProUGUI textMesh;

    [SerializeField]
    private float startDisplayValue = 0;

    private void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        Display(startDisplayValue);
    }

    public void Display(float value)
    {
        textMesh.SetText(value.ToString(value % 1 == 0 ? "F0" : "F2"));
    }


}
