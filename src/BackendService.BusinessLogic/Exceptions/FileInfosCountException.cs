using BackendService.BusinessLogic.Exceptions.Abstraction;

namespace BackendService.BusinessLogic.Exceptions;

public sealed class FileInfosCountException(int expected, int actual) : FileException($"FileInfo count {actual} not equal {expected}");