using Report._Common.Entities;

namespace Report._Common
{
    public interface IKeyManager
    {
        public IList<KeyPass> KeyList { get; set; }
        public Task<int> ValidateKey(string key);
    }
}
