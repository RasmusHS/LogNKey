using System.Security.Cryptography;

namespace PassGenDomain.PasswordDomainService;

public class CryptoRandom : Random
{
    private RandomNumberGenerator _rng;
    private byte[] _uint32Buffer = new byte[4];

    public CryptoRandom()
    {
    }

#pragma warning disable IDE0060 // Remove unused parameter
    public CryptoRandom(int ignoredSeed)
    {
    }
#pragma warning restore IDE0060 // Remove unused parameter

    public override int Next()
    {
        _rng.GetBytes(_uint32Buffer);

        return BitConverter.ToInt32(_uint32Buffer, 0) & 0x7FFFFFFF;
    }

    public override int Next(int maxValue)
    {
        if (maxValue < 0) throw new ArgumentOutOfRangeException(nameof(maxValue));

        return Next(0, maxValue);
    }

    public override int Next(int minValue, int maxValue)
    {
        if (minValue > maxValue) throw new ArgumentOutOfRangeException(nameof(minValue));
        if (minValue == maxValue) return minValue;

        var diff = maxValue - minValue;

        while (true)
        {
            _rng.GetBytes(_uint32Buffer);

            var rand = BitConverter.ToUInt32(_uint32Buffer, 0);

            var max = 1 + (long)uint.MaxValue;
            var remainder = max % diff;

            if (rand < max - remainder)
            {
                return (int)(minValue + (rand % diff));
            }
        }
    }

    public override double NextDouble()
    {
        _rng.GetBytes(_uint32Buffer);

        var rand = BitConverter.ToUInt32(_uint32Buffer, 0);

        return rand / (1.0 + uint.MaxValue);
    }

    public override void NextBytes(byte[] buffer)
    {
        if (buffer is null) throw new ArgumentNullException(nameof(buffer));

        _rng.GetBytes(buffer);
    }
}