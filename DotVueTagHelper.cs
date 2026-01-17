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

        var scripts = @"
            <script src='_content/DotVue/js/vue.global.js'></script>
            <script src='_content/DotVue/js/vue3-sfc-loader.js'></script>
            <script src='_content/DotVue/js/vueHandler.js'></script>";

        var component = ViewContext.ViewData["DotVue_Component"] ?? "";
        var data = ViewContext.ViewData["DotVue_Data"] ?? "{}";

        var initScript = $@"
            <script>
                window.addEventListener('load', () => {{
                    if (typeof initVuePage === 'function') 
                        initVuePage('{component}', {data});
                }});
            </script>";

        output.PostElement.AppendHtml(scripts);
        output.PostElement.AppendHtml(initScript);
    }
}