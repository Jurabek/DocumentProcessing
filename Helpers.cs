using System;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DocumentProcessing
{
    public class DisableTagHelper : TagHelper
    {      
        [HtmlAttributeName("asp-is-disabled")]
        public bool IsDisabled { set; get; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (IsDisabled)
            {
                var d = new TagHelperAttribute("disabled", "disabled");
                output.Attributes.Add(d);
            }
            base.Process(context, output);
        }
    }
    
    public static class Extensions
    {
        public static bool CaseInsensitiveContains(this string text, string value)
        {
            return text.IndexOf(value, StringComparison.CurrentCultureIgnoreCase) >= 0;
        }
    }
}