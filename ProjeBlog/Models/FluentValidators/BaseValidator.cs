using FluentValidation;

namespace ProjeBlog.Models.FluentValidators
{
    public class BaseValidator<T>: AbstractValidator<T> where T : class
    {
        public string MsgZorunlu = "* Zorunlu alan.";
        public string MsgMaxStr(int count) 
        {
            return "Maksimum "+ count.ToString() +" karakter olabilir.";
        }
        public string MsgMinStr(int count)
        {
            return "Minimum " + count.ToString() + " karakter olabilir.";
        }
    }
}
