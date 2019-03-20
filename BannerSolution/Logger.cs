using Microsoft.SharePoint.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BannerSolution
{/// <summary>
    /// refer to http://msdn.microsoft.com/en-us/library/office/gg512103(v=office.14).aspx
    /// </summary>
    public class APPSLogger : SPDiagnosticsServiceBase
    {
        private static object lockObj = new object();
        private static APPSLogger instance;

        private static string ProductDiagnosticName = "APPS";
        private static string ProductCategoryName = "Logging Service";
        private static string ProductDiagnosticLoggerName = "APPS Logging Service";

        public static APPSLogger Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockObj)
                    {
                        if (instance == null)
                        {
                            instance = new APPSLogger();
                        }
                    }
                }

                return instance;
            }
        }

        public APPSLogger()
            : base(ProductDiagnosticLoggerName, SPFarm.Local)
        { }

        protected override IEnumerable<SPDiagnosticsArea> ProvideAreas()
        {
            return new List<SPDiagnosticsArea>()
            {
                new SPDiagnosticsArea(ProductDiagnosticName, new List<SPDiagnosticsCategory>()
                {
                    new SPDiagnosticsCategory(ProductCategoryName, TraceSeverity.VerboseEx, EventSeverity.Information),
                })
            };
        }

        internal static void Logger(TraceSeverity severity, string format, params object[] args)
        {
            Logger(ProductCategoryName, severity, format, args);
        }

        internal static void Logger(string categoryName, TraceSeverity severity, string format, params object[] args)
        {
            var logger = Instance;
            var category = logger.Areas[ProductDiagnosticName].Categories[categoryName];
            logger.WriteTrace(0, category, severity, format, args);
        }

        /// </summary>
        /// <param name="farm"></param>
        internal static void RegisterLoggingService(SPFarm farm)
        {
            if (farm != null)
            {
                var instance = APPSLogger.Instance;
                if (instance != null)
                {
                    instance.Update();
                    if (instance.Status != SPObjectStatus.Online)
                    {
                        instance.Provision();
                    }
                }
            }
        }

        /// <summary>
        /// same above 
        /// </summary>
        /// <param name="farm"></param>
        internal static void UnRegisterLoggingService(SPFarm farm)
        {
            if (farm != null)
            {
                var instance = APPSLogger.Instance;
                if (instance != null)
                {
                    instance.Delete();
                }
            }
        }
    }
}
