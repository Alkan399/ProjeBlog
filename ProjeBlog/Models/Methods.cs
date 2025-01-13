using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;
using System.Web;
using HtmlAgilityPack;
using ProjeBlog.Context;
using ProjeBlog.RepositoryPattern.Concrete;
using ProjeBlog.RepositoryPattern.Interfaces;

namespace ProjeBlog.Models
{
    public class Methods
    {
        
        public string BlogContentFormatting(Content content)
        {
            string sString = content.Entry;
            //sString = content.Entry.Substring(0, 200);
            if (content.Entry.Length > 450)//Burası değişecek
            {

                for (int y = 0; y < 445; y++)
                {
                    //string sBr = String.Concat(sString[y + 1], sString[y + 2]) + sString[y + 3] + sString[y + 4];
                    string sBr = String.Concat(sString[y], sString[y + 1], sString[y + 2], sString[y + 3]);

                    //string sP = String.Concat(sString[y + 1], sString[y + 2]) + sString[y + 3];
                    if (sBr == "<br>")
                    {

                        sString = sString.Substring(0, y);

                        break;
                    }

                    else
                    {
                        sString = content.Entry.Substring(0, 450);
                        continue;
                    }
                }
            }
            else
            {
                for (int y = 0; y < content.Entry.Length - 5; y++)
                {
                    //string sBr = String.Concat(sString[y + 1], sString[y + 2]) + sString[y + 3] + sString[y + 4];
                    //string sP = String.Concat(sString[y + 1], sString[y + 2]) + sString[y + 3];

                    string sBr = String.Concat(sString[y], sString[y + 1], sString[y + 2], sString[y + 3]);
                    if (sBr == "<br>")
                    {

                        sString = sString.Substring(0, y);

                        break;
                    }

                    else
                    {
                        sString = content.Entry.Substring(0, content.Entry.Length);
                        continue;
                    }
                }

            }
            //var htmlDoc = new HtmlDocument();
            //htmlDoc.LoadHtml(sString);

            //return htmlDoc.DocumentNode.OuterHtml;
            sString = HttpUtility.HtmlDecode(sString);
            string TagReplaced = Regex.Replace(sString, "<.*?>", String.Empty);
            string pattern2 = @"<([a-zA-Z]+)([^>]*)>(.*?)<\/\1>|<([a-zA-Z]+)([^>]*)\/?>|<([a-zA-Z]+)([^>]*)$";
            
            string result = Regex.Replace(TagReplaced, pattern2, String.Empty);
            return result;
        }
    }
}