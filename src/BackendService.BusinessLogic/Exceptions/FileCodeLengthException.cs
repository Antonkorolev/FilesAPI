using BackendService.BusinessLogic.Exceptions.Abstraction;

namespace BackendService.BusinessLogic.Exceptions;

public sealed class FileCodeLengthException(int fileCodeLength) : FileException($"FileCode length should 2 or more. Current value: {fileCodeLength}");