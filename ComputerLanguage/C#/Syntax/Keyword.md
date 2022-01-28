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

  

