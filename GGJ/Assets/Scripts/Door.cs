using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    [Header("Components")]
    public Animator animator;

    [Header("Audio")]
    public AudioClip doorOpenSFX;

    int mNumRegisteredOrbs  = 0;
    int mNumOrbsObtained    = 0;
    bool mIsLocked = true;

    public void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void RegisterOrb()
    {
        mNumRegisteredOrbs++;
    }

    public void NotifyOrbObtained()
    {
        mNumOrbsObtained++;
        if (mNumOrbsObtained == mNumRegisteredOrbs)
        {
            mIsLocked = false;
            animator.SetTrigger("Open");
            AudioManager.PlayClip(doorOpenSFX);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player != null && !mIsLocked)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
