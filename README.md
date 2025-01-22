# GBK to UTF-8

## ⚠️续作⚠️

***由于[原项目](https://github.com/Roger-WIN/GBKtoUTF-8.git)已经多年未更新，因此fork该库优化功能体验***


- [ ] 💪 允许设置文件筛选器
- [ ] 💪 增加自动跳过目标编码
- [ ] 💪 允许设置是否将未修改的文件复制到目标位置
- [ ] 🤔 更好的UI



这是一个将文本文件由 GBK 转码为 UTF-8 的小工具。


![image](./image1.png)

可以选择多个文件，或者选择一个文件夹（支持在子文件夹中查找），但二者不可同时选择。

## 下载

https://github.com/ILoveScratch2/GBKtoUTF-8-Revivied/releases

提供两种可执行文件：

- `GBK.to.UTF-8_with-runtime.exe`：已内置框架（.NET 桌面运行时），无需另外安装，可直接运行；
- `GBK.to.UTF-8.exe`：未内置框架，需自行安装，方可运行。

## 系统要求

- | 操作系统版本 |                           安装要求                           |
  | :----------: | :----------------------------------------------------------: |
  |  Windows 11  |                           所有版本                           |
  |  Windows 10  |                       1607 或更高版本                        |
  | Windows 8.1  | [VC++ 2015~2019 运行库](https://aka.ms/vs/16/release/vc_redist.x64.exe) |
  |  Windows 7   | [VC++ 2015~2019 运行库](https://aka.ms/vs/16/release/vc_redist.x64.exe) |

- [.NET 6 桌面运行时](https://dotnet.microsoft.com/zh-cn/download/dotnet/thank-you/runtime-desktop-6.0.9-windows-x64-installer)

## 开发

- 开发先决条件
  - [Microsoft Windows 10 19045+](https://www.microsoft.com/zh-cn/software-download)
    - Windows 10 19045 及更高版本
  - [Microsoft Visual Studio 2022+](https://visualstudio.microsoft.com/zh-hans/)
     - 需要包含.NET SDK
  - [.Net SDK](https://dotnet.microsoft.com/zh-cn/download/dotnet/thank-you/sdk-6.0.401-windows-x64-installer)
  - [Git](https://git-scm.com/)

- 发布

  ## 概述
  由于 `.csproj` 项目文件中已包含

  ```
  <PublishSingleFile>true</PublishSingleFile>
  <IncludeAllContentForSelfExtract>true</IncludeAllContentForSelfExtract>
  ```



  因此默认发布为单个可执行文件，文档见[单文件部署和可执行文件](https://docs.microsoft.com/zh-cn/dotnet/core/deploying/single-file)。

  ## 命令行
  文档见[使用 CLI 发布应用](https://docs.microsoft.com/zh-cn/dotnet/core/deploying/deploy-with-cli)。

  - 依赖框架（不内置框架）：

    ```dotnet
    dotnet publish -c Release -r win-x64 --self-contained false
    ```

    

  - 独立（内置框架）：

    ```dotnet
    dotnet publish -c Release -r win-x64 --self-contained true -p:EnableCompressionInSingleFile=true
    ```

    

    > 从 .NET 6 开始，在嵌入式程序集上启用压缩后，可以创建单文件应用。将 `EnableCompressionInSingleFile` 属性设置为 `true` 即可实现此目的。生成的单个文件将包含所有已压缩的嵌入式程序集，这可以显著减小可执行文件的大小。

  另外，可以通过追加选项

  ```dotnet
  -p:Version=X.Y.Z.W
  ```



  来指定可执行文件的版本号为 `X.Y.Z.W`。

    ## Visual Studio
  文档见[使用 Visual Studio 部署应用](https://docs.microsoft.com/zh-cn/dotnet/core/deploying/deploy-with-vs?tabs=vs157)。

  1. 右键单击项目，选择「发布」；

    ![image-20211227235138902](https://camo.githubusercontent.com/9e5c8e99e086ef74f0fa3e9cf1927a5b244c61941043056e330b4af4a5742822/68747470733a2f2f696d672e726f6765726b756e672d77696e2e746f702f756e646566696e6564696d6167652d32303231313232373233353133383930322e706e67)

  2. 在弹出的对话框中，两次选择「文件夹」；

    ![image-20211227235228638](https://camo.githubusercontent.com/11fbc555249036feca81049b09408b1893c2f16ad170a0ab2d71384dbd9c54d0/68747470733a2f2f696d672e726f6765726b756e672d77696e2e746f702f756e646566696e6564696d6167652d32303231313232373233353232383633382e706e67)

    ![image-20211227235339859](https://camo.githubusercontent.com/8418abc005268f96df22e86f6af8c0e7449d2514738fa2a084ef9850378134fe/68747470733a2f2f696d672e726f6765726b756e672d77696e2e746f702f756e646566696e6564696d6167652d32303231313232373233353333393835392e706e67)

  3. 然后指定发布的位置；

    ![image-20211227235409609](https://camo.githubusercontent.com/e846c1f976579a2d659b1a70b955df223858f62313234020e97ac4f6ac07ec98/68747470733a2f2f696d672e726f6765726b756e672d77696e2e746f702f756e646566696e6564696d6167652d32303231313232373233353430393630392e706e67)

  4. 建立初始的发布配置文件后，点击「编辑」；

    ![image-20211227235559609](https://camo.githubusercontent.com/1ee5db14da34e5e47a64b005200095db13e0d77c726a9fd5054018eda0a9819a/68747470733a2f2f696d672e726f6765726b756e672d77696e2e746f702f756e646566696e6564696d6167652d32303231313232373233353535393630392e706e67)

    - 依赖框架：

      ![image-20211227235902824](https://camo.githubusercontent.com/9bb62bf8c51c9c635221acdcc63db45d44564b75944e2fc9c448388dbd772709/68747470733a2f2f696d672e726f6765726b756e672d77696e2e746f702f756e646566696e6564696d6167652d32303231313232373233353930323832342e706e67)

    - 独立：

      ![image-20211227235944907](https://camo.githubusercontent.com/4eb71f43794b4b2eeebb60d4fb83cf4f96e53e70401c5b6e4cb5ff573028dee0/68747470733a2f2f696d672e726f6765726b756e672d77696e2e746f702f756e646566696e6564696d6167652d32303231313232373233353934343930372e706e67)

  5. 最后点击「发布」便可发布应用。

    ![image-20211228000244152](https://camo.githubusercontent.com/9c684dd8ff062f727e6bf42e9ced695d4debb20ffc3b8cbf74f3ad1b9a457d2c/68747470733a2f2f696d672e726f6765726b756e672d77696e2e746f702f756e646566696e6564696d6167652d32303231313232383030303234343135322e706e67)

  ------

  创建得到的发布配置文件可能像这样：

  ```
  <?xml version="1.0" encoding="utf-8"?>
  <!--
  https://go.microsoft.com/fwlink/?LinkID=208121.
  -->
  <Project ToolsVersion="4.0" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
    <PropertyGroup>
      <Configuration>Release</Configuration>
      <Platform>Any CPU</Platform>
      <PublishDir>bin\Release\net6.0-windows10.0.22000.0\publish\x64\with-runtime</PublishDir>
      <PublishProtocol>FileSystem</PublishProtocol>
      <TargetFramework>net6.0-windows10.0.22000.0</TargetFramework>
      <RuntimeIdentifier>win-x64</RuntimeIdentifier>
      <SelfContained>true</SelfContained>
    </PropertyGroup>
  </Project>
  ```



  提示：可向发布配置文件中 `<PropertyGroup>` 内增加 `<EnableCompressionInSingleFile>true</EnableCompressionInSingleFile>` 来压缩文件的体积。但此属性仅在 `<SelfContained>` 属性为 `true` 时可用。

  ------

  你甚至可以在命令行中调用已创建好的发布配置文件：

  ```dotnet
  dotnet publish -p:PublishProfile=FolderProfile
  ```



  好处是方便添加一些不便写在发布配置文件中的属性。如版本号，方式为追加如下选项：

  ```dotnet
  -p:Version=X.Y.Z.W
  ```



  记得将 `X`、`Y`、`Z`、`W` 替换为实际的数字。

