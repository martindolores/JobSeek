#pragma checksum "D:\Projects\JobSeek\Areas\Recruiter\Views\Candidates\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a516d3d95792b542f896fa3cc2e4a68f2ccdc0b2"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Recruiter_Views_Candidates_Details), @"mvc.1.0.view", @"/Areas/Recruiter/Views/Candidates/Details.cshtml")]
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
#line 1 "D:\Projects\JobSeek\Areas\Recruiter\Views\_ViewImports.cshtml"
using JobSeek;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Projects\JobSeek\Areas\Recruiter\Views\_ViewImports.cshtml"
using JobSeek.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a516d3d95792b542f896fa3cc2e4a68f2ccdc0b2", @"/Areas/Recruiter/Views/Candidates/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"50237c23c3818123f8d15ffd1460b198434fd316", @"/Areas/Recruiter/Views/_ViewImports.cshtml")]
    public class Areas_Recruiter_Views_Candidates_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<JobSeek.Models.Application>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Apply", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("enctype", new global::Microsoft.AspNetCore.Html.HtmlString("multipart/form-data"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "D:\Projects\JobSeek\Areas\Recruiter\Views\Candidates\Details.cshtml"
  
    ViewData["Title"] = "Candidates";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "a516d3d95792b542f896fa3cc2e4a68f2ccdc0b24437", async() => {
                WriteLiteral("\r\n    <div class=\"p-4 rounded border\">\r\n        <h6 style=\"font-weight:300\">Application For</h6>\r\n        <h1 class=\"display-4\">");
#nullable restore
#line 9 "D:\Projects\JobSeek\Areas\Recruiter\Views\Candidates\Details.cshtml"
                         Write(Model.Jobseeker.FirstName);

#line default
#line hidden
#nullable disable
                WriteLiteral(" ");
#nullable restore
#line 9 "D:\Projects\JobSeek\Areas\Recruiter\Views\Candidates\Details.cshtml"
                                                    Write(Model.Jobseeker.LastName);

#line default
#line hidden
#nullable disable
                WriteLiteral("</h1>\r\n        <h4>");
#nullable restore
#line 10 "D:\Projects\JobSeek\Areas\Recruiter\Views\Candidates\Details.cshtml"
       Write(Model.Jobseeker.PhoneNumber);

#line default
#line hidden
#nullable disable
                WriteLiteral("</h4>\r\n        <h4>");
#nullable restore
#line 11 "D:\Projects\JobSeek\Areas\Recruiter\Views\Candidates\Details.cshtml"
       Write(Model.Jobseeker.Email);

#line default
#line hidden
#nullable disable
                WriteLiteral("</h4>\r\n        <hr />\r\n        <h6>");
#nullable restore
#line 13 "D:\Projects\JobSeek\Areas\Recruiter\Views\Candidates\Details.cshtml"
       Write(Model.JobOffer.Title);

#line default
#line hidden
#nullable disable
                WriteLiteral("</h6>\r\n        <h6>");
#nullable restore
#line 14 "D:\Projects\JobSeek\Areas\Recruiter\Views\Candidates\Details.cshtml"
       Write(Model.JobOffer.JobType.JobTypes);

#line default
#line hidden
#nullable disable
                WriteLiteral(" (");
#nullable restore
#line 14 "D:\Projects\JobSeek\Areas\Recruiter\Views\Candidates\Details.cshtml"
                                         Write(Model.JobOffer.JobCategory.JobCategories);

#line default
#line hidden
#nullable disable
                WriteLiteral(")</h6>\r\n        <h6><i class=\"fa-solid fa-dollar-sign\"></i>");
#nullable restore
#line 15 "D:\Projects\JobSeek\Areas\Recruiter\Views\Candidates\Details.cshtml"
                                              Write(Model.JobOffer.Wage);

#line default
#line hidden
#nullable disable
                WriteLiteral("</h6>\r\n        <br />\r\n        <div class=\"container\">\r\n            <div class=\"row\">\r\n                <div class=\"col\">\r\n                    <div class=\"p-4 rounded border\">\r\n                        <h6>Resume for ");
#nullable restore
#line 21 "D:\Projects\JobSeek\Areas\Recruiter\Views\Candidates\Details.cshtml"
                                  Write(Model.Jobseeker.FirstName);

#line default
#line hidden
#nullable disable
                WriteLiteral(" ");
#nullable restore
#line 21 "D:\Projects\JobSeek\Areas\Recruiter\Views\Candidates\Details.cshtml"
                                                             Write(Model.Jobseeker.LastName);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"</h6>
                        <div class=""row"">
                            <div class=""col"">
                                <a class=""btn btn-outline-secondary"" data-toggle=""modal"" data-target=""#exampleModal"">Preview</a>
                            </div>
                            <div class=""col"">
                                <a class=""btn btn-secondary""");
                BeginWriteAttribute("href", " href=\"", 1284, "\"", 1304, 1);
#nullable restore
#line 27 "D:\Projects\JobSeek\Areas\Recruiter\Views\Candidates\Details.cshtml"
WriteAttributeValue("", 1291, Model.Resume, 1291, 13, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(">Download</a>\r\n                            </div>\r\n                        </div>\r\n\r\n                    </div>\r\n");
#nullable restore
#line 32 "D:\Projects\JobSeek\Areas\Recruiter\Views\Candidates\Details.cshtml"
                     if (Model.CoverLetter != null)
                    {

#line default
#line hidden
#nullable disable
                WriteLiteral("                        <div class=\"p-4 rounded border\">\r\n                            <h6>Cover Letter for for ");
#nullable restore
#line 35 "D:\Projects\JobSeek\Areas\Recruiter\Views\Candidates\Details.cshtml"
                                                Write(Model.Jobseeker.FirstName);

#line default
#line hidden
#nullable disable
                WriteLiteral(" ");
#nullable restore
#line 35 "D:\Projects\JobSeek\Areas\Recruiter\Views\Candidates\Details.cshtml"
                                                                           Write(Model.Jobseeker.LastName);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"</h6>
                            <div class=""row"">
                                <div class=""col"">
                                    <a class=""btn btn-outline-secondary"" data-toggle=""modal"" data-target=""#exampleModal1"">Preview</a>
                                </div>
                                <div class=""col"">
                                    <a class=""btn btn-secondary""");
                BeginWriteAttribute("href", " href=\"", 2052, "\"", 2077, 1);
#nullable restore
#line 41 "D:\Projects\JobSeek\Areas\Recruiter\Views\Candidates\Details.cshtml"
WriteAttributeValue("", 2059, Model.CoverLetter, 2059, 18, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(">Download</a>\r\n                                </div>\r\n                            </div>\r\n                        </div>\r\n");
#nullable restore
#line 45 "D:\Projects\JobSeek\Areas\Recruiter\Views\Candidates\Details.cshtml"
                    }

#line default
#line hidden
#nullable disable
                WriteLiteral(@"                    <div class=""form-group"">
                        <input type=""submit"" class=""btn btn-primary"" value=""Submit"">
                    </div>
                </div>
                <div class=""col"">
                    <h6>Job Description</h6>
                    <hr />
                    ");
#nullable restore
#line 53 "D:\Projects\JobSeek\Areas\Recruiter\Views\Candidates\Details.cshtml"
               Write(Model.JobOffer.Description);

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                    <br />\r\n                    <hr />\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"

<!-- Modal -->
<div class=""modal fade"" id=""exampleModal"" tabindex=""-1"" role=""dialog"" aria-labelledby=""exampleModalLong"" aria-hidden=""true"">
    <div class=""modal-dialog modal-lg"" role=""document"">
        <div class=""modal-content"">
            <div class=""modal-header"">
                <h5 class=""modal-title"" id=""exampleModalLabel"">Resume</h5>
                <button type=""button"" class=""close"" data-dismiss=""modal"" aria-label=""Close"">
                    <span aria-hidden=""true"">&times;</span>
                </button>
            </div>
            <div class=""modal-body"">
                <embed");
            BeginWriteAttribute("src", " src=\"", 3319, "\"", 3347, 1);
#nullable restore
#line 73 "D:\Projects\JobSeek\Areas\Recruiter\Views\Candidates\Details.cshtml"
WriteAttributeValue("", 3325, ViewBag.resumePreview, 3325, 22, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" class=""w-100"" />
            </div>
            <div class=""modal-footer"">
                <button type=""button"" class=""btn btn-secondary"" data-dismiss=""modal"">Close</button>
            </div>
        </div>
    </div>
</div>

<div class=""modal fade"" id=""exampleModal1"" tabindex=""-1"" role=""dialog"" aria-labelledby=""exampleModalLabel"" aria-hidden=""true"">
    <div class=""modal-dialog modal-lg"" role=""document"">
        <div class=""modal-content"">
            <div class=""modal-header"">
                <h5 class=""modal-title"" id=""exampleModalLabel"">Cover Letter</h5>
                <button type=""button"" class=""close"" data-dismiss=""modal"" aria-label=""Close"">
                    <span aria-hidden=""true"">&times;</span>
                </button>
            </div>
            <div class=""modal-body"" >
                <embed");
            BeginWriteAttribute("src", " src=\"", 4193, "\"", 4226, 1);
#nullable restore
#line 92 "D:\Projects\JobSeek\Areas\Recruiter\Views\Candidates\Details.cshtml"
WriteAttributeValue("", 4199, ViewBag.coverLetterPreview, 4199, 27, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"w-100\" />\r\n            </div>\r\n            <div class=\"modal-footer\">\r\n                <button type=\"button\" class=\"btn btn-secondary\" data-dismiss=\"modal\">Close</button>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<JobSeek.Models.Application> Html { get; private set; }
    }
}
#pragma warning restore 1591
