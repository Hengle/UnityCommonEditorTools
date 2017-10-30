using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using Kent.Boogaart.KBCsv;
using UnityEngine;
using Debug = UnityEngine.Debug;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
public class ColumnMappingAttribute : Attribute
{
    private static readonly Dictionary<Type, Action<ColumnMappingAttribute,HeaderRecord>> s_inits
         = new Dictionary<Type, Action<ColumnMappingAttribute, HeaderRecord>>();

    static ColumnMappingAttribute()
    {
        var methods = typeof (ColumnMappingAttribute).GetMethods(BindingFlags.Static | BindingFlags.NonPublic);

        foreach (var methodInfo in methods)
        {
            ParameterInfo[] parameters;
            if(methodInfo.Name.StartsWith("Init")
                && !methodInfo.Name.Equals("Init")
                && (parameters = methodInfo.GetParameters()).Length == 2)
            {
                var tname = methodInfo.Name.Substring(4, methodInfo.Name.Length - 4);
                var type = Type.GetType(tname, false);
                if (type == null)
                    type = Type.GetType("System." + tname, false);
                if (type == null)
                    type = Assembly.Load("UnityEngine").GetType("UnityEngine." + tname, false);

                if(type != null
                    && parameters[0].ParameterType == typeof(ColumnMappingAttribute)
                    && parameters[1].ParameterType == typeof(HeaderRecord))
                {
                    if(!s_inits.ContainsKey(type))
                    {
                        s_inits.Add(
                            type,
                            (Action<ColumnMappingAttribute, HeaderRecord>)
                            Delegate.CreateDelegate(typeof (Action<ColumnMappingAttribute, HeaderRecord>), methodInfo)
                            );
                    }
                }
            }
        }
    }

    private bool isinited;
    private string[] m_header;
    private Func<DataRecord, object> getObject;
    public FieldInfo TargetField;

    public object ImportParam;

    public ColumnMappingAttribute() : this(null)
    {
    }

    public ColumnMappingAttribute(params string[] header)
    {
        m_header = header;
        if(m_header == null)
            m_header = new string[0];
    }

    public void ParseTo(DataRecord record, object target)
    {
        var fvalue = getObject(record);
        this.TargetField.SetValue(target, fvalue);
    }

    public void Init(HeaderRecord headers)
    {
        if(this.isinited)
            throw new InvalidOperationException();

        Action<ColumnMappingAttribute, HeaderRecord> initHandler;
        if (s_inits.TryGetValue(this.TargetField.FieldType, out initHandler))
            initHandler(this, headers);
        else
            Debug.LogError("typeof:" + this.TargetField.DeclaringType + ", with field:" + this.TargetField.Name + " not support");

        this.isinited = true;
    }

    private static void InitString(ColumnMappingAttribute attribute, HeaderRecord record)
    {
        int index = SearchHeadIndex(attribute, record);

        if(index != -1)
                attribute.getObject = r => r[index];
        else
            attribute.getObject = r => string.Empty;
    }

    private static void InitBoolean(ColumnMappingAttribute attribute, HeaderRecord record)
    {
        var importP = attribute.ImportParam as string;
        if(importP != null)
        {
            int index = SearchHeadIndex(attribute, record);

            if (index != -1)
                attribute.getObject = r => string.Equals(r[index], importP, StringComparison.InvariantCultureIgnoreCase);
            else
                attribute.getObject = r => false;
        }else
            ConverToInternal<bool>(attribute, record);
    }

    private static void InitInt32(ColumnMappingAttribute attribute, HeaderRecord record)
    { ConverToInternal<int>(attribute, record); }

    private static void InitSingle(ColumnMappingAttribute attribute, HeaderRecord record)
    { ConverToInternal<float>(attribute, record); }

    private static void InitAudioClip(ColumnMappingAttribute attribute, HeaderRecord record)
    {
        LoadObjectFromResource(attribute,record);
    }

    private static void InitTexture2D(ColumnMappingAttribute attribute, HeaderRecord record)
    {
        LoadObjectFromResource(attribute,record);
    }
    private static void InitGameObject(ColumnMappingAttribute attribute, HeaderRecord record)
    {
        LoadObjectFromResource(attribute, record);
    }

    private static int SearchHeadIndex(ColumnMappingAttribute attribute, HeaderRecord record)
    {
        var searchs = new List<string>();
        if (attribute.m_header.Length == 0)
            searchs.Add(attribute.TargetField.Name);
        else
            searchs.AddRange(attribute.m_header);

        var index = -1;
        foreach (var search in searchs)
        {
            if ((index = record.IndexOf(search)) != -1)
                return index;
        }
        return index;
    }

    private static void ConverToInternal<T>(ColumnMappingAttribute attribute, HeaderRecord record)
    {
        int index = SearchHeadIndex(attribute, record);


        var type = typeof (T);
		if (index != -1)
		{
            attribute.getObject = r =>
			{
                                          try { 

					return Convert.ChangeType(r[index], type); }
                                          catch (Exception) {
					return GetDefaultValue<T>(attribute);
				}
                                      };
		}
        else
		{
			attribute.getObject = r => {  

				return GetDefaultValue<T>(attribute);
			}
			;
		}
    }

	static object GetDefaultValue<T>(ColumnMappingAttribute attribute)
	{
		//Type t = attribute.GetType();
		return default(T);
	}



    private static void LoadObjectFromResource(ColumnMappingAttribute attribute, HeaderRecord record)
    {
        int index = SearchHeadIndex(attribute, record);

        if(index != -1)
                attribute.getObject = r =>
                                          {
                                              var path = r[index];
                                              if(string.IsNullOrEmpty(path))
                                                  return null;

                                              var format = attribute.ImportParam as String;
                                              if(format != null)
                                                  path = string.Format(format, path);

                                              return Resources.Load(path);
                                          };
        else
            attribute.getObject = r => null;
    }
}