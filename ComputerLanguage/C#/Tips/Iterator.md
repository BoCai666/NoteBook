# 迭代器

- ## [简介](https://www.cnblogs.com/murongxiaopifu/p/4437432.html)

  迭代器方便访问集合中的元素，而不会修改集合本身，不用关心各种集合内部具体的实现即可访问内部元素。

- ## 实现方式

  - **IEnumerable、IEnumerable\<T>：**一个集合能够被遍历（foreach）访问内部元素，需继承实现该接口

  ```C#
  	class MyCollection : IEnumerable
      {
          // 需实现获取迭代器的方法
          public IEnumerator GetEnumerator()
          {
              throw new NotImplementedException();
          }
      }
  ```

  

  - **IEnumerator、IEnumerator\<T>：**实现该接口的类即为迭代器

  ```c#
  	class MyIterator : IEnumerator
      {
          public object Current => throw new NotImplementedException();
          public void Dispose()
          {
              throw new NotImplementedException();
          }
          public bool MoveNext()
          {
              throw new NotImplementedException();
          }
          public void Reset()
          {
              throw new NotImplementedException();
          }
      }
  ```

  - **实现上述IEnumerable接口的类即可被foreach遍历，具体步骤**
    1. 调用IEnumerable的GetEnumerator方法获取迭代器
    2. 执行IEnumerator的MoveNext方法，若返回true则输出Current，若返回false则遍历终止

- ## yield 

  - **yield break：**终止迭代器继续迭代

  - **yield return：**上述实现迭代器的步骤比较繁杂，C#通过yield关键字可快速实现一个迭代器，是一种语法糖。

    - **yield return所在方法返回值为IEnumerator**

    ​	eg：

    ```c#
    	class EnumerableClass<T> : IEnumerable<T>
            {
                public IEnumerator<T> iterator;
                public IEnumerator<T> GetEnumerator()
                {
                    return iterator;
                }
                IEnumerator IEnumerable.GetEnumerator()
                {
                    throw new NotImplementedException();
                }
            }
    
            static unsafe void Main(string[] args)
            {
                var enumeableClass = new EnumerableClass<int>();
                enumeableClass.iterator = YieldIEnumeratorTest();
                foreach (var item in enumeableClass)
                {
                    Console.WriteLine(item); // 输出 0 - 9
                }
                Console.ReadKey();
            }
    
            static IEnumerator<int> YieldIEnumeratorTest()
            {
                for (int i = 0; i < 10; i++)
                {
                    yield return i;
                }
            }
    ```

    ​	解析：编译器在编译阶段会生成一个迭代器，大致实现如下：

    ```c#
    	static IEnumerator<int> YieldIEnumeratorTest()
        {
            return new <TestIterator>d__0(0)    
        }
    
     	private sealed class <TestIterator>d__0 : IEnumerator<int>, IEnumerator, IDisposable
        {
            // Fields 字段：state和current是默认出现的
            private int <>1__state;
            private int <>2__current;
            public int <i>5__1;//<i>5__1来自我们迭代器块中的局部变量
    
            // Methods 构造函数，初始化状态
            [DebuggerHidden]
            public <TestIterator>d__0(int <>1__state)
            {
                this.<>1__state = <>1__state;
            }
            // 几乎所有的逻辑在这里
            private bool MoveNext()
            {
                switch (this.<>1__state)
                {
                    case 0:
                        this.<>1__state = -1;
                        this.<i>5__1 = 0;
                        while (this.<i>5__1 < 10)
                        {
                            this.<>2__current = this.<i>5__1;
                            this.<>1__state = 1;
                            return true;
                        Label_0046:
                            this.<>1__state = -1;
                            this.<i>5__1++;
                        }
                        break;
                    case 1:
                        goto Label_0046;
                }
                return false;
            }
    
            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }
            void IDisposable.Dispose()
            {
            }
            // Properties
            int IEnumerator<int>.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.<>2__current;
                }
            }
            object IEnumerator.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.<>2__current;
                }
            }
        }
    ```

    - **yield return所在方法返回值为IEnumerable**

      eg：

      ```c#
      		static unsafe void Main(string[] args)
              {
                  foreach (var item in YieldIEnumerableTest())
                  {
                      Console.WriteLine(item);
                  }
                  Console.ReadKey();
              }
      
              static IEnumerable<int> YieldIEnumerableTest()
              {
                  for (int i = 0; i < 10; i++)
                  {
                      yield return i;
                  }
              }
      ```

      解析：

      ```c#
      	static IEnumerable<int> YieldIEnumerableTest()
          {
              return new <TestIterator>d__0(-2);
          }
      
          private sealed class <TestIterator>d__0 : IEnumerable<int>, IEnumerable, IEnumerator<int>, IEnumerator, IDisposable
          {
              // Fields
              private int <>1__state;
              private int <>2__current;
              private int <>l__initialThreadId;
              public int <count>5__1;
      
              public <TestIterator>d__0(int <>1__state)
              {
                  this.<>1__state = <>1__state;
                  this.<>l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
              }
      
              private bool MoveNext()
              {
                  switch (this.<>1__state)
                  {
                      case 0:
                          this.<>1__state = -1;
                          this.<count>5__1 = 0;
                          while (this.<count>5__1 < 10)
                          {
                              this.<>2__current = this.<count>5__1;
                              this.<>1__state = 1;
                              return true;
                          Label_0046:
                              this.<>1__state = -1;
                              this.<count>5__1++;
                          }
                          break;
                      case 1:
                          goto Label_0046;
                  }
                  return false;
              }
      
              IEnumerator<int> IEnumerable<int>.GetEnumerator()
              {
                  if ((Thread.CurrentThread.ManagedThreadId == this.<>l__initialThreadId) && (this.<>1__state == -2))
                  {
                      this.<>1__state = 0;
                      return this;
                  }
                  return new Test.<TestIterator>d__0(0);
              }
      
              IEnumerator IEnumerable.GetEnumerator()
              {
                  return ((IEnumerable<Int32>) this).GetEnumerator();
              }
      
              void IEnumerator.Reset()
              {
                  throw new NotSupportedException();
              }
      
              void IDisposable.Dispose()
              {
              }
      
              int IEnumerator<int>.Current
              {
                  get
                  {
                      return this.<>2__current;
                  }
              }
      
              object IEnumerator.Current
              {
                  get
                  {
                      return this.<>2__current;
                  }
              }
          }
      ```

      

  
