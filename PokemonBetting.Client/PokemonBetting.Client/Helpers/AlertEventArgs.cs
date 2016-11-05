using System;

namespace PokemonBetting.Client.Helpers
{
    public class AlertEventArgs : EventArgs
    {
        public string Title { get; }
        public string Message { get; }

        public AlertEventArgs(string title, string message)
        {
            this.Title = title;
            this.Message = message;
        }
    }
}
