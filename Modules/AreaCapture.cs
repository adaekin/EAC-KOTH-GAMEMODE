using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EACProject.Commands;
using Rocket.Core.Logging;
using Rocket.Core.Plugins;
using Rocket.Core.Steam;
using Rocket.Unturned;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Events;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Steamworks;
using UnityEngine;

namespace EACProject.Modules
{

    public class AreaCapture
    {
        ulong PointsGiven = 10;


        public void givingpoints()
        {
            
            if (EACProject.Instance.blue_areaplayer.Count > EACProject.Instance.red_areaplayer.Count)
            {
                EACProject.BluePoints = EACProject.BluePoints + PointsGiven;

            }
            if(EACProject.Instance.blue_areaplayer.Count < EACProject.Instance.red_areaplayer.Count)
            {
                EACProject.RedPoints = EACProject.RedPoints + PointsGiven;
            }
            if(EACProject.Instance.blue_areaplayer.Count == EACProject.Instance.red_areaplayer.Count)
            {
                UnturnedChat.Say("Equal", Color.blue);
            }
            
        }
        public void newgame()
        {
            EACProject.GameActive = false;
            EACProject.NewGameCreatining = true;
        }
    }
}
