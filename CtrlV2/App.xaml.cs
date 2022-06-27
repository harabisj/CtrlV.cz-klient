using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CtrlV2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        internal static readonly string[] DeleteTypes = new string[]
        {
            "hodina", "den", "týden", "měsíc", "rok",
        };

        internal static CtrlvApi API { get; } = new();

        internal static StorageManager Storage { get; } = new();

        protected override void OnStartup(StartupEventArgs e)
        {
        }
    }
}
