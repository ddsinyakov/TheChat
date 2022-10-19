namespace TheChat.Services.Hash
{
    public interface IHashService
    {
        String Hash(String toHash);
        String GenerateSalt();
        bool CheckEquality(String firtsHash, String secondHash);
    }
}
