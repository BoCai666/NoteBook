## 简介
在.NET中，Parallel类的底层实现主要依赖于任务并行库（Task Parallel Library，TPL）。TPL是.NET Framework 4及更高版本中的一部分，它提供了一种易于使用的并行编程模型。

当你调用Parallel.Invoke，Parallel.For或Parallel.ForEach时，TPL会创建一组任务（Task），每个任务代表一个要并行执行的工作项。然后，这些任务会被调度到线程池中的线程上执行。线程池会尽可能地并行执行这些任务，以提高整体的执行效率。

值得注意的是，TPL使用了一种称为工作窃取（work-stealing）的技术来优化任务的调度。如果一个线程完成了所有的任务，它可以“窃取”其他线程的任务来执行，从而更好地利用系统资源。

此外，Parallel类还提供了一些高级功能，如并行循环的取消和异常处理

## API
```csharp
// 并行执行Action委托数组中的所有方法
Parallel.Invoke(
    () => { Console.WriteLine("1"); },
    () => { Console.WriteLine("2"); },
    () => { Console.WriteLine("3"); }
);

// 并行执行10次for循环（常规for循环为顺序执行）
Parallel.For(0, 10, i =>
{
    Console.WriteLine($"i: {i}");
});

// 并行执行10次foreach循环（常规foreach循环为顺序执行）
Parallel.ForEach(new List<int> { 1, 2, 3, 4, 5 }, i =>
{
    Console.WriteLine($"i: {i} {Thread.CurrentThread.ManagedThreadId}");
});
```