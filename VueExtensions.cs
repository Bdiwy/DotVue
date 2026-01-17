using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace DotVue;

public static class VueExtensions
{
    public static ViewResult Vue(this Controller controller, string componentName, object? model = null)
    {
        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false
        };

        controller.ViewData["DotVue_Component"] = componentName;
        controller.ViewData["DotVue_Data"] = model != null 
            ? JsonSerializer.Serialize(model, jsonOptions) 
            : "{}";

        return controller.View("DotVueMaster");
    }
}