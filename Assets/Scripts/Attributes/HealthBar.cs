using RPG.Attributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Health healthComponent;
    [SerializeField] RectTransform foreground;
    [SerializeField] Canvas healthBarCanvas;

    private void Update()
    {
        if (Mathf.Approximately(healthComponent.GetFraction(), 0) ||
            Mathf.Approximately(healthComponent.GetFraction(), 1))
        {
            healthBarCanvas.enabled = false;
            return;
        }
        healthBarCanvas.enabled = true;
        foreground.localScale = new Vector3(healthComponent.GetFraction(), 1, 1);
    }
}
