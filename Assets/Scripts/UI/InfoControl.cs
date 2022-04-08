using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoControl : MonoBehaviour
{
    public Text text_info;
    public bool overrideTimer;

    float timer;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!overrideTimer)
            if (timer > 0)
                timer -= Time.deltaTime;
            else
                text_info.text = "";
    }

    public void NotifyText(int type, string text = "", float _timer = 3)
    {
        switch (type)
        {
            case 0:
                text_info.text = "YOU PICKED UP " + text;
                break;
            case 1:
                text_info.text = "THE DOOR IS LOCKED";
                break;
            case 2:
                text_info.text = "YOU NEED A KEYCARD";
                break;
            case 3:
                text_info.text = text;
                break;
        }
        timer = _timer;
    }
}
