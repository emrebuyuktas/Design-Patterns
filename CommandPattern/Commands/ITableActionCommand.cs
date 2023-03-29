using Microsoft.AspNetCore.Mvc;

namespace CommandPattern.Commands;

public interface ITableActionCommand
{
    IActionResult Execute();
}