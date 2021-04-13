# DavidDiscordBot

## How to add commands
1. create new class that extends IModule
  
    public class myCommands : IMoule
  
2. 
  [Command("your command")]
  [Description("about your command")]
  public async Task Command(CommandContext ctx) {
    await ctx.RespondAsync("Hey");
  }
