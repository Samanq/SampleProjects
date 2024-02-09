using System.Buffers.Text;
using System.Runtime.InteropServices;

namespace SpanSample.ConsoleApp.Helpers;

public static class GuidHelper
{
    public static string GuidToFriendlyString(Guid id)
    {
        return Convert.ToBase64String(id.ToByteArray())
            .Replace('/', '-')
            .Replace('+', '_')
            .Replace("=", string.Empty);
    }

    public static string GuidToFriendlyStringWithSpan(Guid id)
    {
        Span<byte> idBytes = stackalloc byte[16];
        Span<byte> base64Bytes = stackalloc byte[24];

        MemoryMarshal.TryWrite(idBytes, ref id);

        Base64.EncodeToUtf8(idBytes, base64Bytes, out _, out _);

        Span<char> charecters = stackalloc char[22];

        for (int i = 0; i < 22; i++)
        {
            charecters[i] = base64Bytes[i] switch
            {
                (byte)'/' => '-',
                (byte)'+' => '_',
                _ => (char)base64Bytes[i]

            };
        }

        return new string(charecters);
    }

    public static Guid FriendlyStringToGuid(string id)
    {
        var res = Convert.FromBase64String(id
            .Replace('-', '/')
            .Replace('_', '+') + "==");

        return new Guid(res);
    }

    public static Guid FriendlyStringToGuidWithSpan(ReadOnlySpan<char> id)
    {
        Span<char> base64Chars = stackalloc char[24];

        for (int i = 0; i < 22; i++)
        {
            switch (id[i])
            {
                case '-':
                    base64Chars[i] = '/';
                    break;
                case '_':
                    base64Chars[i] = '+';
                    break;
                default:
                    base64Chars[i] = id[i];
                    break;
            }
        }

        base64Chars[22] = '=';
        base64Chars[23] = '=';

        Span<byte> idBytes = stackalloc byte[16];
        Convert.TryFromBase64Chars(base64Chars, idBytes, out _);

        return new Guid(idBytes);
    }
}
