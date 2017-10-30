using UnityEngine;
using System.Collections;

public class ExportResourceInfo
{
    /// <summary>
    /// 序号
    /// </summary>
    [ColumnMappingAttribute("Index")]
    public int index;
    /// <summary>
    /// 相对于Asset.DataPath 资源目录
    /// </summary>
    [ColumnMappingAttribute("resourcePath")]
    public string resourcePath;
    /// <summary>
    /// 后缀名1
    /// </summary>
    [ColumnMappingAttribute("extensions1")]
    public string extensions1;
    /// <summary>
    /// 后缀名2
    /// </summary>
    [ColumnMappingAttribute("extensions2")]
    public string extensions2;
    /// <summary>
    /// 后缀名3
    /// </summary>
    [ColumnMappingAttribute("extensions3")]
    public string extensions3;
    /// <summary>
    /// 后缀名4
    /// </summary>
    [ColumnMappingAttribute("extensions4")]
    public string extensions4;
    /// <summary>
    /// 后缀名5
    /// </summary>
    [ColumnMappingAttribute("extensions5")]
    public string extensions5;
    /// <summary>
    /// 导出的父文件夹名称
    /// </summary>
    [ColumnMappingAttribute("exportrootpath")]
    public string exportrootpath;
    /// <summary>
    /// 过滤后缀名1
    /// </summary>
    [ColumnMappingAttribute("filter1")]
    public string filter1;
    /// <summary>
    /// 过滤后缀名2
    /// </summary>
    [ColumnMappingAttribute("filter2")]
    public string filter2;
    /// <summary>
    /// 过滤后缀名3
    /// </summary>
    [ColumnMappingAttribute("filter3")]
    public string filter3;
    /// <summary>
    /// 过滤后缀名4
    /// </summary>
    [ColumnMappingAttribute("filter4")]
    public string filter4;
    /// <summary>
    /// 过滤后缀名5
    /// </summary>
    [ColumnMappingAttribute("filter5")]
    public string filter5;

    [ColumnMapping("SetName Min PubRef")]
    public int MinSetnameRef;
}
