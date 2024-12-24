﻿using System;
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
    public class teamui : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Both;

        public string Name => "teamui";

        public string Help => "";

        public string Syntax => "";

        public List<string> Aliases => new List<string>();
        
        public List<string> Permissions => new List<string>();
        
        public void Execute(IRocketPlayer caller, string[] command)
        {
            UnturnedPlayer player = (UnturnedPlayer)caller;
            EACProject.Instance.SendTeamui(player);


        }
 
    } 
}
