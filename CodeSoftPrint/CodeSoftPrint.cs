using System;
using System.Collections.Generic;
using System.Text;
using LabelManager2;
namespace CodeSoftPrint
{
   public class CodeSoftPrint
    {
       ApplicationClass lppx = new ApplicationClass();


       public string PrintDoc(string filepath,Dictionary<string,string> pStr,int printNum)
       {
           try
           {
               Document doc = lppx.Documents.Open(filepath, true);
               doc.Activate();
               bool bf = false;
               if (pStr.Keys.Count != doc.Variables.FormVariables.Count)
               {
                   return "程序变量数量与模板变量数量不一致";
               }
               for (int i = 0; i < doc.Variables.FormVariables.Count; i++)
               {
                   if (!(bf = CheckDic(pStr, doc.Variables.FormVariables.Item(i).Name)))
                   {
                       return "传入的变量与模板文件设置不一致";
                   }

                   doc.Variables.FormVariables.Item(pStr[doc.Variables.FormVariables.Item(i).Name.Trim()]).Value = pStr[doc.Variables.FormVariables.Item(i).Name.Trim()];
               }

               doc.PrintDocument(printNum);
               doc.Close(false);
               lppx.Documents.CloseAll(false);
               lppx.Quit();
               return string.Empty;
           }
           catch (Exception ex)
           {
               return ex.Message;
           }
       }

       private bool CheckDic(Dictionary<string,string> pStr,string str)
       {
           bool flag = false ;
           foreach(string item in pStr.Keys)
           {
               if (item.Trim().ToUpper() == str.Trim().ToUpper())
               {
                   flag = true;
                   break;
               }
           }
           return flag;
       }
    }
}
