﻿using System;
using System.IO;
using DotVVM.CommandLine.Commands.Core;
using DotVVM.CommandLine.Commands.Logic.SeleniumGenerator;
using DotVVM.CommandLine.Core.Arguments;
using DotVVM.CommandLine.Core.Metadata;

namespace DotVVM.CommandLine.Commands.Handlers
{
    public class GenerateUiTestStubCommandHandler : CommandBase
    {
        public override string Name => "Generate UI Test Stub";

        public override string[] Usages => new[] { "dotvvm gen uitest <NAME>", "dotvvm gut <NAME>" };

        public override bool TryConsumeArgs(Arguments args, DotvvmProjectMetadata dotvvmProjectMetadata)
        {
            if (string.Equals(args[0], "gen", StringComparison.CurrentCultureIgnoreCase)
                && string.Equals(args[1], "uitest", StringComparison.CurrentCultureIgnoreCase))
            {
                args.Consume(2);
                return true;
            }

            if (string.Equals(args[0], "gut", StringComparison.CurrentCultureIgnoreCase))
            {
                args.Consume(1);
                return true;
            }

            return false;
        }

        public override void Handle(Arguments args, DotvvmProjectMetadata dotvvmProjectMetadata)
        {
            var appFullPath = Path.GetDirectoryName(Path.GetFullPath(dotvvmProjectMetadata.ProjectDirectory));
            var websiteAssemblyPath = Path.Combine(appFullPath, "bin", "Debug", "netcoreapp2.0", "SampleApp1.dll");
            dotvvmProjectMetadata.WebAssemblyPath = websiteAssemblyPath;

            SeleniumGeneratorLauncher.Start(args, dotvvmProjectMetadata);
        }
    }
}