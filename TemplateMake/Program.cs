using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using RazorEngine;

namespace TemplateMake
{
    public class Program
    {
        private static string TemplatePath
        {
            get { return CurrentPath + "Template\\"; }
        }
        private static string CurrentPath
        {
            get { return Directory.GetCurrentDirectory() + "\\"; }
        }
        private static string TemplateConfigPath
        {
            get { return CurrentPath + "templateconfig.ini"; }
        }

        public static string DbConfig
        {
            get
            {
                return ConfigurationManager.AppSettings["conStr"];
            }
        }

        public static void WriteAllText(string filePath, string str)
        {
            FileInfo fi = new FileInfo(filePath);
            var di = fi.Directory;
            if (di != null && !di.Exists)
                di.Create();
            File.WriteAllText(filePath, str);
        }

        static void Main(string[] args)
        {
            SqlConnection cn = null;
            try
            {
                if(args.Length == 0) throw new Exception("没有输入文件");
                var inputtxt = args[0];
                var inputtxts = File.ReadAllLines(inputtxt);
                var dictionary = new Dictionary<string, string>();
                var tableName = "";
                foreach (var line in inputtxts)
                {
                    var lineSplit = line.Split(':');
                    if (lineSplit.Length != 2) { continue; }

                    switch (lineSplit[0].Trim().ToLower())
                    {
                        case "tablename":
                            tableName = lineSplit[1].Trim();
                            break;
                        default:
                            dictionary.Add(lineSplit[0].Trim(), lineSplit[1].Trim());
                            break;
                    }
                }
                var tableNames = tableName.Split(',');
                if (!tableNames.Any()) { throw new Exception("TableName不能为空"); }

                var alllines = File.ReadAllLines(TemplateConfigPath);
                cn = DbHelper.GetConnection();
                foreach (var tn in tableNames)
                {
                    var dt = DbHelper.GetTableMsg(cn, null, tn);
                    var tableMsgList = (from DataRow row in dt.Rows select new TableMsgModel(row)).ToList();

                    foreach (var line in alllines)
                    {
                        var lineParse = Razor.Parse(line, new { TableName = tn });

                        var lps = lineParse.Split('>');
                        if (lps.Length != 2) { continue; }
                        //先获取模版的文件内容
                        var template = File.ReadAllText(TemplatePath + lps[0]);
                        //转换为需要的内容
                        var parseTemplate = Razor.Parse(template, new { TableName = tn, Attr = dictionary, TableMsgList = tableMsgList, Helper = new RazorHelper() });
                        //放入设定的文件里面
                        WriteAllText(CurrentPath + lps[1], parseTemplate);
                    }
                }
                Console.WriteLine("生成成功");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                DbHelper.CloseConnection(cn);
                Console.ReadLine();
            }
        }
    }

    public class TableMsgModel
    {
        public string name { get; set; }
        public string xtype { get; set; }
        public string xtypename { get; set; }

        public string nameL
        {
            get { return name.ToLower(); }
        }
        
        public TableMsgModel(DataRow dr)
        {
            name = dr["name"].ToString();
            xtype = dr["xtype"].ToString();
            switch (xtype)
            {
                case "35": 
                case "98": 
                case "99": 
                case "165": 
                case "167": 
                case "173": 
                case "175": 
                case "189": 
                case "231": 
                case "239": 
                case "241": 
                    xtypename = "string"; 
                    break;
                case "59":
                case "60":
                case "62":
                case "106":
                case "108":
                case "122":
                    xtypename = "double";
                    break;
                case "48":
                case "52":
                case "56":
                case "127":
                case "240":
                    xtypename = "int";
                    break;
                case "61":
                case "58":
                case "43":
                case "42":
                case "41":
                case "40":
                    xtypename = "DateTime";
                    break;
                case "104":
                    xtypename = "bool";
                    break;
                case "34":
                    xtypename = "image";
                    break;
            }
        }
    }
}
