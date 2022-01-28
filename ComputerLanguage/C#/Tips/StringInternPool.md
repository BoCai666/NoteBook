# 字符串常量池

- ## 简介

  .Net的CLR在运行期间会管理一个字符串常量池（类似Dictionary），用于重用字符串对象，避免频繁开辟内存。

- ## 常量池

  - **常量池存在于常量区**

  - **编译期间的可确定的字符串对象会放入常量池**
  - **运行期间使用string.Intern()生成的字符串对象会放入常量池**
  - **运行期间动态创建的字符串不会放入常量池**

  ```C#
  // a b c 三者变量指向地址相同，d与其他不同
  eg:
  	string a = "123";
  	string b = "12" + "3";
  	string c = string.Intern("123");
  	string d = new StringBuilder().Append("123").ToString();
  ```

  

