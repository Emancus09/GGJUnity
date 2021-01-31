using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    static GameObject instance;

    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this.gameObject;
        DontDestroyOnLoad(this);
    }
}
