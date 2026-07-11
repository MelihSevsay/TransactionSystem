using System;
using System.Collections.Generic;
using System.Text;

namespace TransactionSystem.Application.Common;

public class NotFoundException : Exception
{
    public NotFoundException(string entityName, object key)
        : base($"{entityName} with id '{key}' was not found.")
    {
    }
}