using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallbackTest : MonoBehaviour
{
    
    void Start()
    {
        
    }

    public void DoSomething()
    {
        Debug.Log("让小明先去写作业");
        DoHomework();
    }

    private void DoHomework()
    {
        Debug.Log("小明开始写作业");
    }
}
