# Git 命令

- ## 配置

  - **git config --global user.name "用户名"：** 配置用户名
  - **git config --global user.email 邮箱：** 配置邮箱
  - **git config --list：** 查看所有git配置
  - **git config \<key>：** 查看某项配置，如：git config user.name

- ## 上传

  - **git add：** 跟踪文件

    ```
    git add .		// 跟踪当前所有修改的文件和未跟踪的文件
    git add 文件名	  // 跟踪指定文件	 
    ```

  - **git commit：** 提交文件到本地暂存区

    ```
    git commit -m "提交描述" // 提交文件到暂存区
    ```

  - **git push：** 推送本地缓存区的文件至远程仓库

- ## 拉取

  - **git pull：**拉取远程文件并与本地版本合并， 是`git fetch` 和 `git merge FETCH_HEAD` 的简写

    ```
    git pull <远程主机名> <远程分支名>:<本地分支名>
    ```

    
