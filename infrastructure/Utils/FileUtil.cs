using domain.Enums;
using Microsoft.AspNetCore.Http;

namespace infrastructure.Utils;

/// <summary>
/// 文件工具类
/// </summary>
public class FileUtil
{
    static string[] imgs = { ".jpg", ".jpeg", ".png", ".xbm", ".tif", ".pjp", ".svgz", ".jpg",
        ".jpeg", ".ico", ".tiff", ".gif", ".svg", ".jfif", ".webp", ".png", ".bmp", ".pjpeg", ".avif" };
    static string[] txts = { ".txt", ".text",  };

    
    public static FileTypeEnum GetIFormFileType(IFormFile file)
    {    
        //获得到文件名
        string fileName = Path.GetFileName(file.FileName);
        //获得文件扩展名
        string fileNameEx = Path.GetExtension(fileName);
        //没有扩展名的文件名
        string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
            
        foreach (var item in imgs)
        {
            if (fileNameEx.Equals(item))
            {
                return FileTypeEnum.Image;
            }
        }
        foreach (var txt in txts)
        {
            if (fileNameEx.Equals(txt))
            {
                return FileTypeEnum.Txt;
            }
        }
        return FileTypeEnum.Other;
    }

    
}