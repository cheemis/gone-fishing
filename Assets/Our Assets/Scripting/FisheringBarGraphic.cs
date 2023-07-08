using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FisheringBarGraphic : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Have 1 color for each input")]
    private List<Color> inputColors;
    public float percentFull;
    public float timeRemainingPercent;
    public Color statusColor;

    [SerializeField]
    private Image image;

    [SerializeField]
    private Image timer;

    //internal keyword generated by vsc extension
	internal void ChangeColor(char v) {
        switch (v) {
            case 'U':
                statusColor = inputColors[0];
                return;
            case 'L':
                statusColor = inputColors[1];
                return;
            case 'R':
                statusColor = inputColors[2];
                return;
            case 'D':
                statusColor = inputColors[3];
                return;
        }
	}

	// Update is called once per frame
	void Update()
    {
        image.fillAmount = percentFull;
        image.color = statusColor;
        timer.fillAmount = timeRemainingPercent;
    }
}
