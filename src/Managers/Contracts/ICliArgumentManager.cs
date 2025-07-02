using Models;

namespace Managers.Contracts;

public interface ICliArgumentManager
{
    Task<RemapVariables> Execute(string[] args);
}