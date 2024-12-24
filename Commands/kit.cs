using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rocket.API;
using Rocket.Core.Logging;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;

namespace EACProject.Commands
{
    public class kit : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Both;

        public string Name => "kit";

        public string Help => "";

        public string Syntax => "";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string>();

        public void Execute(IRocketPlayer caller, string[] command)
        {

            Logger.Log("TEST");
            EACProject.Instance.LoadPlugin(); //Instanceten voidleri çekebiliyorum.
            if (command.Length < 1)
            {
                UnturnedChat.Say(caller, "Kits invalid");
                return;
            }
            var kit = EACProject.Instance.Configuration.Instance.Kits.FirstOrDefault(x => x.Name == command[0]);
            
            if (kit == null)
            {
                UnturnedChat.Say(caller, "Kit not found");
                return;
            }
            UnturnedPlayer player = (UnturnedPlayer)caller;
            foreach (var item in kit.Items)
            {
                player.GiveItem(item, 1);
                
            }
        }
    }
}
