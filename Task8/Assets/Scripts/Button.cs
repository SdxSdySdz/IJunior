using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button : MonoBehaviour
{
    [SerializeField] private Image _buttonBackground;

    public void OnButtonClick()
    {
        _buttonBackground.color = Random.ColorHSV();
    }
}
