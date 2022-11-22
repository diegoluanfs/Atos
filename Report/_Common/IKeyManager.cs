using report._Common.Entities;

namespace report._Common
{
    public interface IKeyManager
    {
        public IList<KeyPass> KeyList { get; set; }
        public Task<int> ValidateKey(string key);
    }
}
