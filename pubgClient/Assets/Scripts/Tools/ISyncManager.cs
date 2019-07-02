using System;

namespace server.Utils
{
    public interface ISyncManager
    {
        void Exec();

        void Add(Action action);

        void Close();
    }
}
