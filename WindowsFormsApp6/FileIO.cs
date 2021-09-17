using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp6
{
    class FileIO
    {
        // ファイルを一行ずつ読み取って
        public static string ReadFromFile(string path)
        {
            // 一行ずつ読み取るためのバッファ
            string line;

            // ファイルの内容を格納するStringBuilder
            StringBuilder stringBuilder = new StringBuilder();

            // File streamクラスでストリームリーダークラスをラッピングする
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {

                // ファイルの内容を流し込むストリームリーダークラスをインスタンス化
                using (StreamReader reader = new StreamReader(fs))
                {
                    // 新しい行がなくなるまで一行ずつ読み取っていく
                    while ((line = reader.ReadLine()) != null)
                    {
                        // 新しい行を一行ずつappendして改行
                        stringBuilder.Append(line + "\r\n");
                    }
                }
            }

            // stringBuilderを文字列に戻して返す
            return stringBuilder.ToString();

        }

        public static List<string> ReadLines(string path)
        {
            // 一行ずつ読み取るためのバッファ
            string line;

            // 戻り値としてリターンするリスト
            List<string> retList = new List<string>();

            // File streamクラスでストリームリーダークラスをラッピングする
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {

                // ファイルの内容を流し込むストリームリーダークラスをインスタンス化
                using (StreamReader reader = new StreamReader(fs))
                {
                    // 新しい行がなくなるまで一行ずつ読み取っていく
                    while ((line = reader.ReadLine()) != null)
                    {
                        // 新しい行を一行ずつappendして改行
                        retList.Add(line);
                    }
                }
            }

            // stringBuilderを文字列に戻して返す
            return retList;

        }
    }
}

