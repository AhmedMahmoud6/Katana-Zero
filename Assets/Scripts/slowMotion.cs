using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slowMotion : MonoBehaviour
{
    public float slowMotionTimeScale;

    private float startTimeScale;
    private float startFixedDeltaTime;



    // Start is called before the first frame update
    void Start()
    {
        startTimeScale = Time.timeScale;
        startFixedDeltaTime = Time.fixedDeltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartSlowMotion();
        }
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            StopSlowMotion();
        }
    }



    private void StartSlowMotion()
    {
        Time.timeScale = slowMotionTimeScale;
        Time.fixedDeltaTime = startFixedDeltaTime * slowMotionTimeScale;
    }

    private void StopSlowMotion()
    {
        Time.timeScale = startTimeScale;
        Time.fixedDeltaTime = startFixedDeltaTime;
    }

}
