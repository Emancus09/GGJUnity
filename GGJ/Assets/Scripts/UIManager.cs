using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Sprite[] greyArrows;
    public Sprite[] colorArrows;
    public GameObject[] currentArrows;

    public InputManager mInputMgr;

    // Update is called once per frame
    void Update()
    {
        setAbilities();
    }

    void setAbilities(){
        bool[] abilities = mInputMgr.getAllAbilitiesAvail();
        for (int i = 0; i < currentArrows.Length; i++){
            if (abilities[i]){
                currentArrows[i].GetComponent<SpriteRenderer>().sprite = 
                    colorArrows[i];
            }
            else {
                currentArrows[i].GetComponent<SpriteRenderer>().sprite = 
                    greyArrows[i];
            }
        }
    }
}
