# Thread
```csharp
// 创建一个线程并启动
var thread1 = new Thread(Run);
thread1.Start();
public void Run()
{
    // Do something
}

// 创建一个线程并启动（可传参）
var thread2 = new Thread(new ParameterizedThreadStart(Run));
thread2.Start();

public void Run(object args)
{
    // Do something
}

thread1.Abort(); // 终止线程
```
