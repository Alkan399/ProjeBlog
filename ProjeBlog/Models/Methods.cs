using System;
using System.Reflection.Metadata.Ecma335;

namespace ProjeBlog.Models
{
    public class Methods
    {
        public string BlogContentFormatting(Content content)
        {
            string sString = content.Entry;
            //sString = content.Entry.Substring(0, 200);
            if (content.Entry.Length > 200)//Burası değişecek
            {

                for (int y = 0; y < 196; y++)
                {
                    string sBr = String.Concat(sString[y + 1], sString[y + 2]) + sString[y + 3] + sString[y + 4];
                    string sP = String.Concat(sString[y + 1], sString[y + 2]) + sString[y + 3];
                    if (sBr == "<br>" || sP == "<p>")
                    {

                        sString = sString.Substring(0, y);

                        break;
                    }

                    else
                    {
                        sString = content.Entry.Substring(0, 200);
                        continue;
                    }
                }
            }
            else
            {
                for (int y = 0; y < content.Entry.Length; y++)
                {
                    string sBr = String.Concat(sString[y + 1], sString[y + 2]) + sString[y + 3] + sString[y + 4];
                    string sP = String.Concat(sString[y + 1], sString[y + 2]) + sString[y + 3];
                    if (sBr == "<br>" || sP == "<p>")
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
            return sString;
        }
    }
}