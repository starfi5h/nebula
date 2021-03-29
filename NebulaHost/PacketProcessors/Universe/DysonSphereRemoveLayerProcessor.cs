﻿using NebulaModel.Attributes;
using NebulaModel.Networking;
using NebulaModel.Packets.Universe;
using NebulaModel.Logger;
using NebulaModel.Packets.Processors;
using NebulaWorld.Universe;

namespace NebulaHost.PacketProcessors.Universe
{
    [RegisterPacketProcessor]
    class DysonSphereRemoveLayerProcessor : IPacketProcessor<DysonSphereRemoveLayerPacket>
    {
        private PlayerManager playerManager;

        public DysonSphereRemoveLayerProcessor()
        {
            playerManager = MultiplayerHostSession.Instance.PlayerManager;
        }

        public void ProcessPacket(DysonSphereRemoveLayerPacket packet, NebulaConnection conn)
        {
            Log.Info($"Processing DysonSphere Remove Layer notification for system {GameMain.data.galaxy.stars[packet.StarIndex].name} (Index: {GameMain.data.galaxy.stars[packet.StarIndex].index})");
            Player player = playerManager.GetPlayer(conn);
            if (player != null)
            {
                playerManager.SendPacketToOtherPlayers(packet, player);
                DysonSphere_Manager.IncomingDysonSpherePacket = true;
                GameMain.data.dysonSpheres[packet.StarIndex]?.RemoveLayer(packet.LayerId);
                DysonSphere_Manager.IncomingDysonSpherePacket = false;
            }
        }
    }
}