using System;
using System.Collections.Generic;
using System.IO;
using Kent.Boogaart.KBCsv;
using UnityEngine;

public static class CsvImporter
{
    public static T[] Parser<T>(byte[] data )
    {
		if( data.Length == 0)
		{
			Debug.LogError("data length = 0");
			return new T[0];
		}
        var fhandles = GetFieldHandles(typeof (T));

        var reader = new StreamReader(new MemoryStream(data));
        using(var csReader = new CsvReader(reader))
        {
            var heads = csReader.ReadHeaderRecord();
                foreach (var handle in fhandles)
                    handle.Init(heads);

            var rets = new List<T>();
            DataRecord record;
            while ((record = csReader.ReadDataRecord()) != null)
            {
                var item = Activator.CreateInstance<T>();
				try{
                foreach (var handle in fhandles)
                    handle.ParseTo(record, item);
				}
				catch (Exception)
				{
				}
				rets.Add(item);
            }

            return rets.ToArray();
        }
    }

    private static ColumnMappingAttribute[] GetFieldHandles(Type type)
    {
        var fields = type.GetFields();

        var rets = new List<ColumnMappingAttribute>();

        foreach (var fieldInfo in fields)
        {
            if (Attribute.IsDefined(fieldInfo, typeof(ColumnMappingAttribute)))
            {
                var handle = (ColumnMappingAttribute)Attribute.GetCustomAttribute(fieldInfo, typeof (ColumnMappingAttribute));
                handle.TargetField = fieldInfo;
                rets.Add(handle);
            }
        }

        return rets.ToArray();

    }

    /// <summary>
    /// ÆÁ±Î×ÖµÄ¶ÁÈ¡
    /// </summary>
    public static string[] ParserShieldStrings(byte[] data)
    {
        var reader = new StreamReader(new MemoryStream(data));
        using(var csReader = new CsvReader(reader))
        {
            List<string> listWord = new List<string>();
           foreach (var str in csReader.ShiedRecord())
           {
               if (!listWord.Contains(str))
                   listWord.Add(str);
           }
            return listWord.ToArray();
        }
    }

}