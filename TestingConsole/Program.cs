﻿using System;
using System.IO;
using System.Threading;
using CryMediaAPI.Video;

namespace TestingConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2) return;

            string input = args[0];
            string output = args[1];

            // ReadWriteAudio(input, output);
            // ReadWriteVideo(input, output);
            ReadPlayVideo(input, output);
        }

        static void ReadWriteAudio(string input, string output)
        {

        }

        static void ReadWriteVideo(string input, string output)
        {
            var video = new VideoReader(input);
            video.LoadMetadata().Wait();
            video.Load();

            using (var writer = new VideoWriter(output, video.Metadata.Width, video.Metadata.Height, video.Metadata.AvgFramerate))
            {
                writer.OpenWrite(false);

                var frame = new VideoFrame(video.Metadata.Width, video.Metadata.Height);
                while (true)
                {
                    // read next frame
                    var f = video.NextFrame(frame);
                    if (f == null) break;


                    for (int i = 0; i < 100; i++)
                        for (int j = 0; j < 100; j++)
                        {
                            var px = frame.GetPixels(i, j).Span;
                            px[0] = 255;
                            px[1] = 0;
                            px[2] = 0;
                        }

                    writer.WriteFrame(frame);
                }
            }
        }

        static void ReadPlayVideo(string input, string output)
        {
            var video = new VideoReader(input);
            video.LoadMetadata().Wait();
            video.Load();

            using (var player = new VideoPlayer(output))
            {
                player.OpenWrite(video.Metadata.Width, video.Metadata.Height, video.Metadata.AvgFramerateText, true);

                var frame = new VideoFrame(video.Metadata.Width, video.Metadata.Height);
                while (true)
                {
                    // read next frame
                    var f = video.NextFrame(frame);
                    if (f == null) break;


                    for (int i = 0; i < 100; i++)
                        for (int j = 0; j < 100; j++)
                        {
                            var px = frame.GetPixels(i, j).Span;
                            px[0] = 255;
                            px[1] = 0;
                            px[2] = 0;
                        }

                    player.WriteFrame(frame);
                }
            }
        }
    }
}
