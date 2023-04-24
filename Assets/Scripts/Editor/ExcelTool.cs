using ExcelDataReader;
using System;
using System.Data;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

public class ExcelTool
{
    public static string EXCEL_PATH = Application.dataPath + "/Excel/";

    public static string DATA_CLASS_PATH = Application.dataPath + "/Scripts/ExcelData/DataClass/";

    public static string DATA_CONTAINER_PATH = Application.dataPath + "Scripts/ExcelData/Container/";

    [MenuItem("GameTool/GenerateExcel")]
    private static void GenerateExcelInfo()
    {
        //Remember All Excel files
        DirectoryInfo dInfo = Directory.CreateDirectory(EXCEL_PATH);
        //Get all Excel files
        FileInfo[] files = dInfo.GetFiles();
        //Container
        DataTableCollection tableCollection;
        for(int i = 0;i < files.Length; i++)
        {
            if(files[i].Extension != ".xlsx" && files[i].Extension != ".xls")
            {
                continue;
            }
            using (FileStream fs = files[i].Open(FileMode.Open, FileAccess.Read))
            {
                IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(fs);
                tableCollection = excelReader.AsDataSet().Tables;
                fs.Close();
            }

            foreach(DataTable table in tableCollection)
            {
                GenerateExcelDataClass(table);
                GenerateExcelContainer(table);
                GenerateExcelBinary(table);
            }
        }
    }



    #region Generate Data Class
    private static void GenerateExcelDataClass(DataTable table)
    {
        //Load default data row
        DataRow rowName = GetVariableNameRow(table);
        DataRow rowType = GetVariableTypeRow(table);

        if (!Directory.Exists(DATA_CLASS_PATH))
        {
            Directory.CreateDirectory(DATA_CLASS_PATH);
        }
        string str = "public class " + table.TableName + "\n{\n";

        //Write the information of variables into the file
        for(int i = 0; i < table.Columns.Count; i++)
        {
            str += "    public " + rowType[i].ToString() + " " + rowName[i].ToString() + ";\n";
        }

        str += "}";

        //Load the test into the file
        File.WriteAllText(DATA_CLASS_PATH + table.TableName + ".cs", str);

        //Refresh Project Window
        AssetDatabase.Refresh();
    }

    private static DataRow GetVariableNameRow(DataTable table)
    {
        return table.Rows[0];
    }

    private static DataRow GetVariableTypeRow(DataTable table)
    {
        return table.Rows[1];
    }

    #endregion

    #region ExcelContainer

    private static void GenerateExcelContainer(DataTable table)
    {
        int keyIndex = GetKeyIndex(table);
        DataRow rowType = GetVariableTypeRow(table);
        if (!Directory.Exists(DATA_CONTAINER_PATH))
        {
            Directory.CreateDirectory(DATA_CONTAINER_PATH);
        }
        string str = "using System.Collections.Generic;\n";
        str += "public class " + table.TableName + "Container" + "\n{\n";
        str += "    ";
        str += "public Dictionary<" + rowType[keyIndex].ToString() + "," + table.TableName + ">";
        str += "dataDic = new " + "Dictionary<" + rowType[keyIndex].ToString() + ", " + table.TableName + ">();\n";
        str += "}";
        File.WriteAllText(DATA_CONTAINER_PATH + "/" + table.TableName + "Container.cs", str);
        AssetDatabase.Refresh();
    }

    private static int GetKeyIndex(DataTable table)
    {
        DataRow row = table.Rows[2];
        for(int i = 0; i < table.Columns.Count; i++)
        {
            if (row[i].ToString() == "key")
            {
                return i;
            }
        }
        return 0;
    }

    #endregion



    private static void GenerateExcelBinary(DataTable table)
    {
        if (!Directory.Exists(DATA_CONTAINER_PATH))
        {
            Directory.CreateDirectory(DATA_CONTAINER_PATH);
        }
        using (FileStream fs = new FileStream(DATA_CONTAINER_PATH + table.TableName + ".mqx", FileMode.OpenOrCreate, FileAccess.Write))
        {
            fs.Write(BitConverter.GetBytes(table.Rows.Count - 3), 0, 4);
            string keyName = GetVariableNameRow(table)[GetKeyIndex(table)].ToString();
            byte[] bytes = Encoding.UTF8.GetBytes(keyName);
            fs.Write(BitConverter.GetBytes(bytes.Length), 0, 4);
            fs.Write(bytes, 0, bytes.Length);
            DataRow row;
            DataRow rowType = GetVariableTypeRow(table);
            for(int i = 0; i < table.Rows.Count; i++)
            {
                row = table.Rows[i];
                for(int j = 0; j < table.Columns.Count; j++)
                {
                    switch (rowType[j].ToString())
                    {
                        case "int":
                            fs.Write(BitConverter.GetBytes(int.Parse(row[j].ToString())), 0, 4);
                            break;
                        case "float":
                            fs.Write(BitConverter.GetBytes(int.Parse(row[j].ToString())), 0, 4);
                            break;
                        case "string":
                            fs.Write(BitConverter.GetBytes(int.Parse(row[j].ToString())), 0, 1);
                            break;
                        case "bool":
                            bytes = Encoding.UTF8.GetBytes(row[j].ToString());
                            fs.Write(BitConverter.GetBytes(int.Parse(row[j].ToString())), 0, 4);
                            fs.Write(bytes, 0, bytes.Length);
                            break;
                    }
                }
            }
            fs.Close();
        }
        AssetDatabase.Refresh();
    }

}
