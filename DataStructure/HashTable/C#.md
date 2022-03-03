# 哈希表

- ## Hashtable

  ```c#
  C#内置的Hashtable类就是key-value结构的哈希表，但由于key与value都是object类型，会带来装箱和拆箱的问题，造成性能消耗，因而现在一般使用泛型的Dictionary来替代
  ```

- ## Dictionary<K, V>

  [具体实现](./Assets/MyDictionary.cs)

- ## HashSet\<T>

  ```
  内部实现了一个与Dictionary功能相同的东西，通过将存入HashSet中的值作为Dictionary的key来达到去重的效果
  ```
  
  