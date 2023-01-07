using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Mono的管理者
public class MonoController : MonoBehaviour
{
    //帧更新事件
    private event UnityAction UpdateEvent;
    private void Start()
    {
        //此对象不可移除
        //从而方便别的对象找到该物体，从而获取脚本，从而添加方法
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        UpdateEvent?.Invoke();
    }
    //为外部提供的添加帧更新事件的方法
    public void AddUpdateListener(UnityAction func)
    {
        UpdateEvent += func;
    }
    //为外部提供的移除帧更新事件的方法
    public void RemoveUpdateListener(UnityAction func) {
        UpdateEvent -= func;
    } 
}
