using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rocket.API;
using Rocket.Core.Logging;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using Rocket.Unturned.Events;
using SDG.Unturned;
using Steamworks;
using SDG.NetTransport;

namespace EACProject.Commands
{
    public class area : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Both;

        public string Name => "area";

        public string Help => "";

        public string Syntax => "";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string>();
        
        public void Execute(IRocketPlayer caller, string[] command)
        {
            UnturnedPlayer player = (UnturnedPlayer)caller;
            var x = player.Player.transform.position.x;
            var y = player.Player.transform.position.y;
            var z = player.Player.transform.position.z;
            UnturnedChat.Say(" X: " + x.ToString() + " Y: " + y.ToString() + " Z: " + z.ToString());
            
        } 
    } 
}
