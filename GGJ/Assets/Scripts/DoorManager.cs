using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorManager : MonoBehaviour
{
    public string nextScene;

    int mNumRegisteredOrbs = 0;
    int mNumOrbsTouched = 0;

    bool mIsLocked = true;
    public bool isLocked
    {
        get
        {
            return mIsLocked;
        }
        set
        {
            mIsLocked = value;
            if(!mIsLocked)
            {
                Debug.Log("door unlocked");
                // TODO: trigger unlock animation
            }
        }
    }

    public void registerOrb()
    {
        mNumRegisteredOrbs++;
        Debug.Log("orb was registered");
    }

    public void notifyOrbTouched()
    {
        mNumOrbsTouched++;
        if (mNumOrbsTouched == mNumRegisteredOrbs)
        {
            isLocked = false;
        }
    }

    [Header("Components")]
    public SpriteRenderer spriteRenderer;

    void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        Debug.Log("can't proceed - door is locked");
        if (player != null && !isLocked)
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}
