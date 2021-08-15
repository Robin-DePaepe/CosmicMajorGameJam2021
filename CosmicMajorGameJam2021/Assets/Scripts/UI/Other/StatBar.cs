using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatBar : MonoBehaviour
{
    public RectTransform holderRect;
    public RectTransform progressRect;
    public RectTransform greenZoneRect;
    public RectTransform bufferLeftRect;
    public RectTransform bufferRightRect;
    public TextMeshProUGUI statNameText;
    public TextMeshProUGUI statDescriptionText;
    public TextMeshProUGUI pointProductionText;
    public TextMeshProUGUI barProgressText;
    internal Stat stat;
    internal Planet planet;
    internal int index;
    void Awake()
    {
        
    }

    void Update()
    {
        stat = planet.stats.list[index];
        
        statNameText.text = stat.statName;
        statDescriptionText.text = stat.description;
        SetProgress(stat.barProgress);
        SetGreenZone(stat.greenMin, stat.greenMax, stat.CalculateBuffer());
        pointProductionText.text = "Satisfaction: " + stat.pointsProduced + "/" + stat.PointProduction;
        barProgressText.text = stat.barProgress + "/" + 100;
    }

    void SetProgress(int progress)
    {
        float progressPos = progress / 100f * holderRect.rect.width;
        progressRect.localPosition = new Vector3( progressPos - (holderRect.rect.width/2) - (progressRect.rect.width/2), progressRect.localPosition.y);
    }

    void SetGreenZone(int min, int max, int buffer)
    {
        float width = holderRect.rect.width;
        float greenPos = min / 100f * width;
        float greenWidth = (max-min) / 100f * width;
        greenZoneRect.sizeDelta = new Vector2(greenWidth, greenZoneRect.sizeDelta.y);
        greenZoneRect.localPosition = new Vector3(greenPos - (width/2), greenZoneRect.localPosition.y);

        float bufferWidth = buffer / 100f * width;
        float bufferLeftPos = (min - buffer) / 100f * width;
        bufferLeftRect.sizeDelta = new Vector2(bufferWidth, bufferLeftRect.sizeDelta.y);
        bufferLeftRect.localPosition = new Vector3(bufferLeftPos - (width / 2), bufferLeftRect.localPosition.y);
        
        float bufferRightPos = max / 100f * width;
        bufferRightRect.sizeDelta = new Vector2(bufferWidth, bufferRightRect.sizeDelta.y);
        bufferRightRect.localPosition = new Vector3(bufferRightPos - (width / 2), bufferRightRect.localPosition.y);
        
        
    }
}
