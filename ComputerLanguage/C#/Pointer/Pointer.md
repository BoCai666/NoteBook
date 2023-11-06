## 指针
在C#中，指针只能被声明为持有值类型和数组的内存地址。与引用类型不同，指针类型不被默认的垃圾收集机制所跟踪。出于同样的原因，指针不允许指向引用类型，甚至不允许指向包含引用类型的结构类型。因此指针只能指向非托管类型，包括所有基本数据类型、枚举类型、其他指针类型和只包含非托管类型的结构。
可以使用unsafe关键词，开启不安全代码(unsafe code)开发模式。在不安全模式下，我们可以直接操作内存，这样就可以使用指针了。在不安全模式下，CLR并不检测unsafe代码的安全，而是直接执行代码。unsafe代码的安全需要开发人员自行检测。

```csharp
class People
{
    public int Age;   //值类型，不可以是属性
    public void ShowAge()
    {
        Console.WriteLine(Age);
    }
}

People people = new People();
people.Age = 10;
fixed(int* agePtr = &people.Age)
{
    *agePtr += 1;
}
people.ShowAge();  // 11
```