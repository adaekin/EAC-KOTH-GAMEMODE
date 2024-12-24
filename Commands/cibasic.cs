using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rocket.API;
using Rocket.Core.Logging;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;
using SDG.Framework.Utilities;

namespace EACProject.Commands
{
    public class cibasic : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Both;

        public string Name => "cibasic";

        public string Help => "";

        public string Syntax => "";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string>();

        public readonly byte[] EMPTY_BYTE_ARRAY = new byte[0];

        public void Execute(IRocketPlayer caller, string[] command)
        {
            UnturnedPlayer player = (UnturnedPlayer)caller;
            Logger.Log("TEST");
            var playerinv = player.Inventory;
            deleteitems();
             void deleteitems()
            {
                for (byte page = 0; page < PlayerInventory.PAGES; page++)
                {
                    if (page == PlayerInventory.AREA)
                        continue;
                    var count = playerinv.getItemCount(page);
                    for (byte index = 0; index < count; index++)
                    {
                        playerinv.removeItem(page, 0);
                    }
                }
                System.Action removeuneqquiped = () =>
                {
                    for (byte i = 0; i < playerinv.getItemCount(2); i++)
                    {
                        playerinv.removeItem(2, 0);
                    }
                };
                player.Player.clothing.askWearBackpack(0, 0, EMPTY_BYTE_ARRAY, true);
                removeuneqquiped();
                player.Player.clothing.askWearGlasses(0, 0, EMPTY_BYTE_ARRAY, true);
                removeuneqquiped();
                player.Player.clothing.askWearHat(0, 0, EMPTY_BYTE_ARRAY, true);
                removeuneqquiped();
                player.Player.clothing.askWearMask(0, 0, EMPTY_BYTE_ARRAY, true);
                removeuneqquiped();
                player.Player.clothing.askWearPants(0, 0, EMPTY_BYTE_ARRAY, true);
                removeuneqquiped();
                player.Player.clothing.askWearShirt(0, 0, EMPTY_BYTE_ARRAY, true);
                removeuneqquiped();
                player.Player.clothing.askWearVest(0, 0, EMPTY_BYTE_ARRAY, true);
                removeuneqquiped();
            }
            
            
        }
    }
}
