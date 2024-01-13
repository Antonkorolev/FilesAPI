using BackendService.BusinessLogic.Exceptions.Abstraction;

namespace BackendService.BusinessLogic.Exceptions;

public sealed class FilesCountException(int expected, int actual) : FileException($"Files count {actual} not equal {expected}");