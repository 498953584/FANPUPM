namespace cn.justwin.Web
{
    using ICSharpCode.SharpZipLib.Zip;
    using System;
    using System.IO;

    /// <summary>
    /// 对SharpZipLib.dll进行简单的包装
    /// </summary>
    public class SharpZipLibWrap
    {
        public void Zip(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException(path);
            }
            string fileName = FileHelper.GetFileName(path);
            string currentDirectory = Directory.GetCurrentDirectory();
            string str3 = fileName + ".zip";
            File.Copy(path, currentDirectory + "/" + fileName, true);
            FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            ZipOutputStream stream3 = new ZipOutputStream(File.Create(str3));
            ZipEntry entry = new ZipEntry(fileName);
            stream3.PutNextEntry(entry);
            stream3.SetLevel(8);
            byte[] buffer = new byte[0x400];
            int count = stream.Read(buffer, 0, buffer.Length);
            stream3.Write(buffer, 0, count);
            try
            {
                while (count < stream.Length)
                {
                    int num2 = stream.Read(buffer, 0, buffer.Length);
                    stream3.Write(buffer, 0, num2);
                    count += num2;
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            stream3.Finish();
            stream3.Close();
            stream.Close();
            File.Delete(fileName);
            File.Move(str3, path + ".zip");
        }
    }
}

