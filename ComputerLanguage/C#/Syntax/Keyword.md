# 关键字

- ## fixed

  ```c#
  fixed关键字：一般在unsafe下使用，保证语句块内的变量地址不会移动（保证垃圾回收管理器能够正确回收变量）
  eg:
  	static unsafe void Test()
      {
          int[] arr = { 1, 2, 3 };
          fixed (int* p = arr)
          {
              p = p + 1; // Error: 不允许更改变量地址
              Console.WriteLine(*p);
              Console.WriteLine(*(p + 1));
          }
      }
  ```

- ## unchecked

  ```
  不检测数值溢出，不会抛出数值溢出的异常
  ```

- ## [volatile](https://www.cnblogs.com/OpenCoder/p/7723825.html)

  ```C#
  用法：修饰变量
  作用：volatile提醒编译器它后面所定义的变量随时都有可能改变，因此编译后的程序每次需要存储或读取这个变量的时候，都会直接从变量地址中读取数据。如果没有volatile关键字，则编译器可能优化读取和存储，可能暂时使用寄存器中的值，如果这个变量由多线程更新了的话，将出现不一致的现象。
  
  eg:
  public class Worker
  {
      private bool _shouldStop; // 该情况下线程不会退出
      //private volatile bool _shouldStop;
  
      public void DoWork()
      {
          bool work = false;
          // 注意：這裡會被編譯器優化為 while(true)
          while (!_shouldStop)
          {
              work = !work; // do sth.
          }
          Console.WriteLine("工作執行緒：正在終止...");
      }
  
      public void RequestStop()
      {
          _shouldStop = true;
      }
  }
  
  public class Program
  {
      public static void Main()
      {
          var worker = new Worker();
  
          Console.WriteLine("主執行緒：啟動工作執行緒...");
          var workerTask = Task.Run(worker.DoWork);
  
          // 等待 500 毫秒以確保工作執行緒已在執行
          Thread.Sleep(500);
  
          Console.WriteLine("主執行緒：請求終止工作執行緒...");
          worker.RequestStop();
  
          // 待待工作執行緒執行結束
          workerTask.Wait();
          //workerThread.Join();
  
          Console.WriteLine("主執行緒：工作執行緒已終止");
      }
  }
  ```

  

  

