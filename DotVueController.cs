using Microsoft.AspNetCore.Mvc;

namespace DotVue;

public abstract class DotVueController : Controller
{
    protected IActionResult Vue(string componentName, object? model = null)
    {
        return VueExtensions.Vue(this, componentName, model);
    }
}