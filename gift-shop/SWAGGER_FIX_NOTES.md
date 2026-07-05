# Swagger/OpenAPI Configuration Fix

## Issue Summary
- `'OpenApiSecurityScheme' does not contain a definition for 'Reference'`
- `'OpenApiReference' could not be found`

## Root Causes
1. Missing `using Microsoft.OpenApi.Models;` namespace
2. Swashbuckle.AspNetCore package may be outdated (v5.x instead of v6.x+)
3. Incorrect SecurityScheme property usage

## Required NuGet Package
Ensure your .csproj contains:
```xml
<PackageReference Include="Swashbuckle.AspNetCore" Version="6.4.0" />
```

The v6.x version includes:
- `Microsoft.OpenApi.Models` namespace
- `OpenApiReference` type
- Proper `Reference` property on `OpenApiSecurityScheme`

## Files Modified
1. **SwaggerExtensions.cs** - Recreated with proper namespaces and modern API

## Next Steps
1. Update NuGet packages: `dotnet package update Swashbuckle.AspNetCore`
2. Rebuild the solution: `dotnet build`
3. Verify appsettings configuration calls the extension correctly

## Verification Checklist
- [ ] SwaggerExtensions.cs has `using Microsoft.OpenApi.Models;`
- [ ] SwaggerExtensions.cs has `using Swashbuckle.AspNetCore.SwaggerGen;`
- [ ] Program.cs calls `AddSwaggerWithSecurity()` extension
- [ ] Program.cs configures Swagger middleware with `app.UseSwagger()` and `app.UseSwaggerUI()`
- [ ] Swashbuckle.AspNetCore NuGet package is v6.4.0 or higher
