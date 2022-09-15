namespace FastAPI.Layers.Infrastructure.Email.IO;

using Microsoft.Extensions.FileProviders;

using System;
using System.IO;

public sealed class EmbededResourceFileInfo : IFileInfo
{
    private long length = 0;
    private bool exists = false;
    private bool metaDataInitialized = false;

    private readonly string viewPath;
    private readonly AssemblyResource resource;

    public EmbededResourceFileInfo(string viewPath, AssemblyResource resource)
    {
        this.viewPath = viewPath;
        this.resource = resource;
        this.LastModified = DateTime.UtcNow;
    }

    public bool Exists
    {
        get
        {
            if (!metaDataInitialized)
            {
                InitializeMetaData();
            }

            return exists;
        }
    }

    public bool IsDirectory => false;

    public DateTimeOffset LastModified { get; }

    public long Length
    {
        get
        {
            if (!metaDataInitialized)
            {
                InitializeMetaData();
            }

            return length;
        }
    }

    public string Name => Path.GetFileName(viewPath);

    public string PhysicalPath => string.Empty;

    public Stream CreateReadStream()
    {
        return resource.Assembly.GetManifestResourceStream(resource.Name)
            ?? throw new FileNotFoundException();
    }

    private void InitializeMetaData()
    {
        using var stream = CreateReadStream();
        this.metaDataInitialized = true;

        if (stream is null)
        {
            return;
        }

        this.length = stream.Length;
        this.exists = stream.Length > 0;
    }
}