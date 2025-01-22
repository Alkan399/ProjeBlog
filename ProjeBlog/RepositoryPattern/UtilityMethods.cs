using ProjeBlog.Models;
using ProjeBlog.RepositoryPattern.Concrete;
using ProjeBlog.RepositoryPattern.Interfaces;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;
using System;
using System.Linq;
using ProjeBlog.Context;
using System.Text;
using System.Net.Mail;
using System.Net;
using Org.BouncyCastle.Crypto.Macs;
using Microsoft.AspNetCore.Razor.Language;
using ProjeBlog.RepositoryPattern.Base;
using System.Threading.Tasks;

namespace ProjeBlog.RepositoryPattern
{
    public class UtilityMethods : IUtilityMethods
    {
        MyDbContext _context;
        private readonly IContentSetElementRepository _repoContentSetElement;
        IContentRepository _repoContent;
        IRepository<ContentSet> _repoContentSet; 
        public UtilityMethods(IContentSetElementRepository repoContentSetElement, IContentRepository contentRepository, IRepository<ContentSet> repoContentSet)
        {
            _repoContentSetElement = repoContentSetElement;
            _repoContent = contentRepository;
            _repoContentSet = repoContentSet;
        }
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

        //Düzenlenecek
        public string BlogContentFormatting2(Content content, int size)
        {
            string sString = content.Entry;
            //sString = content.Entry.Substring(0, 200);
            if (content.Entry.Length > size)//Burası değişecek
            {

                for (int y = 0; y < size-5; y++)
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
                        sString = content.Entry.Substring(0, size);
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

        public List<ContentSetElement> ContentSetElements(Enums.ElementLocation location)
        {
            var contentSetElements = _repoContentSetElement.GetByFilter(x => x.Status != Enums.DataStatus.Deleted && x.Location == location);

            List<ContentSetElement> contentSetElementList = new List<ContentSetElement>();

            foreach (var contentSetElement in contentSetElements)
            {
                ContentSetElement element = new ContentSetElement();
                if (contentSetElement.Recent == true)
                {
                    element.Recent = true;
                    element.Location = location;
                    element.ShowCount = contentSetElement.ShowCount;

                    var contents = _repoContent.GetActives().OrderByDescending(c => c.CreatedDate).Take(contentSetElement.ShowCount).ToList();
                    element.ContentSet = new ContentSet();
                    element.ContentSet.Name = "Son Gönderiler";
                    element.ContentSet.ContentSetContents = new List<ContentSetContent>();
                    foreach (var item in contents)
                    {
                        ContentSetContent contentSetContent = new ContentSetContent();
                        contentSetContent.Content = item;
                        contentSetContent.Content.Entry = BlogContentFormatting2(item, 80);
                        if(contentSetContent.Content.CoverImagePath == null)
                        {
                            contentSetContent.Content.CoverImagePath = "/images/noImage.png";
                        }
                        contentSetContent.ContentID = item.ID;
                        element.ContentSet.ContentSetContents.Add(contentSetContent);
                    }

                }
                else if (contentSetElement.MostPopular == true)
                {
                    element.MostPopular = true;
                    element.Location = location;
                    element.ShowCount = contentSetElement.ShowCount;
                    var contents = _repoContent.GetActives().OrderByDescending(c => c.Views).Take(contentSetElement.ShowCount).ToList();
                    element.ContentSet = new ContentSet();
                    element.ContentSet.Name = "En Çok Tıklananlar";
                    element.ContentSet.ContentSetContents = new List<ContentSetContent>();
                    foreach (var item in contents)
                    {
                        ContentSetContent contentSetContent = new ContentSetContent();
                        contentSetContent.Content = item;
                        contentSetContent.Content.Entry = BlogContentFormatting2(item, 80);
                        if (contentSetContent.Content.CoverImagePath == null)
                        {
                            contentSetContent.Content.CoverImagePath = "/images/noImage.png";
                        }
                        contentSetContent.ContentID = item.ID;
                        element.ContentSet.ContentSetContents.Add(contentSetContent);
                    }
                }
                else if (contentSetElement.Custom == true)
                {
                    element.MostPopular = true;
                    element.Location = location;
                    element.ShowCount = contentSetElement.ShowCount;
                    var contents = _repoContent.GetActives().OrderByDescending(c => c.Views).Take(contentSetElement.ShowCount).ToList();
                    element.ContentSet = new ContentSet();
                    element.ContentSet.Name = _repoContentSet.GetById((contentSetElement.ContentSetID).GetValueOrDefault()).Name;
                    element.ContentSet.ContentSetContents = new List<ContentSetContent>();
                    foreach (var item in contents)
                    {
                        ContentSetContent contentSetContent = new ContentSetContent();
                        contentSetContent.Content = item;
                        contentSetContent.Content.Entry = BlogContentFormatting2(item, 80);
                        if (contentSetContent.Content.CoverImagePath == null)
                        {
                            contentSetContent.Content.CoverImagePath = "/images/noImage.png";
                        }
                        contentSetContent.ContentID = item.ID;
                        element.ContentSet.ContentSetContents.Add(contentSetContent);
                    }
                }
                contentSetElementList.Add(element);
            }
            return contentSetElementList;
        }
        public string GenerateUsername(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            StringBuilder username = new StringBuilder();
            Random random = new Random();

            for (int i = 0; i < length; i++)
            {
                char randomChar = chars[random.Next(chars.Length)];
                username.Append(randomChar);
            }

            return username.ToString();
        }
        public string SendMail(string subject, string body, bool isBodyHtml, string email)
        {
            try
            {
                // SMTP sunucusu ve kimlik doğrulama bilgileri
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("ialkanarpatblog@gmail.com", "nomd lste xatz fcgd"),
                    EnableSsl = true
                };

                // Gönderilecek e-posta
                MailMessage mail = new MailMessage
                {
                    From = new MailAddress("ialkanarpatblog@gmail.com"),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = false
                };

                // Alıcı adresi ekle
                mail.To.Add(email);

                // E-postayı gönder
                smtpClient.Send(mail);
                Console.WriteLine("E-posta başarıyla gönderildi!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"E-posta gönderilemedi: {ex.Message}");
            }
            return "";
        }
    }
}
