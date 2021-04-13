using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DavidDiscordBot.Core;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;

namespace DavidDiscordBot.Commands
{
    class basicCommands : IModule
    {
        [Command("alive")]
        [Description("Simple command to test if the bot is running!")]
        public async Task Alive(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            await ctx.RespondAsync("I'm alive!");
        }

        public Stream GetStream(Image img, ImageFormat format)
        {
            var ms = new MemoryStream();
            img.Save(ms, format);
            return ms;
        }


        [Command("shh")]
        public async Task Shush(CommandContext ctx)
        {
            await ctx.Message.DeleteAsync();
            await ctx.RespondAsync("Everyone, please be quiet", true);
        }


        [Command("say")]
        public async Task Say(CommandContext ctx)
        {
            await ctx.Message.DeleteAsync();
            await ctx.RespondAsync(ctx.RawArgumentString.Trim(), true);
        }


        [Command("stack")]
        [Description("the stack command generates a jpg of a certain conversation. The maximum length of a stack is a 100 messages")]
        public async Task Stack(CommandContext ctx)
        {

            await ctx.TriggerTypingAsync();
            String srcChannel = ctx.Channel.Name;
            await ctx.RespondAsync("The stack will run for a 40 messages");
            var intr = ctx.Client.GetInteractivityModule();
            List<MessageContext> messages = new List<MessageContext>();
            bool live = true;
            for (int i = 0; i < 40 && live; i += 0)
            {
                var reminderContent = await intr.WaitForMessageAsync(
                    c => c.Author.Id.ToString().Length > 0
                );
                if (reminderContent.Message.Content.Equals("$endstack"))
                {
                    live = false;
                    Console.WriteLine("ended");
                }
                else
                {
                    if (reminderContent.Channel.Name.Equals(srcChannel))
                    {
                        messages.Add(reminderContent);
                        i += 1;
                    }
                    else
                    {

                    }
                }


                Console.WriteLine(reminderContent);
            }
            ctx.RespondAsync("Generating stack...");
            Converter converter = new Converter();
            Console.WriteLine("converting");
            await ctx.RespondWithFileAsync(converter.textToImage(messages));
            Console.WriteLine("sent img");
        }

    }
}
