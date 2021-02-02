using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Sprite[] greyArrows;
    public Sprite[] colorArrows;
    public Image[] currentArrows;

    public InputManager inputManager;

    // Update is called once per frame
    void Update()
    {
        setAbilities();
    }

    void setAbilities(){
        bool[] abilities = inputManager.getAllAbilitiesAvail();
        for (int i = 0; i < currentArrows.Length; i++){
            if (abilities[i]){
                currentArrows[i].sprite = 
                    colorArrows[i];
            }
            else {
                currentArrows[i].sprite = 
                    greyArrows[i];
            }
        }
    }
}
