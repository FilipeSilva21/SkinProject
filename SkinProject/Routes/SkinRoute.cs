using Microsoft.EntityFrameworkCore;
using SkinProject.Data;
using SkinProject.Models;

namespace SkinProject.Routes;

public static class SkinRoute
{
    public static void SkinRoutes(this WebApplication app)
    {
        var route = app.MapGroup("skins");

        route.MapPost("",
            async (SkinRequest request, SkinContext context) =>
            {
                var skin = new Skin(request.Name);
                await context.Skins.AddAsync(skin);
                await context.SaveChangesAsync();
            });

        route.MapGet("", async (SkinContext context) =>
        {
            var skins = await context.Skins.AsNoTracking().ToListAsync(); 
            return Results.Ok(skins);
        });

        route.MapPut("{id:guid}",
            async (Guid id,
                SkinRequest request,
                SkinContext context) =>
            {
                var skin = context.Skins.FirstOrDefault
                    (x => x.Id == id);

                if (skin is null)
                {
                    return Results.NotFound();
                    
                } skin.ChangeName(request.Name);
                await context.SaveChangesAsync();
                
                return Results.Ok();
            });

        route.MapDelete("{id:guid}", async (Guid id, SkinContext context) =>
        {
            var skin = await context.Skins.FirstOrDefaultAsync(x => x.Id == id);

            if (skin is null)
            {
                return Results.NotFound();
            } 
            skin.SetInactive();
            await context.SaveChangesAsync();
            
            return Results.Ok();
        });


    }
}