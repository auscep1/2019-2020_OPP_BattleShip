using System;
namespace GameServer.Models
{
    public class PlayerPvz
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long Score { get; set; }
        public long PosX { get; set; }
        public long PosY { get; set; }

        public PlayerPvz()
        {
        }
    }
}
