# Signature

中文 | [English](README.md)
## 背景
在Web应用开发中,我们接受用户上传的文件,很多时候需要知道用户上传文件的类型,  
通常我们可以通过文件的扩展名来假定用户上传的文件类型,但很多时候用户的文件扩展名称是不可信任的,  
因此我开发了这个库,这是一个通过读取文件头从而获取文件真实类型和`MIME TYPE`的通用库(本库只依赖于`.NET Standard 2.1`).

## 安装
如果使用Visual Studio开发项目,你可以使用NuGet命令`Install-Package`安装项目,  
如果使用其它工具开发项目, 你可以选择.NET Core的CLI工具`dotnet.exe`安装项目.

* `Install-Package`安装
```powershell
PM> Install-Package Thinkershare.Signature
```
* `dotnet.exe`安装
```bash
$ dotnet add package Thinkershare.Signature
```

## 使用
下面是简单的使用示例,详情请参考[Signature文档](https://thinkershare.com/project/signature)

+ 获取一个文件扩展名字符串对应的MIME TYPE
```CSharp
var extension = ".jpg";
var result = extension.GetMimeType();
//结果: "image/jpeg"
```

+ 获取一个文件的真实格式
```CSharp
var signature = new Signature();
signature.AddRecords(Record.FrequentRecords);
signature.AddRecords(Record.UnfrequentRecords);

var head = new byte[] { 0xff, 0xd8, 0xff, 0xdb };
var result = signature.Match(head);
//结果: ["jpg","jpeg"]
```

+ 获取一个文件的真实格式(快捷模式)
```CSharp
using Thinkershare.Signature.Extensions;

var head = new byte[] { 0xff, 0xd8, 0xff, 0xdb };
var result = head.GetExtension(head);
//结果: "jpg"

var result = head.GetExtensions(head);
//结果: ["jpg","jpeg"]
```

## 徽标
[![MIT License](https://img.shields.io/badge/license-MIT-green)](https://github.com/thinkershare/signature/blob/main/LICENSE)
[![Document](https://img.shields.io/badge/documentation-signature-orange)](https://thinkershare.com/project/signature)
[![Signature](https://img.shields.io/badge/nuget-1.0.1-blue)](https://www.nuget.org/packages/thinkershare.signature)

## 维护人员
[@rocketrobin](https://github.com/rocketrobin)  
[@thinkershare](https://github.com/thinkershare)

## 如何贡献
[提一个Issue](contributing.md)  
[Pull Request](contributing.md)  

**项目遵守: [Contributor Covenant](http://contributor-covenant.org/version/1/3/0/)**  

### 贡献者
<img height="60" src="https://thinkershare.com/storage/project/signature/contributors.png" />

## 使用许可
[MIT](LICENSE) © thinkershare
