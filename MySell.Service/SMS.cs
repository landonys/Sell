using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace MySell.Service
{
    public class SMS
    {
        public static string PostData(string purl, string str)
        {
            try
            {
                byte[] data = System.Text.Encoding.GetEncoding("GB2312").GetBytes(str);
                // 准备请求    
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(purl);
                //设置超时     
                req.Timeout = 30000;
                req.Method = "Post";
                req.ContentType = "application/x-www-form-urlencoded";
                req.ContentLength = data.Length;
                Stream stream = req.GetRequestStream();
                // 发送数据   
                stream.Write(data, 0, data.Length);
                stream.Close();

                HttpWebResponse rep = (HttpWebResponse)req.GetResponse();
                Stream receiveStream = rep.GetResponseStream();
                Encoding encode = System.Text.Encoding.GetEncoding("UTF-8");
                // Pipes the stream to a higher level stream reader with the required encoding format.   
                StreamReader readStream = new StreamReader(receiveStream, encode);

                Char[] read = new Char[256];
                int count = readStream.Read(read, 0, 256);
                StringBuilder sb = new StringBuilder("");
                while (count > 0)
                {
                    String readstr = new String(read, 0, count);
                    sb.Append(readstr);
                    count = readStream.Read(read, 0, 256);
                }

                rep.Close();
                readStream.Close();

                return sb.ToString();
            }
            catch (Exception ex)
            {
                return "posterror";
            }
        }
    }
}
