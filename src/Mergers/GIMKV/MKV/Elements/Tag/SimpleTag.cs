using ZZZCutscenes.Mergers.ZZZMKV.MKV.Generics;

namespace ZZZCutscenes.Mergers.ZZZMKV.MKV.Elements.Tag
{
    internal class SimpleTag : MKVContainerElement
    {
        public MKVElement<string> TagName;
        public MKVElement<string> TagString;

        public SimpleTag(string tagName, string tagString) : base(Signatures.SimpleTag)
        {
            TagName = new MKVElement<string>(Signatures.TagName, tagName);
            TagString = new MKVElement<string>(Signatures.TagString, tagString);
        }
    }
}
