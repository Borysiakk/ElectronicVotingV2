﻿using System;
using System.Numerics;

namespace ElectronicVoting.Paillier
{
    public class BigIntegerRandom
    {
        private static Random _random = new Random();

        public static  BigInteger NextBigInteger(int bitLength)
        {
            int value = 0;
            int bits = bitLength % 8;
            bitLength = bitLength - bits;
            int bytes = bitLength / 8;
            
            if (bits != 0)
            {
                for (int i = bits-1; i >= 0; i--)
                {
                    value |= _random.Next(0,2) << i;
                }
            }

            if (bytes != 0)
            {
                byte [] bs = new byte[bytes + 2];
                _random.NextBytes(bs);
                
                bs[bytes] = 0;
                bs[bytes + 1] = 0;
                
                return new BigInteger(bs) + value;
            }
            
            else
            {
                return value;
            }
            
        }

        public static BigInteger NextBigInteger(BigInteger start, BigInteger end)
        {
            BigInteger value = 0;
            BigInteger res = end - start;
            int bitLength = res.ToBinaryString().Length;

            do
            {
                
                value = NextBigInteger(bitLength);
                
            } while ( (value + start) > end);

            return (value + start);

        }
    }
}