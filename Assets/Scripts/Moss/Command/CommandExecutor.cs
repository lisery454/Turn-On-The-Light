namespace Moss
{
    public class CommandExecutor
    {
        public void Execute(ICommand command)
        {
            if (Game.Instance.Context != null)
                Game.Instance.Context.Container.injector.Inject(command, InjectFlag.Service | InjectFlag.State);

            command.Execute();
        }
    }
}