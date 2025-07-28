using Models;

namespace CLI.Managers.Contracts;

public interface ICliArgumentManager
{
    Task<RemapVariables> Execute(string[] args);
}