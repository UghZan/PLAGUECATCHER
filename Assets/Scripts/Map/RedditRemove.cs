using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedditRemove : MonoBehaviour
{
    public Vector3 openPos;
    public float speed;

    Vector3 target;
    AudioSource s;
    float timer = 0;
    bool yes = false;
    // Start is called before the first frame update
    void Start()
    {
        target = transform.localPosition;
        s = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(yes)
            timer += Time.deltaTime * speed;
        transform.localPosition = Vector3.Lerp(transform.localPosition, target, timer);
        if (Vector3.Distance(transform.localPosition, target) > 0.2f)
        {
            s.UnPause();
        }
        else
        {
            s.Pause();
        }
    }

    public void SwitchStates(Player p)
    {
        target = openPos;
        yes = true;
    }

    public void Final()
    {

    }
}
