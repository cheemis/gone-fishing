using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FisheringBarGraphic : MonoBehaviour
{
    public float percentFull;
    public Color statusColor;

    [SerializeField]
    private Image image;

    // Update is called once per frame
    void Update()
    {
        image.fillAmount = percentFull;
        image.color = statusColor;
    }
}
