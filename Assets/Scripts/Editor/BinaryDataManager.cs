using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEngine;

public interface ISingleton
{
    void Init();
}

public abstract class Singleton<T> where T : ISingleton, new()
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Lazy<T>(true).Value;
                instance.Init();
            }
            return instance;
        }
    }
}

public class BinaryDataManager:ISingleton
{
    //The path that saves all the binary data
    public static string DATA_BINARY_PATH = Application.streamingAssetsPath + "/Binary/";

    //Save all the table data
    private Dictionary<string, object> tableDic = new Dictionary<string, object>();


    private static string SAVE_PATH = Application.persistentDataPath + "/Data/";

    public static BinaryDataManager Instance => Singleton<BinaryDataManager>.Instance;

    public void Init()
    {
        throw new NotImplementedException();
    }

    public void LoadTable<T, K>()
    {
        using (FileStream fs = File.Open(DATA_BINARY_PATH + typeof(K).Name + ".tang", FileMode.Open, FileAccess.Read))
        {
            byte[] bytes = new byte[fs.Length];
            fs.Read(bytes, 0, bytes.Length);
            fs.Close();

            int index = 0;

            int count = BitConverter.ToInt32(bytes, index);
            index += 4;

            int keyNameLength = BitConverter.ToInt32(bytes, index);
            index += 4;
            string keyName = Encoding.UTF8.GetString(bytes, index, keyNameLength);
            index += keyNameLength;

            Type contaninerType = typeof(T);
            object contaninerObj = Activator.CreateInstance(contaninerType);
            
            Type classType = typeof(K);
            //What is reflection?
            FieldInfo[] infos = classType.GetFields();

            for (int i = 0; i < count; i++)
            {
                object dataObj = Activator.CreateInstance(classType);
                foreach (FieldInfo info in infos)
                {
                    if (info.FieldType == typeof(int))
                    {
                        info.SetValue(dataObj, BitConverter.ToInt32(bytes, index));
                        index += 4;
                    }
                    else if (info.FieldType == typeof(float))
                    {
                        info.SetValue(dataObj, BitConverter.ToSingle(bytes, index));
                        index += 4;
                    }
                    else if (info.FieldType == typeof(bool))
                    {
                        info.SetValue(dataObj, BitConverter.ToBoolean(bytes, index));
                        index += 1;
                    }
                    else if (info.FieldType == typeof(string))
                    {
                        int length = BitConverter.ToInt32(bytes, index);
                        index += 4;
                        info.SetValue(dataObj, Encoding.UTF8.GetString(bytes, index, length));
                        index += length;
                    }
                }

                object dicObject = contaninerType.GetField("dataDic").GetValue(contaninerObj);

                MethodInfo mInfo = dicObject.GetType().GetMethod("Add");

                object keyValue = classType.GetField(keyName).GetValue(dataObj);
                mInfo.Invoke(dicObject, new object[] { keyValue, dataObj });
            }

            tableDic.Add(typeof(T).Name, contaninerObj);
            fs.Close();
        }
    }
}
