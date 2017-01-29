using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.SystemWeb;
using Microsoft.Extensions.DependencyInjection;

namespace MachineKeyAlternative
{
    public class CustomDataProtectionStartup : DataProtectionStartup
    {
        private const string Thumbprint = "B68662EF887F6FB70BB111A44ABD67CC085C3A3A";

        public override void ConfigureServices(IServiceCollection services)
        {
            var workingpath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory),
                "cookie-protection"
            );

            services.AddDataProtection()
                .SetApplicationName("machinekey-alternative")
                .PersistKeysToFileSystem(new DirectoryInfo(workingpath))
                .ProtectKeysWithCertificate(GetCertificate(Thumbprint));
        }

        private X509Certificate2 GetCertificate(string thumbprint)
        {
            using (var store = new X509Store(StoreName.My, StoreLocation.CurrentUser)) {
                store.Open(OpenFlags.OpenExistingOnly | OpenFlags.ReadOnly);
                var certs = store.Certificates.Find(X509FindType.FindByThumbprint, thumbprint, false /* POC HACK - allow invalid certificate */);

                return certs[0];
            }
        }
    }
}