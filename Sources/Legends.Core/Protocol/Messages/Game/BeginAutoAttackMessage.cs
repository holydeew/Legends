﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Legends.Core.IO;
using System.Numerics;
using Legends.Core.Geometry;

namespace Legends.Core.Protocol.Messages.Game
{
    public class BeginAutoAttackMessage : BaseMessage
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_S2C_BeginAutoAttack;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_S2C;
        public override Channel Channel => CHANNEL;

        public int targetNetId;
        public byte extraTime;
        public int futureProjectileNetId;
        public bool isCritical;
        public Vector2 targetPosition;
        public Vector2 sourcePosition;
        public Vector2 middleOfMap;

        public BeginAutoAttackMessage(int sourceNetId, int targetNetId, byte extraTime, int futureProjectileNetId, bool isCritical, 
            Vector2 targetPosition,Vector2 sourcePosition,Vector2 middleOfMap) : base(sourceNetId)
        {
            this.extraTime = extraTime;
            this.futureProjectileNetId = futureProjectileNetId;
            this.isCritical = isCritical;
            this.targetNetId = targetNetId;
            this.sourcePosition = sourcePosition;
            this.targetPosition = targetPosition;
        }
        public BeginAutoAttackMessage()
        {
            
        }
        public override void Deserialize(LittleEndianReader reader)
        {
            throw new NotImplementedException();
        }

        public override void Serialize(LittleEndianWriter writer)
        {
            writer.WriteInt(targetNetId);
            writer.WriteByte((byte)extraTime); // extraTime
            writer.WriteInt(futureProjectileNetId); // Basic attack projectile ID, to be spawned later

            if (isCritical)
                writer.WriteByte((byte)0x49); // attackSlot
            else
                writer.WriteByte((byte)0x40); // attackSlot

            writer.WriteByte((byte)0x80); // not sure what this is, but it should be correct (or maybe attacked x z y?) - 4.18
            writer.WriteByte((byte)0x01);
            writer.WriteShort(MovementVector.TargetXToNormalFormat(targetPosition.X,middleOfMap));
            writer.WriteByte((byte)0x80);
            writer.WriteByte((byte)0x01);
            writer.WriteShort(MovementVector.TargetYToNormalFormat(targetPosition.Y, middleOfMap));
            writer.WriteByte((byte)0xCC);
            writer.WriteByte((byte)0x35);
            writer.WriteByte((byte)0xC4);
            writer.WriteByte((byte)0xD1);
            writer.WriteFloat(sourcePosition.X);
            writer.WriteFloat(sourcePosition.Y);
        }
    }
}