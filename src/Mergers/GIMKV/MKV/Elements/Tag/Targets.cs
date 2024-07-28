using ZZZCutscenes.Mergers.ZZZMKV.MKV.Generics;

namespace ZZZCutscenes.Mergers.ZZZMKV.MKV.Elements.Tag
{
    internal class Targets : MKVContainerElement
    {
        public MKVElement<byte> TargetTypeValue;
        public MKVElement<byte[]> TargetTypeUID;

        public Targets(byte[] trackUID) : base(Signatures.Targets)
        {
            TargetTypeValue = new MKVElement<byte>(Signatures.TargetTypeValue, 0x32); // Will always be 0x32
            TargetTypeUID = new MKVElement<byte[]>(Signatures.TargetTypeUID, trackUID);
        }
    }
}
