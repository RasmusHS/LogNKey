using PassGenApplication.Commands.Dto;

namespace PassGenApplication.Commands;

public interface ICreatePasswordCommand
{
    void CreatePassword(PasswordCreateRequestDto request);
}