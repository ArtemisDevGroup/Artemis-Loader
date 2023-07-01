using System;
using System.Windows;

namespace Artemis_Loader
{
    public static class Artemis
    {
        public static void Exit()
        {
            Application.Current.Shutdown();
        }
    }
}
