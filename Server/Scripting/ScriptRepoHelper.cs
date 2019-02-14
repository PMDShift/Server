using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using LibGit2Sharp;

namespace Server.Scripting
{
    public static class ScriptRepoHelper
    {
        //[Conditional("RELEASE")]
        public static void PullChanges()
        {
            var targetDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Scripts");
            if (!Directory.Exists(targetDirectory))
            {
                Directory.CreateDirectory(targetDirectory);
            }

            if (!Directory.Exists(Path.Combine(targetDirectory, ".git")))
            {
                Repository.Clone("https://github.com/PMDShift/Scripts.git", targetDirectory);
            }
            else
            {
                using (var repo = new Repository(targetDirectory))
                {
                    // Credential information to fetch
                    var options = new LibGit2Sharp.PullOptions();

                    var signature = new LibGit2Sharp.Signature(new Identity("Server", "server@server.pmdshift"), DateTimeOffset.Now);

                    // Pull
                    Commands.Pull(repo, signature, options);
                }
            }
        }
    }
}
