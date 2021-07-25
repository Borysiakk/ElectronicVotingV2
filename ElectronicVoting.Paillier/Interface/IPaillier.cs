using System.Numerics;
using ElectronicVoting.Paillier.Model;

namespace ElectronicVoting.Paillier.Interface
{
    public interface IPaillier
    {
        public BigInteger Encryption(BigInteger msg, KeyPublic keyPublic);
        public BigInteger Decryption(BigInteger encrypted , KeyPrivate keyPrivate);
    }
}