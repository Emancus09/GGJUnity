using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/OrbData")]
public class OrbData : ScriptableObject
{
    public Sprite[] orbSprites;
}

public class Orb : MonoBehaviour
{
    [Header("General")]
    public OrbData data;

    [Header("Mechanics")]
    public Ability initialAbility = Ability.NULL;
    Ability mCrrtAbility;

    public Ability crrtAbility
    {
        get 
        { 
            return mCrrtAbility; 
        }
        set
        {
            mCrrtAbility = value;
            spriteRenderer.sprite = data.orbSprites[(int)mCrrtAbility];
            Debug.Log(value);
        }
    }

    [Header("Components")]
    public SpriteRenderer spriteRenderer;

    private void Start() 
    {
        crrtAbility = initialAbility;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            crrtAbility = player.SwapAbility(crrtAbility);
        }
    }
}
