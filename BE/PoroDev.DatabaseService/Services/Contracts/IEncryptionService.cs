namespace PoroDev.DatabaseService.Services.Contracts
{
    public interface IEncryptionService
    {
        byte[] EncryptBytes(byte[] data);

        byte[] DecryptBytes(byte[] data);
    }
}
