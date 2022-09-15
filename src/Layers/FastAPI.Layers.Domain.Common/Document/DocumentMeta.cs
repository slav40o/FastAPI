namespace FastAPI.Layers.Domain.Common.Document;

using FastAPI.Layers.Domain.Entities;

public class DocumentMeta : Entity<int>
{
    public DocumentMeta(string fileName, string labelName, string extension, string storageId)
    {
        this.FileName = fileName;
        this.LabelName = labelName;
        this.Extension = extension;
        this.StorageId = storageId;
    }

    public string FileName { get; private set; }

    public string LabelName { get; private set; }

    public string Extension { get; private set; }

    public string StorageId { get; private set; }
}