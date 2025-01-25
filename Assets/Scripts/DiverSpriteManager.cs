using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiverSpriteManager : MonoBehaviour
{
    [SerializeField] Sprite[] umbrellaSprites;
    [SerializeField] SpriteRenderer umbrellaRenderer;

    [SerializeField] private GameObject umbrellaPivot;

    private float MaxUmbrellaAngle = 30;
    public void SetSprite(int spriteIndex)
    {
        if(spriteIndex >= 0 && spriteIndex < umbrellaSprites.Length)
            umbrellaRenderer.sprite = umbrellaSprites[spriteIndex];
    }

    
    public void SetUmbrellaRotation(float rotation)
    {
        umbrellaPivot.transform.localRotation = Quaternion.Euler(0,0, MaxUmbrellaAngle * rotation);
        
    }
}
