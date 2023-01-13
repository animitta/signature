# Signature

English | [中文](README-zh-Hans.md)

## Background
In web application, we accept files uploaded by users,  
and many times need to know the type of files uploaded by users.  
Usually we can assume the type of file uploaded by the user by the file extension,  
but many times the file extension name is untrusted.  
Therefore, I developed this library,  
which is a general library that reads the file header to obtain the  
true type and MIME TYPE of the file(only depend `.NET Standard 2.1`).

## References

* [MIME](https://www.iana.org/assignments/media-types/media-types.xhtml)
* [Garykessler File_sigs](https://www.garykessler.net/library/file_sigs.html)
* [List of file signatures](https://en.wikipedia.org/wiki/List_of_file_signatures)

## Install
* `Install-Package`
```powershell
PM> Install-Package Thinkershare.Signature
```

* `dotnet.exe`
```bash
$ dotnet add package Thinkershare.Signature
```

## Usage
The following is a simple usage example, please refer [Signature Documentation](https://thinkershare.com/project/signature) to the details

+ Obtaining MIME TYPE for file extension string
```CSharp
var extension = ".jpg";
var result = extension.GetMimeType();
//Result: "image/jpeg"
```

+ Obtaining the factual file format of a file
```CSharp
var signature = new Signature();
signature.AddRecords(Record.FrequentRecords);
signature.AddRecords(Record.UnfrequentRecords);

var head = new byte[] { 0xff, 0xd8, 0xff, 0xdb };
var result = signature.Match(head);
//Result: ["jpg","jpeg"]
```

+ Obtaining the factual file format of a file(express mode)
```CSharp
using Thinkershare.Signature.Extensions;

var head = new byte[] { 0xff, 0xd8, 0xff, 0xdb };
var result = head.GetExtension(head);
//Result: "jpg"

var result = head.GetExtensions(head);
//Result: ["jpg","jpeg"]
```

## Badge
[![MIT License](https://img.shields.io/badge/license-MIT-green)](https://github.com/thinkershare/signature/blob/main/LICENSE)
[![Document](https://img.shields.io/badge/documentation-signature-orange)](https://thinkershare.com/project/signature)
[![Signature](https://img.shields.io/badge/nuget-1.0.1-blue)](https://www.nuget.org/packages/thinkershare.signature)

## Maintainers
[@rocketrobin](https://github.com/rocketrobin)  
[@thinkershare](https://github.com/thinkershare)

## Contributing
[Submit Issue](contributing.md)  
[Pull Request](contributing.md)  

**This project follows the [Contributor Covenant](http://contributor-covenant.org/version/1/3/0/) Code of Conduct**  

### Contributors
<img height="60" src="https://thinkershare.com/storage/project/signature/contributors.png" />

## License
[MIT](LICENSE) © thinkershare
