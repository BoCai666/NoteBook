# 行为树

- ## 简介

  1. 行为树是一个树结构，根节点就是root节点，作为行为树的入口，节点类型为`Root`,每个行为树有且只有一个`Root`类型节点；
  2. 所有的叶子节点的类型一定是`Action`，同时`Action`类型的节点一定不能作为非叶子节点来使用。
  3. 非叶子节点也称为组合节点`Composition`，可以有一个或多个子节点，`root`节点一定只有一个子节点
  4. `Action`节点类型和`Composition`的节点类型可以做进一步的细分
  5. 每种类型的的组合节点能拥有的子节点数量与节点类型有关
  6. 一个节点的所有子节点是一个有序列表
  7. 有些节点可以附加特定参数来执行，有些节点则不需要参数
  8. 一颗行为树可以以叶子节点的形式被另外一颗行为树进行调用，就相当于一棵树挂接到了另外一棵树上一样

- ## 节点

  - **行为节点（ActionNode）：**行为树的叶子节点，一种字节点，存储的是具体的行为函数
  - **序列节点（SequenceNode）：**
    1. 行为树的非叶节点，一种组合节点，由多个子节点组成。
    2. 顺序调用所有字节点，当一个字节点失败时，则整个顺序节点失败
  - **选择节点（SelectorNode）：**
    1. 行为树的非叶节点，一种组合节点，由多个子节点组成
    2. 顺序调用所有字节点，当一个字节点成功时，则整个选择节点成功，当所有子节点失败时则整个选择节点失败
  - **并行节点（ParallelNode）：**
    1. 行为树的非叶节点，一种组合节点，由多个子节点组成
  - **条件节点（ConditionNode）：**
    1. 条件成功则节点成功，条件失败则节点失败
    2. 是一个字节点
  - **装饰节点（DecorateNode）：**
    1. 是一种控制分支节点，只包含一个子节点
    2. 先执行子节点的逻辑，再根据子节点的结果与自身的控制逻辑来决定整个装饰节点的状态

​		