using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            StartCoroutine(FlipOrb());
        }
    }
    public Door door;

    [Header("Components")]
    public SpriteRenderer spriteRenderer;

    float mNextPickupTime = -1f;
    bool mObtained = false;

    private void Start()
    {
        door.RegisterOrb();
        mCrrtAbility = initialAbility;
        spriteRenderer.sprite = data.orbSprites[(int)mCrrtAbility];
    }

    private IEnumerator FlipOrb()
    {
        Vector3 angle = transform.eulerAngles;
        Vector3 initialPos = transform.position;
        float timeStart = Time.time;
        float t;
        do
        {
            t = (Time.time - timeStart) / 2f;
            float y = t < 0.5 ? 4 * t * t * t : 1 - Mathf.Pow(-2 * t + 2, 3) / 2;
            angle.y = 90f - (180f * Mathf.Abs(t - 0.5f));
            transform.eulerAngles = angle;
            transform.position = initialPos + Vector3.up * Mathf.Sin(y * Mathf.PI * 2) * 0.1f;
            if (t > 0.5f)
            {
                spriteRenderer.sprite = data.orbSprites[(int)mCrrtAbility];
            }
            yield return null;
        }
        while (t < 1);
        transform.position = initialPos;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (mNextPickupTime > Time.time)
        {
            return;
        }
        mNextPickupTime = Time.time + 1f;

        Player player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            if (!mObtained) door.NotifyOrbObtained();
            crrtAbility = player.SwapAbility(crrtAbility);
            mObtained = true;
        }
    }
}
