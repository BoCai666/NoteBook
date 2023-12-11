- ## [async_await](https://github.com/BoCai666/Promise)
    async和await关键字用于异步编程，async关键字用于声明一个方法是异步的。这意味着该方法将在后台线程上运行，而不会阻塞主线程。异步方法通常返回一个Task或Task<T>对象，表示一个正在进行的工作。
    await关键字用于等待一个异步操作的完成。当你在异步方法中使用await关键字，编译器会自动将该方法分割为多个部分。在await表达式完成之前，方法的执行将被暂停，控制权将返回给方法的调用者。当await表达式完成时，方法将从暂停的地方继续执行。

- ## 示例
    ```csharp
    // 源码
    public class Program
    {
        static int Work()
        {
            Console.WriteLine("In Task.Run");
            return 1;
        }

        static async Task TestAsync()
        {
            Console.WriteLine("Before Task.Run");
            await Task.Run(Work);
            Console.WriteLine("After Task.Run");
        }

        static void Main()
        {
            _ = TestAsync();
            Console.WriteLine("End");
            Console.ReadKey();
        }
    }

    // 编译器生成代码
    class Program
    {
        static int Work()
        {
            Console.WriteLine("In Task.Run");
            return 1;
        }

        static Task TestAsync()
        {
            var stateMachine = new StateMachine()
            {
                _builder = AsyncTaskMethodBuilder.Create(),
                _state = -1
            };
            stateMachine._builder.Start(ref stateMachine);
            return stateMachine._builder.Task;
        }

        static void Main()
        {
            _ = TestAsync();
            Console.WriteLine("End");
            Console.ReadKey();
        }

        class StateMachine : IAsyncStateMachine
        {
            public int _state;
            public AsyncTaskMethodBuilder _builder;
            private TaskAwaiter<int> _awaiter;

            void IAsyncStateMachine.MoveNext()
            {
                int num = _state;
                try
                {
                    TaskAwaiter<int> awaiter;
                    if (num != 0)
                    {
                        Console.WriteLine("Before Task.Run");
                        awaiter = Task.Run(Work).GetAwaiter();
                        if (!awaiter.IsCompleted)
                        {
                            _state = 0;
                            _awaiter = awaiter;
                            StateMachine stateMachine = this;
                            _builder.AwaitUnsafeOnCompleted(ref awaiter, ref stateMachine);
                            return;
                        }
                    }
                    else
                    {
                        awaiter = _awaiter;
                        _awaiter = default;
                        _state = -1;
                    }
                    awaiter.GetResult();
                    Console.WriteLine("After Task.Run");
                }
                catch (Exception exception)
                {
                    _state = -2;
                    _builder.SetException(exception);
                    return;
                }
                _state = -2;
                _builder.SetResult();
            }

            void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine) { }
        }
    }
    ```