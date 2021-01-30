namespace Game.Runtime.Dialogue
{
    public readonly struct YarnCommandPacket
    {
        public readonly string Name;
        public readonly string Action;

        public YarnCommandPacket(string InName, string InAction)
        {
            Name = InName;
            Action = InAction;
        }

        public override bool Equals(object obj) 
            => obj is YarnCommandPacket Packet && Name == Packet.Name && Action == Packet.Action;

        public override int GetHashCode() => base.GetHashCode();

        public static bool operator ==(YarnCommandPacket Self, YarnCommandPacket Other) 
            => Self.Name == Other.Name && Self.Action == Other.Action;

        public static bool operator !=(YarnCommandPacket Self, YarnCommandPacket Other) 
            => !(Self == Other);
    }
}