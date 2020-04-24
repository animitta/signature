using System.Collections.Generic;

namespace ThinkerShare.Signature
{
    /// <summary>
    /// 文件头记录
    /// </summary>
    public class Record
    {
        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="extensions">文件扩展名列表</param>
        /// <param name="hex">十六进制字符串</param>
        /// <param name="offsetSize">偏移</param>
        /// <param name="description">描述</param>
        protected Record(string extensions, string hex, int offsetSize, string description)
        {
            Hex = hex;
            OffsetSize = offsetSize;
            Extensions = extensions;
            Description = description;
        }

        /// <summary>
        /// 常用的文件类型
        /// </summary>
        public static List<Record> FrequentRecords => new List<Record>
        {
            Create("asf wma wmv", "30 26 B2 75 8E 66 CF 11 A6 D9 00 AA 00 62 CE 6C"),
            Create("ogg oga ogv", "4F 67 67 53"),
            Create("psd", "38 42 50 53"),
            Create("mp3", "FF FB"),
            Create("mp3", "49 44 33"),
            Create("bmp dib", "42 4D"),
            Create("jpg,jpeg", "ff,d8,ff,db"),
            Create("png", "89,50,4e,47,0d,0a,1a,0a"),
            Create("zip,jar,odt,ods,odp,docx,xlsx,pptx,vsdx,apk,aar", "50,4b,03,04"),
            Create("zip,jar,odt,ods,odp,docx,xlsx,pptx,vsdx,apk,aar", "50,4b,07,08"),
            Create("zip,jar,odt,ods,odp,docx,xlsx,pptx,vsdx,apk,aar", "50,4b,05,06"),
            Create("rar", "52,61,72,21,1a,07,00"),
            Create("rar", "52,61,72,21,1a,07,01,00"),
            Create("class", "CA FE BA BE"),
            Create("pdf", "25 50 44 46"),
            Create("rpm", "ed ab ee db"),
            Create("flac", "66 4C 61 43"),
            Create("mid midi", "4D 54 68 64"),
            Create("ico", "00 00 01 00"),
            Create("z,tar.z", "1F 9D"),
            Create("z,tar.z", "1F A0"),
            Create("gif", "47 49 46 38 37 61"),
            Create("dmg", "78 01 73 0D 62 62 60"),
            Create("gif", "47 49 46 38 39 61"),
            Create("exe", "4D 5A"),
            Create("tar", "75 73 74 61 72", 257),
            Create("mkv mka mks mk3d webm", "1A 45 DF A3"),
            Create("gz tar.gz", "1F 8B"),
            Create("xz tar.xz", "FD 37 7A 58 5A 00 00"),
            Create("7z", "37 7A BC AF 27 1C"),
            Create("mpg mpeg", "00 00 01 BA"),
            Create("mpg mpeg", "00 00 01 B3"),
            Create("woff", "77 4F 46 46"),
            Create("woff2", "77 4F 46 32"),
            Create("XML", "3c 3f 78 6d 6c 20"),
            Create("swf", "43 57 53"),
            Create("swf", "46 57 53"),
            Create("deb", "21 3C 61 72 63 68 3E"),
            Create("jpg,jpeg", "FF D8 FF E0 ?? ?? 4A 46 49 46 00 01"),
            Create("jpg,jpeg", "FF D8 FF E1 ?? ?? 45 78 69 66 00 00"),
        };

        /// <summary>
        /// 不常用的文件类型
        /// </summary>
        public static List<Record> UnfrequentRecords => new List<Record>
        {
            Create("bin", "53 50 30 31"),
            Create("bac", "42 41 43 4B 4D 49 4B 45 44 49 53 4B"),
            Create("bz2", "42 5A 68"),
            Create("tif tiff", "49 49 2A 00"),
            Create("tif tiff", "4D 4D 00 2A"),
            Create("cr2", "49 49 2A 00 10 00 00 00 43 52"),
            Create("cin", "80 2A 5F D7"),
            Create("exr", "76 2F 31 01"),
            Create("dpx", "53 44 50 58"),
            Create("dpx", "58 50 44 53"),
            Create("bpg", "42 50 47 FB"),
            Create("lz", "4C 5A 49 50"),
            Create("ps", "25 21 50 53"),
            Create("fits", "3D 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 54"),
            Create("doc xls ppt msg", "D0 CF 11 E0 A1 B1 1A E1"),
            Create("dex", "64 65 78 0A 30 33 35 00"),
            Create("vmdk", "4B 44 4D"),
            Create("crx", "43 72 32 34"),
            Create("cwk", "05 07 00 00 42 4F 42 4F 05 07 00 00 00 00 00 00 00 00 00 00 00 01"),
            Create("fh8", "41 47 44 33"),
            Create("cwk", "06 07 E1 00 42 4F 42 4F 06 07 E1 00 00 00 00 00 00 00 00 00 00 01"),
            Create("toast", "45 52 02 00 00 00"),
            Create("toast", "8B 45 52 02 00 00 00"),
            Create("xar", "78 61 72 21"),
            Create("dat", "50 4D 4F 43 43 4D 4F 43"),
            Create("nes", "4E 45 53 1A"),
            Create("tox", "74 6F 78 33"),
            Create("MLV", "4D 4C 56 49"),
            Create("lz4", "04 22 4D 18"),
            Create("cab", "4D 53 43 46"),
            Create("flif", "46 4C 49 46"),
            Create("stg", "4D 49 4C 20"),
            Create("der", "30 82"),
            Create("wasm", "00 61 73 6d"),
            Create("lep", "cf 84 01"),
            Create("rtf", "7B 5C 72 74 66 31"),
            Create("m2p vob", "00 00 01 BA"),
            Create("zlib", "78 01"),
            Create("zlib", "78 9c"),
            Create("zlib", "78 da"),
            Create("lzfse", "62 76 78 32"),
            Create("orc", "4F 52 43"),
            Create("avro", "4F 62 6A 01"),
            Create("rc", "53 45 51 36"),
            Create("tbi", "00 00 00 00 14 00 00 00"),
            Create("dat", "00 00 00 00 62 31 05 00 09 00 00 00 00 20 00 00 00 09 00 00 00 00 00 00", 8, "Bitcoin Core wallet.dat file"),
            Create("jp2", "00 00 00 0C 6A 50 20 20 0D 0A", "Various JPEG-2000 image file formats"),
            Create("ttf", "00 01 00 00 00"),
            Create("mdf", "00 FF FF FF FF FF FF FF FF FF FF 00 00 02 00 01"),
            Create("pdb", "00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00", 11),
            Create("3gp 3g2", "66 74 79 70 33 67", 4),
            Create("iso", "43 44 30 30 31", 32769),
            Create("iso", "43 44 30 30 31", 34817),
            Create("iso", "43 44 30 30 31", 36865),
        };

        /// <summary>
        /// 十六进制字符串
        /// </summary>
        public string Hex { get; set; }

        /// <summary>
        /// 偏移(需要补齐的任意前缀字节数)
        /// </summary>
        public int OffsetSize { get; set; }

        /// <summary>
        /// 文件扩展名列表
        /// </summary>
        public string Extensions { get; set; }

        /// <summary>
        /// 文件类型记录描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 是否是复合映射记录
        /// </summary>
        public bool IsComplex
        {
            get => OffsetSize > 0 || Hex.Contains("?");
        }

        /// <summary>
        /// 创建Record记录
        /// </summary>
        /// <param name="extensions">文件扩展名列表</param>
        /// <param name="hex">十六进制字符串</param>
        /// <returns>Record记录</returns>
        public static Record Create(string extensions, string hex)
        {
            return Create(extensions, hex, 0, null);
        }

        /// <summary>
        /// 创建Record记录
        /// </summary>
        /// <param name="extensions">文件扩展名列表</param>
        /// <param name="hex">十六进制字符串</param>
        /// <param name="offsetSize">文件头偏移</param>
        /// <returns>Record记录</returns>
        public static Record Create(string extensions, string hex, int offsetSize)
        {
            return Create(extensions, hex, offsetSize, null);
        }

        /// <summary>
        /// 创建Record记录
        /// </summary>
        /// <param name="extensions">文件扩展名列表</param>
        /// <param name="hex">十六进制字符串</param>
        /// <param name="description">描述</param>
        /// <returns>Record记录</returns>
        public static Record Create(string extensions, string hex, string description)
        {
            return Create(extensions, hex, 0, description);
        }

        /// <summary>
        /// 创建Record记录
        /// </summary>
        /// <param name="extensions">文件扩展名列表</param>
        /// <param name="hex">十六进制字符串</param>
        /// <param name="offsetSize">文件头偏移</param>
        /// <param name="description">描述</param>
        /// <returns>Record记录</returns>
        public static Record Create(string extensions, string hex, int offsetSize, string description)
        {
            return offsetSize > 0 || hex.Contains("?")
                ? new ComplexRecord(extensions, hex, offsetSize, description)
                : new Record(extensions, hex, offsetSize, description);
        }
    }
}
