using UnityEngine;
using MsgPack;

namespace Multiverse.Network.Packets
{
    public class PLAYER_MOVE : Packet
    {
        public PLAYER_MOVE(Vector3 position, int instanceId) : base(0x000A) {
            var payload = new MessagePackObjectDictionary {
                { "id", instanceId },
                { "x", position.x },
                { "y", position.y },
                { "z", position.z },
            };

            Serialize(payload);
        }
    }
}
