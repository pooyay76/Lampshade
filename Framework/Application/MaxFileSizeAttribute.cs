using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Framework.Application
{
    public class MaxFileSizeAttribute : ValidationAttribute, IClientModelValidator
    {
        private readonly float _maxFileSizeMb;

        public MaxFileSizeAttribute(float maxFileSizeMb)
        {
            _maxFileSizeMb = maxFileSizeMb;
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-maxFileSize", ErrorMessage);
        }

        public override bool IsValid(object value)
        {
            var file = value as IFormFile;
            if (file == null) { return true; }
            return file.Length <= _maxFileSizeMb * 1024 * 1024;
        }
        private bool MergeAttribute(IDictionary<string,string> attributes,string key, string value)
        {
            {
                if (attributes.ContainsKey(key))
                {
                    return false;
                }
                attributes.Add(key, value);
                return true;
            }
        }
    } 
}
