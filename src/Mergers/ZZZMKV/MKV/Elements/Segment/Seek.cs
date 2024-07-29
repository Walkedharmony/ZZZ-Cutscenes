using ZZZCutscenes.Mergers.ZZZMKV.MKV.Generics;

namespace ZZZCutscenes.Mergers.ZZZMKV.MKV.Elements.Segment
{
    internal class Seek : MKVContainerElement
    {
        public MKVElement<byte[]> SeekId; // Top level signature
        public MKVElement<long> SeekPosition; // Position in binary

        public Seek(byte[] seekId, long seekPosition) : base(Signatures.Seek)
        {
            SeekId = new MKVElement<byte[]>(Signatures.SeekId, seekId);
            SeekPosition = new MKVElement<long>(Signatures.SeekPosition, seekPosition);
        }
    }
}
