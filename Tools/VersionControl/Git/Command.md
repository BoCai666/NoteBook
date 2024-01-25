# Git 命令

- ## 创建

  - **git clone：** 克隆仓库

    ```
    git clone <仓库地址> <文件夹名>
    git clone <仓库地址> <文件夹名> --recursive // 克隆仓库并初始化子模块
    ```
  - **git checkout**
    ```
    git checkout 文件名	// 撤销对文件的修改 
    ```
  - **git status：** 查看当前仓库状态
  
  - **git log**: 查看提交历史
  

- ## 配置
  - **git config：** git配置

    ```
    git config --global user.name "用户名"： 配置用户名
    git config --global user.email 邮箱： 配置邮箱
    git config --global https.proxy： 配置代理
    git config --gloabl --unset https.proxy： 取消代理
    git config --list： 查看所有git配置
    git config \<key>： 查看某项配置，如：git config user.name
    ```

- ## 上传
  
  - **git add：** 跟踪文件

    ```
    git add .		// 跟踪当前所有修改的文件和未跟踪的文件
    git add 文件名	  // 跟踪指定文件	 
    ```

  - **git commit：** 提交文件到本地暂存区

    ```
    git commit -m "提交描述" // 提交文件到暂存区
    git commit --amend: 修改最后一次提交，即用新的提交覆盖旧的提交
    ```

  - **git push：** 推送本地仓库的文件至远程仓库
    ```
    git push --set-upstream origin 分支名：用于将本地分支推送到远程仓库，并同时建立本地分支与远程分支之间的关联（后续可直接使用git push推送）
    ```

- ## 拉取

  - **git pull：** 拉取远程文件并与本地版本合并， 是`git fetch` 和 `git merge FETCH_HEAD` 的简写
    ```
    git pull <远程主机名> <远程分支名>:<本地分支名>
    ```
- ## 撤销
  - **git reset：** 撤销commit
    
    ```
    --soft // 不删除工作空间的改动，撤销commit，不撤销add file
    --hard // 删除工作空间的改动，撤销commit，并撤销add file
    git reset --soft HEAD~2 // 表示撤销前两次的commit 
    ```
- ## 远程
  - **git remote：** 远程仓库
  
    ```
    git remote -v：查看远程仓库信息
    git remote add origin "仓库地址"：添加远程仓库地址
    ```
- ## 分支
  master：指代默认分支  
  origin：远程克隆仓库的默认名字
  ```git
  git branch -a：查看所有分支
  git branch -d 分支名：删除分支
  git branch 分支名：创建分支
  git checkout 分支名：切换分支
  ```    
- ## submodule
  submodule允许你将一个Git仓库当作另外一个Git仓库的子目录，submodule本质上是另一个代码仓库，对submodule的修改，只能在submodule对应的代码仓库修改和提交，然后由父代码仓库更新它下面的submodule最新状态
  ```
  git submodule // 查看子模块
  git submodule add <子项目url> <子项目存放到本地的文件夹名> // 添加子模块
  git submodule update --remote // 更新子模块为远程最新版本
  ```
