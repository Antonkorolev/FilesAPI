using BackendService.BusinessLogic.Exceptions.Abstraction;

namespace BackendService.BusinessLogic.Exceptions;

public sealed class FileNotFoundException(string path) : FileException($"File not found. Current path: {path}");