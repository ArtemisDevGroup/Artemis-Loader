using Artemis_Loader.Core;
using System;
using System.Windows;

namespace Artemis_Loader
{
    public static class Artemis
    {
        public static void Exit(MessageRecipent? recipent)
        {
            recipent?.Stop();
            Application.Current.Shutdown();
        }
    }
}
