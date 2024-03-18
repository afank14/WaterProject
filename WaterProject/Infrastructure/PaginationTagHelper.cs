using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using WaterProject.Models.ViewModels;

namespace WaterProject.Infrastructure;

[HtmlTargetElement("div", Attributes = "page-model")]
public class PaginationTagHelper : TagHelper
{
    private IUrlHelperFactory _urlHelperFactory;

    public PaginationTagHelper(IUrlHelperFactory temp)
    {
        _urlHelperFactory = temp;
    }
    
    [ViewContext]
    [HtmlAttributeNotBound]
    public ViewContext? ViewContext { get; set; }
    public string? PageAction { get; set; }
    public PaginationInfo PageModel { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        if (ViewContext != null && PageModel != null)
        {
            IUrlHelper urlHelper = _urlHelperFactory.GetUrlHelper(ViewContext);

            TagBuilder result = new TagBuilder("div");

            for (int i = 1; i <= PageModel.TotalNumPages; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.Attributes["href"] = urlHelper.Action(PageAction, new { pageNum = i });
                tag.InnerHtml.Append(i.ToString());

                result.InnerHtml.AppendHtml(tag);
                result.InnerHtml.AppendHtml("\t");
            }

            output.Content.AppendHtml(result.InnerHtml);
        }
    }
}