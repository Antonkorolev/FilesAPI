using BackendService.BusinessLogic.Exceptions.Abstraction;

namespace BackendService.BusinessLogic.Exceptions;

public sealed class FileInfoNotFoundException() : FileException("FileInfo not found in database");