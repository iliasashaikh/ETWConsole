using Microsoft.Diagnostics.Tracing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trycatchthat.EtwConsole.EventSources
{

    enum Operation
    {
        Insert,
        Update,
        Delete,
        Read
    }

    public sealed class DBOperations : EventSource
    {
        public static DBOperations Log = new DBOperations();

        public void Insert(string TableName) => Execute(1, 0, TableName, Operation.Insert);

        public void Delete(int Key, string TableName) => Execute(2, 0, TableName, Operation.Delete);

        public void Update(int Key, string TableName) => Execute(3, 0, TableName, Operation.Update);

        public void Read(int Key, string TableName) => Execute(4, 0, TableName, Operation.Read);

        [NonEvent]
        private void Execute(int operationId, int Key, string TableName, Operation op)
        {
            WriteEvent(operationId, Key, TableName, op);
        }

    }
}
