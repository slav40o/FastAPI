namespace FastAPI.Layers.Infrastructure.Email.IO;

using System.Diagnostics;
using System.Reflection;

internal class AssemblyResourcesDictionary
{
    private readonly HashSet<Assembly> assemblies;
    private readonly Dictionary<string, AssemblyResource> resources;

    public AssemblyResourcesDictionary(params Assembly[] assemblies)
    {
        this.assemblies = new HashSet<Assembly>(assemblies);
        this.resources = new Dictionary<string, AssemblyResource>();

        LoadEmbededResources();
    }

    public AssemblyResource this[string index]
    {
        get => resources[index];
    }

    public int Count => resources.Count;

    public void Add(Assembly assembly)
    {
        var resourcesNames = assembly.GetEmbededResourceNames();
        foreach (var name in resourcesNames)
        {
            if (this.Contains(name))
            {
                Debug.Assert(this.Contains(name), $"Duplicate Resource Name {name}");
                continue;
            }

            this.resources.Add(name, new(name, assembly));
        }
    }

    public bool Contains(string resourceName)
    {
        return this.resources.ContainsKey(resourceName);
    }

    public IEnumerable<AssemblyResource> Find(string fileName)
    {
        string filter = fileName.Replace('/', '.');
        return this.resources.Where(r => r.Key.Contains(filter)).Select(r => r.Value);
    }

    private void LoadEmbededResources()
    {
        foreach (var assembly in this.assemblies)
        {
            Add(assembly);
        }
    }
}

public record AssemblyResource
{
    public AssemblyResource(string name, Assembly assembly)
    {
        Name = name;
        Assembly = assembly;
    }

    public string Name { get; }

    public Assembly Assembly { get; }
}