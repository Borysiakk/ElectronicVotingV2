using System.Numerics;

namespace ElectronicVoting.Paillier.Interface
{
    public interface IKeyBuilder
    {
        public void Generate(int bitLength, int k);
        public void Generate(BigInteger begin, BigInteger end, int k);
    }
}