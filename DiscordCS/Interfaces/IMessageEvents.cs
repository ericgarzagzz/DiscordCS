using System;
using System.Threading.Tasks;

namespace DiscordCS.Interfaces
{
    public interface IMessageEvents
    {
        Task MessageSent();
    }
}
