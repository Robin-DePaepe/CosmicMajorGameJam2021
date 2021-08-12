using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaySpriteSheet : MonoBehaviour
{

    public Image image;

    public Texture2D spriteSheet;
    private Sprite[] sprites;

    private void Start()
    {
        //sprites = Resources.LoadAll<Sprite>(Texture.name)
    }

    void Update()
    {
        
    }
}
