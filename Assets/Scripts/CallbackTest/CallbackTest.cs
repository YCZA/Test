using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using System;
using System.Runtime.InteropServices;
using Random = System.Random;

public class CallbackTest : MonoBehaviour
{
    public async void Test()
    {
        //一、回调方法：
        //在FirstStep中得到result后，需要我们告诉他下一步如何操作result
        //FirstStep(1, 1, SecondStep); //直接输出结果
        //FirstStep(1, 1, i => { Debug.Log(i * 2); }); //结果乘2

        //二、异步回调
        //回调函数的本质就是“只有我们才知道做些什么(Handle)，但是我们并不清楚什么时候去做这些，
        //只有其它模块(ThirdPartyLibrary 第三方库)才知道，
        //因此我们必须把我们知道的封装成回调函数告诉其它模块”
        ThirdPartyLibrary(10, Handle).Forget();

        //常规模式：调用完S服务后 我 去执行X任务，
        //回调模式：调用完S服务后 你 接着再去执行X任务，

        //三、UniTask
        //使用UniTask的UniTaskCompletionSource将异步回调包装成可await形式
        //这个Download方法可以等待，也可以不等待
        //Download();
        //int result = await Download();
        //Debug.Log(result);

        //尝试使用UniTaskCompletionSource包装上面的游戏关卡的例子


        Debug.Log("继续往下执行...");
    }

    private void FirstStep(int a, int b, Action<int> nextStep)
    {
        int result = a + b;
        nextStep(result);
    }

    private void SecondStep(int result)
    {
        Debug.Log(result);
    }

    //这一部分的操作可能是在第三方库，可能在另一个脚本
    //比如我们需要在游戏关卡内统计玩家的某一数值，等玩家退出关卡后，根据数值发放奖励
    //在关卡外我们不知道关卡中的具体统计方式，但我们能指定在退出关卡后对数值的下一步操作
    //比如在Handle函数中，对数值进行判断
    private async UniTaskVoid ThirdPartyLibrary(int num, Action<int> callback)
    {
        Debug.Log("Begin do something...");
        await UniTask.Delay(TimeSpan.FromSeconds(2));
        num += UnityEngine.Random.Range(0, 100);
        Debug.Log($"Get result: " + num);
        //这里的调用可以选择调用也可以选择不调用
        //比如大于50分才能触发奖励
        if (num > 50)
        {
            callback(num);
        }

        Debug.Log("End third-party library");
    }

    private void Handle(int num)
    {
        Debug.Log("Begin callback...");
        if (num >= 90)
            Debug.Log("完美");
        else if (num >= 70)
            Debug.Log("很棒");
        else
            Debug.Log("一般");
        Debug.Log($"End callback");
    }

    private UniTask<int> Download()
    {
        var downloadTask = new UniTaskCompletionSource<int>();
        Debug.Log("开始");

        FakeDownload(i =>
        {
            //FakeDownload完成后，设置utsc的result
            downloadTask.TrySetResult(i);
        }).Forget();

        Debug.Log("结束");
        return downloadTask.Task; //返回utcs中的Task，让这个方法可等待
    }

    private async UniTask FakeDownload(Action<int> callBack)
    {
        Debug.Log("开始下载");
        await UniTask.Delay(TimeSpan.FromSeconds(2));
        int result = UnityEngine.Random.Range(0, 100);
        Debug.Log("下载完成");
        callBack(result);
    }
}