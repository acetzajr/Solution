namespace Sol;

class MidiEvent(int channel)
{
    public int Channel { get; } = channel;

    public static MidiEvent? Parse(int message)
    {
        switch ((message >> 4) & 0b1111)
        {
            case 0b1000:
                return new NoteOffEvent(ParseChannel(message), ParseKey(message));
            case 0b1001:
                return new NoteOnEvent(
                    ParseChannel(message),
                    ParseKey(message),
                    ParseVelocity(message)
                );
            case 0b1011:
                if (((message >> 16) & 0xff) == 64)
                {
                    if (((message >> 24) & 0xff) < 64)
                    {
                        return new PedalOffEvent(ParseChannel(message));
                    }
                    return new PedalOnEvent(ParseChannel(message));
                }
                break;
        }
        return null;
    }

    public static int ParseChannel(int message)
    {
        return message & 0b1111;
    }

    public static int ParseKey(int message)
    {
        return (message >> 8) & 0b01111111;
    }

    public static int ParseVelocity(int message)
    {
        return (message >> 16) & 0b01111111;
    }
}

class NoteOnEvent(int channel, int key, int velocity) : MidiEvent(channel)
{
    public int Key { get; } = key;
    public int Velocity { get; } = velocity;
}

class NoteOffEvent(int channel, int key) : MidiEvent(channel)
{
    public int Key { get; } = key;
}

class PedalOnEvent(int channel) : MidiEvent(channel) { }

class PedalOffEvent(int channel) : MidiEvent(channel) { }
