namespace BackendService.Extensions;

public static class ApplicationBuilderExtension
{
    public static IApplicationBuilder AddSwagger(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        return app;
    }
}