using Rocket.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EACProject.Modules;

namespace EACProject
{
    public class EACConfiguration : IRocketPluginConfiguration
    {

        public Kit[] Kits { get; set; }
        public void LoadDefaults()
        {
            Kits = new Kit[]
            {
                new Kit()
                {
                    Name = "BLUE",
                    Items = new ushort[]
                    {
                        46725,
                        46760,
                        46770,
                        46775,
                        46780,
                        58606,
                        58332,
                        58332,
                        58332,
                        15,
                        15,
                        15,
                        15,
                        58024,
                        58024,
                        58141,
                        58141
                    }

                }
            };
        }
    }
}
