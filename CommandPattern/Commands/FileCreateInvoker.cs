using Microsoft.AspNetCore.Mvc;

namespace CommandPattern.Commands;

public class FileCreateInvoker
{
    private ITableActionCommand _actionCommand;
    private List<ITableActionCommand> tableActionCommands = new List<ITableActionCommand>();

    public bool SetCommand(ITableActionCommand actionCommand)
    {
        _actionCommand = actionCommand;
        return true;
    }
    
    
    public void AddCommand(ITableActionCommand tableActionCommand)
    {
        tableActionCommands.Add(tableActionCommand);
    }
    
    public IActionResult CreateFile()=>_actionCommand.Execute();

    public List<IActionResult> CreateFiles()=> tableActionCommands.Select(x => x.Execute()).ToList();
}