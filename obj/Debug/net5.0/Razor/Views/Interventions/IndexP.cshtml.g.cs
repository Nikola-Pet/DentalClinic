#pragma checksum "C:\Users\19pet\source\repos\Dental\Dental\Views\Interventions\IndexP.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3e7053e038bce37bf86f6daee9aa79e98327c214"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Interventions_IndexP), @"mvc.1.0.view", @"/Views/Interventions/IndexP.cshtml")]
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
#nullable restore
#line 1 "C:\Users\19pet\source\repos\Dental\Dental\Views\_ViewImports.cshtml"
using Dental;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\19pet\source\repos\Dental\Dental\Views\_ViewImports.cshtml"
using Dental.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3e7053e038bce37bf86f6daee9aa79e98327c214", @"/Views/Interventions/IndexP.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"afdc330c515c6736beeedf42d67276ab3ba48975", @"/Views/_ViewImports.cshtml")]
    public class Views_Interventions_IndexP : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Dental.Models.Intervention>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "C:\Users\19pet\source\repos\Dental\Dental\Views\Interventions\IndexP.cshtml"
  
    ViewData["Title"] = "Index";


#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<h1>Index</h1>


<table class=""table"">
    <thead>
        <tr>
            <th>
                Date
            </th>
            <th>
                Description
            </th>
            <th>
                Dentist
            </th>
            <th></th>
        </tr>
    </thead>
    <tbody>
");
#nullable restore
#line 27 "C:\Users\19pet\source\repos\Dental\Dental\Views\Interventions\IndexP.cshtml"
 foreach (var item in Model) {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <tr>\r\n\r\n            <td>\r\n                ");
#nullable restore
#line 31 "C:\Users\19pet\source\repos\Dental\Dental\Views\Interventions\IndexP.cshtml"
           Write(Html.DisplayFor(modelItem => item.DateTime));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 34 "C:\Users\19pet\source\repos\Dental\Dental\Views\Interventions\IndexP.cshtml"
           Write(Html.DisplayFor(modelItem => item.Description));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n");
#nullable restore
#line 37 "C:\Users\19pet\source\repos\Dental\Dental\Views\Interventions\IndexP.cshtml"
                  string fullName = IDentist.GetDentistFullNamebyId(item.DentistId); 

#line default
#line hidden
#nullable disable
            WriteLiteral("                ");
#nullable restore
#line 38 "C:\Users\19pet\source\repos\Dental\Dental\Views\Interventions\IndexP.cshtml"
           Write(fullName);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n\r\n        </tr>\r\n");
#nullable restore
#line 42 "C:\Users\19pet\source\repos\Dental\Dental\Views\Interventions\IndexP.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("    </tbody>\r\n</table>\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public Dental.Data.IDentistRepo IDentist { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Dental.Models.Intervention>> Html { get; private set; }
    }
}
#pragma warning restore 1591
