﻿using UnityEngine;
using System.Collections;

namespace Multiverse.Network.Packets {
	public sealed class CMSG_HANDSHAKE : Packet {
		public CMSG_HANDSHAKE() : base(0x0000)
		{
			this.EnsureCapacity (0);
		}
	}
}
