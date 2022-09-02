namespace FastAPI.Libraries.Mapping;

using System;
using System.Linq;
using System.Reflection;

using AutoMapper;

using FastAPI.Libraries.Mapping.Contracts;

/// <summary>
/// Custom mapping profile for registering all mapping in the project.
/// </summary>
public class MappingProfile : Profile
{
    private const string MappingMethodName = "Mapping";
    private const string IMapFromGenericInterfaceName = "IMapFrom`1";

    /// <summary>
    /// Initializes a new instance of the <see cref="MappingProfile"/> class.
    /// </summary>
    /// <param name="assembly">Source mappings assembly.</param>
    public MappingProfile(Assembly assembly)
        => this.ApplyMappingsFromAssembly(assembly);

    private void ApplyMappingsFromAssembly(Assembly assembly)
    {
        var types = assembly.GetExportedTypes()
            .Where(t => t
                .GetInterfaces()
                .Any(i => i.IsGenericType && (i.GetGenericTypeDefinition() == typeof(IMapFrom<>) || i.GetGenericTypeDefinition() == typeof(ICustomMapFrom<>))))
            .ToList();

        foreach (var type in types)
        {
            object? instance = Activator.CreateInstance(type);

            var methodInfo = type.GetMethod(MappingMethodName) ??
                             type.GetInterface(IMapFromGenericInterfaceName)?.GetMethod(MappingMethodName);

            methodInfo?.Invoke(instance, new object[] { this });
        }
    }
}
