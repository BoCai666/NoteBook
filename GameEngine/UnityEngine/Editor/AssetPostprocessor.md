# [AssetPostprocessor](https://docs.unity3d.com/cn/2020.3/ScriptReference/AssetPostprocessor.html)

- ## 简介

  该类作为一种钩子（hook）在开发者与Unity编辑器的资源导入管线之间，使开发者能够在知道哪些资源被导入Unity，可在资源导入前后运行开发者编写的脚本。

- ## 使用

  该类位于UnityEditor命名空间下

  - **方法**：

    - OnPostprocessAllAssets：当任意资源导入完毕后Unity都会调用的一个函数接口

      ```c#
      示例：
      class AssetMapGenerator : AssetPostprocessor
      {
          static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
          {
              // 防止死循环
              if (importedAssets.Length == 1)
              {
                  var assetName = Path.GetFileName(importedAssets[0]);
                  if (assetName.Equals(AssetDef.FILE_ASSET_DATA)) return;
              }
              GenerateAssetMappingConfig();
          }
      ```

      

    - OnPostprocessAudio：当AudioClip导入完毕后调用

  

