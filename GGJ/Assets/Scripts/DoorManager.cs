using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorManager : MonoBehaviour
{
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

    public void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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

    void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        Debug.Log("can't proceed - door is locked");
        if (player != null && !isLocked)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
