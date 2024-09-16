using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
public class DebugFile : Singleton<DebugFile>
{
    //ログを表示するパス
    const string m_path = "DebugText.txt";
    //書き込みストリーム
    StreamWriter m_writer = null;
    //上書きフラグ
    const bool isOverWrite = true;
    //生成したパス
    string path = null;

    protected override void Awake()
    {
        //パス取得
        path =Path.Combine(Application.dataPath, m_path);

        try
        {
            using (m_writer = new StreamWriter(path, isOverWrite))
            {
                Debug.Log("開いた");
            }
        }
        catch
        {
            Debug.Log("開けてない");
            m_writer = null;
        }
    }

    public void WriteLog(string _text)
    {
        if (m_writer == null) return;
        else
        {
            using (m_writer = new StreamWriter(path, isOverWrite))
            {
                m_writer.WriteLine(_text);
                m_writer.Flush();
                Debug.Log("書き込み");
            }
        }
    }

    //public void OnDestroy()
    //{
    //    if (m_writer == null) return;
    //    else
    //    {
    //        m_writer.Close();
    //    }
    //}

}
