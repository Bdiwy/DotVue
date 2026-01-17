using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DotVue;

[HtmlTargetElement("dot-vue")]
public class DotVueTagHelper : TagHelper
{
    [ViewContext]
    [HtmlAttributeNotBound]
    public ViewContext ViewContext { get; set; } = null!;

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.Attributes.SetAttribute("id", "vueApp"); 
        var component = ViewContext.ViewData["DotVue_Component"]?.ToString() ?? "";
        var data = ViewContext.ViewData["DotVue_Data"]?.ToString() ?? "{}";

        if (!string.IsNullOrEmpty(component))
        {
            var script = $@"
                <script>
                    document.addEventListener('DOMContentLoaded', function() {{
                        if (typeof initVuePage === 'function') {{
                            initVuePage('{component}', {data});
                        }} else {{
                            console.error('DotVue: initVuePage is not defined. Make sure vueHandler.js is loaded.');
                        }}
                    }});
                </script>";

            output.PostElement.AppendHtml(script);
        }
    }
}