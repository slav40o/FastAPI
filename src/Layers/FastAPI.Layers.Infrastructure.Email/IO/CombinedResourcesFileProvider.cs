namespace FastAPI.Layers.Infrastructure.Email.IO;

using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;

using System.Reflection;

/// <summary>
/// Provides files both from physical file path and from embedder resource path
/// </summary>
public class CombinedResourcesFileProvider : IFileProvider
{
    private readonly IFileProvider physicalFileProvider;
    private readonly AssemblyResourcesDictionary resourceDictionary;

    /// <summary>
    /// Create instance of a file provider
    /// </summary>
    /// <param name="rootPath">Application physical root path</param>
    /// <param name="assemblyPreffix">Filter for assemblies scanning with prefix</param>
    public CombinedResourcesFileProvider(string rootPath, string? assemblyPreffix = null)
    {
        this.physicalFileProvider = new PhysicalFileProvider(rootPath);
        this.resourceDictionary = new(ScanAssemblies(assemblyPreffix));
    }

    public IDirectoryContents GetDirectoryContents(string subpath)
    {
        return this.physicalFileProvider.GetDirectoryContents(subpath);
    }

    public IFileInfo GetFileInfo(string subpath)
    {
        var resource = this.resourceDictionary.Find(subpath).FirstOrDefault();
        if (resource is not null)
        {
            var fileInfo = new EmbededResourceFileInfo(subpath, resource);
            if (fileInfo.Exists)
            {
                return fileInfo;
            }
        }

        return this.physicalFileProvider.GetFileInfo(subpath);
    }

    public IChangeToken Watch(string filter)
    {
        return this.physicalFileProvider.Watch(filter);
    }

    private static Assembly[] ScanAssemblies(string? assemblyPreffix)
    {
        if (assemblyPreffix is null)
        {
            assemblyPreffix = TryGetDefaultPreffix(assemblyPreffix);
        }

        var assemblies = AppDomain.CurrentDomain.GetAssemblies();

        if (!string.IsNullOrEmpty(assemblyPreffix))
        {
            assemblies = assemblies.Where(a => a.FullName is not null && a.FullName.StartsWith(assemblyPreffix)).ToArray();
        }

        return assemblies;
    }

    private static string? TryGetDefaultPreffix(string? assemblyPreffix)
    {
        string? executingAssemblyName = Assembly.GetEntryAssembly()?.FullName;
        if (executingAssemblyName is not null)
        {
            assemblyPreffix = executingAssemblyName.Split('.').FirstOrDefault();
        }

        return assemblyPreffix;
    }
}