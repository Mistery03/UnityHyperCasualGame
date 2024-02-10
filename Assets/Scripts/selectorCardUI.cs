using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class selectorCardUI : MonoBehaviour
{
    public GameObject selector;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openSelectorScreen()
    {
        selector.SetActive(true);
    }

    public void closeSelectorScreen() 
    {
        selector.SetActive(false);
    }


}
