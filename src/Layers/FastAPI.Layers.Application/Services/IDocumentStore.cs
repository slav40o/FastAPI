namespace FastAPI.Layers.Application.Services;

using FastAPI.Layers.Domain.Common.Document;

public interface IDocumentStore
{
    Task<DocumentMeta> StoreDocument(string fileName, byte[] data);

    Task<DocumentMeta> StoreDocument(string labelName, string fileName, byte[] data);

    Task<DocumentMeta> StoreDocument(string labelName, string fileName, string extension, byte[] data);

    Task<byte[]> RetrieveDocument(string storageId);

    Task<byte[]> RetrieveDocument(DocumentMeta document);
}
