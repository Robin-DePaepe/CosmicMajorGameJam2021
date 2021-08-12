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
        SetGreenZone(stat.greenPosition, stat.greenExtent, stat.CalculateBuffer());
    }

    void SetProgress(int progress)
    {
        float progressPercent = progress / 100f;
        progressRect.sizeDelta = new Vector2(holderRect.rect.width * progressPercent, progressRect.sizeDelta.y);
    }

    void SetGreenZone(int position, int extent, int buffer)
    {
        float width = holderRect.rect.width;
        float greenPos = position / 100f * width;
        float greenWidth = extent / 100f * width;
        greenZoneRect.sizeDelta = new Vector2(greenWidth, greenZoneRect.sizeDelta.y);
        greenZoneRect.localPosition = new Vector3(greenPos - (width/2), greenZoneRect.localPosition.y);

        float bufferWidth = buffer / 100f * width;
        float bufferLeftPos = (position - buffer) / 100f * width;
        bufferLeftRect.sizeDelta = new Vector2(bufferWidth, bufferLeftRect.sizeDelta.y);
        bufferLeftRect.localPosition = new Vector3(bufferLeftPos - (width / 2), bufferLeftRect.localPosition.y);
        
        float bufferRightPos = (position + extent) / 100f * width;
        bufferRightRect.sizeDelta = new Vector2(bufferWidth, bufferRightRect.sizeDelta.y);
        bufferRightRect.localPosition = new Vector3(bufferRightPos - (width / 2), bufferRightRect.localPosition.y);
        
        
    }
}
