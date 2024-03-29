﻿using System;
using RPBUtilities;

namespace RPBPacketBase
{
    public abstract class RPBPacket
    {
        public virtual int PacketId { get; set; }

        public byte[] GetData()
        {
            var buffer = new ByteBuffer(GetSize() + sizeof(int));
            buffer.Write(PacketId);
            Serialize(buffer);
            return buffer.Data;
        }

        public virtual void Serialize(ByteBuffer buffer)
        {
        }

        public virtual T Deserialize<T>(ByteBuffer buffer) where T : RPBPacket
        {
            return default;
        }

        public virtual int GetSize()
        {
            return 0;
        }
    }


    public abstract class RPBPacketData
    {
        public virtual void Serialize(ByteBuffer buffer) {}
        public virtual int GetSize()
        {
            return 0;
        }
    }
    [AttributeUsage(AttributeTargets.Class)]
    public abstract class BasePacketAttribute : Attribute
    {
        public readonly byte Category;
        public readonly byte Protocol;

        protected BasePacketAttribute(byte category, byte protocol)
        {
            Category = category;
            Protocol = protocol;
        }
    }

    public abstract class ErrorPacket : RPBPacket
    {
        public int ErrorCode;
    }
}