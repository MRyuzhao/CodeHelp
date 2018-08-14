using System;

namespace CodeHelp.DomainService.UICommands
{
    public class UpdateDataTablesUiCommand
    {
        public Guid Id { get; set; }
        public DataTablesDTO Entity { get; set; }
    }
}