using AngleSharp.Dom;

namespace App.Tests;

public static class Extensions
{
    public static IElement FindByTestId(this IRenderedFragment component, string testId)
    {
        return component.Find($"[data-testid='{testId}']");
    }
}