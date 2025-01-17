﻿using ProjeBlog.Models;
using System.Collections.Generic;

namespace ProjeBlog.RepositoryPattern.Interfaces
{
    public interface IUtilityMethods
    {
        public string BlogContentFormatting(Content content);
        public List<ContentSetElement> ContentSetElements(Enums.ElementLocation location);
        public string GenerateUsername(int length);
        public string SendMail(string subject, string body, bool isBodyHtml, string email);
    }
}
