﻿using System;
using System.Diagnostics;
using NAudio.Wave;

namespace Sol;

internal sealed class Audio : IDisposable
{
    public static Audio Instance { get; } = new();

    private Audio()
    {
        asio.Init(provider);
        Buffer = new(asio.FramesPerBuffer);
        Buffer.OnBlockNotReady += Processor.Instance.OnBlockNotReady;
    }

    public Buffer Buffer { get; }
    private readonly AsioOut asio = new(Constants.Asio);
    private readonly Provider provider = new();
    private bool stopped = true;
    private object stopLock = new();
    public bool Stopped
    {
        get
        {
            bool stopped;
            lock (stopLock)
            {
                stopped = this.stopped;
            }
            return stopped;
        }
    }

    public void Start()
    {
        Console.WriteLine(asio.FramesPerBuffer);
        stopped = false;
        asio.Play();
    }

    public void Stop()
    {
        lock (stopLock)
        {
            stopped = true;
        }
    }

    public void Dispose()
    {
        asio.Dispose();
    }
}
