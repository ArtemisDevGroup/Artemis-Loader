using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Runtime.InteropServices;
using System.ComponentModel.DataAnnotations;

namespace Artemis_Loader.Core
{
    enum MessageType
    {
        Stop,
        BringToForeground,
        Exit
    }

    struct Message
    {
        public MessageType Type;
    }

    public class MessageRecipent
    {
        private bool run;
        private readonly Thread thread;
        private readonly Window window;

        private void InstanceThread(object? parameter)
        {
            if (parameter == null) return;
            NamedPipeServerStream pipe = (NamedPipeServerStream)parameter;

            Message message;

            using (BinaryReader reader = new(pipe))
            {
                byte[] bytes = reader.ReadBytes(Marshal.SizeOf<Message>());

                IntPtr ptr = IntPtr.Zero;
                try
                {
                    ptr = Marshal.AllocHGlobal(Marshal.SizeOf<Message>());
                    Marshal.Copy(bytes, 0, ptr, bytes.Length);
                    message = Marshal.PtrToStructure<Message>(ptr);
                }
                finally
                {
                    Marshal.FreeHGlobal(ptr);
                }
            }

            switch (message.Type)
            {
                case MessageType.Stop:
                    if (run)
                    {
                        run = false;
                        MessageDispatcher.Dispatch(new() { Type = MessageType.Stop });
                    } break;
                case MessageType.BringToForeground:
                    window.Dispatcher.Invoke(() => {
                        if (!window.IsVisible)
                            window.Show();

                        if (window.WindowState == WindowState.Minimized)
                            window.WindowState = WindowState.Normal;

                        window.Topmost = true;
                        window.Topmost = false;
                        window.Focus();
                    });

                    break;
                case MessageType.Exit:
                    break;
                default:
                    break;
            }

            try { pipe.Disconnect(); } catch(InvalidOperationException) { }
            pipe.Close();
        }

        private void MainThread()
        {
            Mutex mutex = new Mutex(false, "ArtemisLoaderMessageHandler");
            if (!mutex.WaitOne(0, false))
            {
                mutex.Dispose();
                return;
            }

            do
            {
                NamedPipeServerStream pipe = new("ArtemisLoaderMessagePipeline", PipeDirection.In, NamedPipeServerStream.MaxAllowedServerInstances, PipeTransmissionMode.Message);
                pipe.WaitForConnection();
                new Thread(new ParameterizedThreadStart(InstanceThread)).Start(pipe);
            } while (run);

            mutex.ReleaseMutex();
            mutex.Dispose();
        }

        public MessageRecipent(Window window)
        {
            run = true;
            this.window = window;
            thread = new Thread(MainThread);
            thread.Start();
        }

        public void Stop()
        {
            MessageDispatcher.Dispatch(new() { Type = MessageType.Stop });
            thread.Join();
        }
    }
    class MessageDispatcher
    {
        public static void Dispatch(Message message)
        {
            int size = Marshal.SizeOf<Message>();
            byte[] bytes = new byte[size];

            IntPtr ptr = IntPtr.Zero;
            try
            {
                ptr = Marshal.AllocHGlobal(size);
                Marshal.StructureToPtr(message, ptr, true);
                Marshal.Copy(ptr, bytes, 0, size);
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }

            using NamedPipeClientStream pipe = new(".", "ArtemisLoaderMessagePipeline", PipeDirection.Out);
            pipe.Connect();

            using BinaryWriter writer = new(pipe);
            writer.Write(bytes);
        }
    }
}
