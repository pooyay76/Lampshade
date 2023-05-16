using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace Framework.Application
{
    public class AllowedFileExtensionsAttribute : ValidationAttribute, IClientModelValidator
    {
        private readonly string[] allowedExtensions;

        public AllowedFileExtensionsAttribute(string[] allowedExtensions)
        {
            this.allowedExtensions = allowedExtensions;
        }


        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            } 
            var file = value as IFormFile;
            if (allowedExtensions.Contains(Path.GetExtension(file.FileName))) 
                return true;
            else
                return false; 
        }
        public void AddValidation(ClientModelValidationContext context)
        {
            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-allowedFileExtensions", ErrorMessage);
        }
        private bool MergeAttribute(IDictionary<string, string> attributes, string key, string value)
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
