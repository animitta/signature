# Signature

English | [中文](README-zh-Hans.md)

## Background
In web application, we accept files uploaded by users,  
and many times need to know the type of files uploaded by users.  
Usually we can assume the type of file uploaded by the user by the file extension,  
but many times the file extension name is untrusted.  
Therefore, I developed this library,  
which is a general library that reads the file header to obtain the  
true type and MIME TYPE of the file(only depend `.NET Standard 2.0`).

## Install
* `Install-Package`
```powershell
PM> Install-Package ThinkerShare.Signature
```

* `dotnet.exe`
```bash
$ dotnet add package ThinkerShare.Signature --version 0.0.1-*
```

## Usage
The following is a simple usage example, please refer [Signature Documentation](https://thinkershare.com/project/signature) to the details

+ Obtaining the factual file format of a file
```CSharp
var sniffer = new Sniffer();
sniffer.Populate(Record.Common);
sniffer.Populate(Record.Unfrequent);

var head = new byte[] { 0xff, 0xd8, 0xff, 0xdb };
List<string> result = sniffer.Match(head);
//Result: ["jpg","jpeg"]
```

## Badge
[![MIT License](https://img.shields.io/badge/License-MIT-green)](https://github.com/thinkershare/owner-signature/blob/master/LICENSE)
[![Document](https://img.shields.io/badge/Document-Signature-orange)](https://thinkershare.com/project/signature)
[![Signature](https://img.shields.io/badge/NuGet-0.0.1--preview.0.0.1-blue)](https://www.nuget.org/packages/thinkershare.signature)

## Maintainers
[@rocketRobin](https://github.com/rocketRobin)  
[@thinkershare](https://github.com/thinkershare)

## Contributing
[Submit Issue](contributing.md)  
[Pull Request](contributing.md)  

** This project follows the [Contributor Covenant](http://contributor-covenant.org/version/1/3/0/) Code of Conduct**  

### Contributors
<img height="60" src="https://thinkershare.com/storage/project/signature/contributors.png" />

## License
[MIT](LICENSE) © thinkershare
