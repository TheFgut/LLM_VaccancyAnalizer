using System.Text;

namespace VaccancyAnalizer.Utils
{
    [Obsolete]
    public class TokenSequenceStringSearcher
    {
        public int sequenceLen {  get; private set; }
        public string[] stringsToSearch { get; private set; }

        private StringBuilder sb;
        private LinkedList<int> tokensLengths;
        public TokenSequenceStringSearcher(int sequenceLen, string[] stringsToSearch)
        {
            if (sequenceLen <= 0) throw new ArgumentException("sequenceLen cant be less than 1");
            if(stringsToSearch.Length == 0) throw new ArgumentException("stringsToSearch len cant be less than 1");
            this.sequenceLen = sequenceLen;
            this.stringsToSearch = stringsToSearch;
            sb = new StringBuilder();
            tokensLengths = new LinkedList<int>();
        }

        public void AddSequencePart(string token, out bool stringFound)
        {
            stringFound = false;
            tokensLengths.AddLast(token.Length);
            sb = sb.Append(token);
            if (tokensLengths.Count > sequenceLen)
            {
                int toRemoveLen = tokensLengths.First.Value;
                tokensLengths.RemoveFirst();
                sb = sb.Remove(0, toRemoveLen);
            }
            string seqStr = sb.ToString();
            foreach (var str in stringsToSearch)
            {
                stringFound = seqStr.Contains(str);
                if(stringFound) break;
            }
        }
    }
}
