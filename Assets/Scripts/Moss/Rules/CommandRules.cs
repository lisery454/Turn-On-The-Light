using UnityEngine;

namespace Moss
{
    public static class CommandRules
    {
        public static void Execute<TCommand>(this MonoBehaviour self) where TCommand : ICommand, new()
        {
            self.Execute(new TCommand());
        }

        public static void Execute<TCommand>(this MonoBehaviour self, TCommand command) where TCommand : ICommand, new()
        {
            Game.Instance.CommandExecutor.Execute(command);
        }


        public static void Execute<TCommand>(this ISystem self) where TCommand : ICommand, new()
        {
            self.Execute(new TCommand());
        }

        public static void Execute<TCommand>(this ISystem self, TCommand command) where TCommand : ICommand, new()
        {
            Game.Instance.CommandExecutor.Execute(command);
        }
    }
}