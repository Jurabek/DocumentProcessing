#pragma checksum "/Users/jurabek/builds/DocumentProcessing/Views/Role/Edit.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "189e801ff7da062214bda21c996b446c400e2611"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Role_Edit), @"mvc.1.0.view", @"/Views/Role/Edit.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Role/Edit.cshtml", typeof(AspNetCore.Views_Role_Edit))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "/Users/jurabek/builds/DocumentProcessing/Views/_ViewImports.cshtml"
using DocumentProcessing;

#line default
#line hidden
#line 2 "/Users/jurabek/builds/DocumentProcessing/Views/_ViewImports.cshtml"
using DocumentProcessing.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"189e801ff7da062214bda21c996b446c400e2611", @"/Views/Role/Edit.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8dd449cf89f32e1e1d8488273ad9011e638b73d6", @"/Views/_ViewImports.cshtml")]
    public class Views_Role_Edit : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<DocumentProcessing.ViewModels.Roles.RoleViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(57, 1, true);
            WriteLiteral("\n");
            EndContext();
#line 3 "/Users/jurabek/builds/DocumentProcessing/Views/Role/Edit.cshtml"
  
    ViewBag.Title = "Edit";

#line default
#line hidden
            BeginContext(91, 16, true);
            WriteLiteral("\n<h2>Edit</h2>\n\n");
            EndContext();
#line 9 "/Users/jurabek/builds/DocumentProcessing/Views/Role/Edit.cshtml"
 using (Html.BeginForm())
{
    

#line default
#line hidden
            BeginContext(140, 23, false);
#line 11 "/Users/jurabek/builds/DocumentProcessing/Views/Role/Edit.cshtml"
Write(Html.AntiForgeryToken());

#line default
#line hidden
            EndContext();
            BeginContext(169, 73, true);
            WriteLiteral("    <div class=\"form-horizontal\">\n        <h4>RoleViewModel</h4>\n        ");
            EndContext();
            BeginContext(243, 25, false);
#line 15 "/Users/jurabek/builds/DocumentProcessing/Views/Role/Edit.cshtml"
   Write(Html.HiddenFor(m => m.Id));

#line default
#line hidden
            EndContext();
            BeginContext(268, 24, true);
            WriteLiteral("\n        <hr />\n        ");
            EndContext();
            BeginContext(293, 64, false);
#line 17 "/Users/jurabek/builds/DocumentProcessing/Views/Role/Edit.cshtml"
   Write(Html.ValidationSummary(true, "", new { @class = "text-danger" }));

#line default
#line hidden
            EndContext();
            BeginContext(357, 46, true);
            WriteLiteral("\n        <div class=\"form-group\">\n            ");
            EndContext();
            BeginContext(404, 93, false);
#line 19 "/Users/jurabek/builds/DocumentProcessing/Views/Role/Edit.cshtml"
       Write(Html.LabelFor(model => model.Name, htmlAttributes: new { @class = "control-label col-md-2" }));

#line default
#line hidden
            EndContext();
            BeginContext(497, 53, true);
            WriteLiteral("\n            <div class=\"col-md-10\">\n                ");
            EndContext();
            BeginContext(551, 93, false);
#line 21 "/Users/jurabek/builds/DocumentProcessing/Views/Role/Edit.cshtml"
           Write(Html.EditorFor(model => model.Name, new { htmlAttributes = new { @class = "form-control" } }));

#line default
#line hidden
            EndContext();
            BeginContext(644, 17, true);
            WriteLiteral("\n                ");
            EndContext();
            BeginContext(662, 82, false);
#line 22 "/Users/jurabek/builds/DocumentProcessing/Views/Role/Edit.cshtml"
           Write(Html.ValidationMessageFor(model => model.Name, "", new { @class = "text-danger" }));

#line default
#line hidden
            EndContext();
            BeginContext(744, 81, true);
            WriteLiteral("\n            </div>\n        </div>\n\n        <div class=\"form-group\">\n            ");
            EndContext();
            BeginContext(826, 94, false);
#line 27 "/Users/jurabek/builds/DocumentProcessing/Views/Role/Edit.cshtml"
       Write(Html.LabelFor(model => model.Title, htmlAttributes: new { @class = "control-label col-md-2" }));

#line default
#line hidden
            EndContext();
            BeginContext(920, 53, true);
            WriteLiteral("\n            <div class=\"col-md-10\">\n                ");
            EndContext();
            BeginContext(974, 94, false);
#line 29 "/Users/jurabek/builds/DocumentProcessing/Views/Role/Edit.cshtml"
           Write(Html.EditorFor(model => model.Title, new { htmlAttributes = new { @class = "form-control" } }));

#line default
#line hidden
            EndContext();
            BeginContext(1068, 17, true);
            WriteLiteral("\n                ");
            EndContext();
            BeginContext(1086, 83, false);
#line 30 "/Users/jurabek/builds/DocumentProcessing/Views/Role/Edit.cshtml"
           Write(Html.ValidationMessageFor(model => model.Title, "", new { @class = "text-danger" }));

#line default
#line hidden
            EndContext();
            BeginContext(1169, 243, true);
            WriteLiteral("\n            </div>\n        </div>\n\n        <div class=\"form-group\">\n            <div class=\"col-md-offset-2 col-md-10\">\n                <input type=\"submit\" value=\"Save\" class=\"btn btn-default\" />\n            </div>\n        </div>\n    </div>\n");
            EndContext();
#line 40 "/Users/jurabek/builds/DocumentProcessing/Views/Role/Edit.cshtml"
}

#line default
#line hidden
            BeginContext(1414, 11, true);
            WriteLiteral("\n<div>\n    ");
            EndContext();
            BeginContext(1426, 40, false);
#line 43 "/Users/jurabek/builds/DocumentProcessing/Views/Role/Edit.cshtml"
Write(Html.ActionLink("Back to List", "Index"));

#line default
#line hidden
            EndContext();
            BeginContext(1466, 8, true);
            WriteLiteral("\n</div>\n");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<DocumentProcessing.ViewModels.Roles.RoleViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
