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

- ## StrcutLayout
  - **LayoutKind.Auto：** 运行时自动选择字段的布局。这可能会根据特定的实现和机器架构进行优化（类的默认布局方式）
  - **LayoutKind.Sequential：** 字段将按照它们在代码中声明的顺序进行布局（结构体的默认布局方式）
  - **LayoutKind.Explicit：** 开发者可以显式控制每个字段的位置。通过FieldOffset属性可以指定每个字段的位置
  
- ## decimal
    - **简介：** 占16个字节，128位，28-29位有效数字，精度高，但计算速度慢，适用于财务或金融软件。
    - **公式：** **符号 * 尾数 / 10 ^指数** 
        - 组成：尾数部分有96位（12字节），指数部分有效的只有5位，符号位有1位
        - 下面使用m表示尾数部分、e表示指数部分、s表示符号位：

            1~4号字节: mmmm mmmm mmmm mmmm mmmm mmmm mmmm mmmm 尾数的低阶部分

            5~8号字节: mmmm mmmm mmmm mmmm mmmm mmmm mmmm mmmm 尾数的中阶部分

            9~12号字节: mmmm mmmm mmmm mmmm mmmm mmmm mmmm mmmm 尾数的高阶部分

            13~16号字节: 0000 0000 0000 0000 000e eeee 0000 000s
        - 指数部分：表示的负指数，（000e eeee）只有5位是有效的，这是因为它的最大值只能到28，且指数部分的底数是10，指数部分控制的便是我们要在28位整数的哪一位点上小数点。
        - 尾数部分：表示的是一个29位或者28位的整数（之所以这样说是由于最高位29的值其实只能到7，所以总共只有28位的值是可以任意设置的）。
        - 范围：decimal能正确表示的数字范围位是-/+79228162514264337593543950335，但是也正是由于decimal 可以表示的十进制数字的有效位数也在28或29（取决于最高位的值是否在7以内）的范围内，因此在表示小数的时候，对小数的位数也是有限制的。


        
