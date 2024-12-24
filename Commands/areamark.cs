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
    public class areamark : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Both;

        public string Name => "areamark";

        public string Help => "";

        public string Syntax => "";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string>();
        
        public void Execute(IRocketPlayer caller, string[] command)
        {
            UnturnedPlayer player = (UnturnedPlayer)caller;
            var x = player.Player.quests.markerPosition.x;
            var y = player.Player.quests.markerPosition.y;
            var z = player.Player.quests.markerPosition.z;
            UnturnedChat.Say(" X: " + x.ToString() + " Y: " + y.ToString() + " Z: " + z.ToString());
            
        } 
    } 
}
