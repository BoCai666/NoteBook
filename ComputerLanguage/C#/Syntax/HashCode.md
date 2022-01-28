# HashCode

- ## int

  ```
  GetHashCode()：即int值本身
  ```

  

- ## short

  ```c#
  GetHashCode()：将short数左移16位再与自身进行或操作
  eg:
  	short a = 10;
  	int hashcode = (ushort)a | a << 16;
  ```

  

- ## long

  ```c#
  GetHashCode()：将long值前32位与后32位进行异或操作得到的数
  eg:
  	long value_Long = 10;
  	int hashcode = (int)value_Long ^ (int)(value_Long >> 32);
  ```

  

- ## float

  ```c#
  GetHashCode()：将float数的二进制值按int类型二进制解释所得到的值(即将float指针转为int指针后取地址中的内容)
  eg: 
  	float a = 12.5f;
  	var p_Float = &a;
  	var p_Int = (int*)p_Float;
  	int hashCode = *p_Int;
  ```

  

- ## double

  ```c#
  GetHashCode()：将double数的二进制按long类型二进制解释得到的值，再将long类型值前32位与后32位进行异或操作得到的数即为hashcode
  eg:
  	double a = 12.5;
  	var p_Float = &a;
  	var p_Long = (long*)P_Float;
  	var value_Long = *p_Long;
  	int hashcode = (int)value_Long ^ (int)(value_Long >> 32);
  ```

  

- ## string

  ```apl
  C#.NetCore 默认启用使用随机生成hash算法来生成string的HashCode，为了避免网络方面Hash攻击导致Hash溢出，使HashTable这类的数据结构在存储数据时Hash碰撞太多，复杂度由o(1)变为o(n^2)。
  随机生成hash的算法，每次运行程序时同样的string变量的HashCode都会不一致。
  
  固定生成Hash的算法：
  static int GetDeterministicHashCode(string str)
  {
      unchecked
      {
          int hash1 = (5381 << 16) + 5381;
          int hash2 = hash1;
  
          for (int i = 0; i < str.Length; i += 2)
          {
              hash1 = ((hash1 << 5) + hash1) ^ str[i];
              if (i == str.Length - 1)
              break;
              hash2 = ((hash2 << 5) + hash2) ^ str[i + 1];
      	}
      	return hash1 + (hash2 * 1566083941);
  	}
  }
  ```

  

