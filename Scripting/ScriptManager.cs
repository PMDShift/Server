// This file is part of Mystery Dungeon eXtended.

// Copyright (C) 2015 Pikablu, MDX Contributors, PMU Staff

// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as published
// by the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.

// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU Affero General Public License for more details.

// You should have received a copy of the GNU Affero General Public License
// along with this program. If not, see <http://www.gnu.org/licenses/>.


namespace Server.Scripting
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Text;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.Text;
    using PMDCP.Core;

    public class ScriptManager
    {
        #region Fields

        static ListPair<string, IAICore> AIInstances;
        static ListPair<string, Type> AITypes;
        static List<Diagnostic> errors;
        static Assembly script;
        static string scriptFolder;
        static Type type;

        #endregion Fields

        #region Properties

        public static ListPair<string, string> NPCAIType {
            get;
            set;
        }

        public static List<Diagnostic> Errors {
            get { return errors; }
        }

        #endregion Properties

        #region Methods

        public static void CallAIProcessSub(string aiType, int tickCount, int mapNum, int mapNpcSlot) {
            if (aiType != null && script != null && NPCAIType.ContainsKey(aiType)) {
                string className = NPCAIType.GetValue(aiType);
                InvokeAISub(className, "ProcessAI", tickCount, mapNum, mapNpcSlot);
            }
        }

        public static void CompileScript(string folder, bool loading) {
            List<string> lstFiles = new List<string>();
            string[] files = System.IO.Directory.GetFiles(folder, "*.cs");
            script = Compile(files);
            CreateInstance("Script.Main");
            if (loading) {
                if (script == null) {
                    //Settings.Scripting = false;
                }
                PopulateAIList();
            }
            scriptFolder = folder;
        }

        public static bool TestCompile(string folder) {
            List<string> lstFiles = new List<string>();
            string[] files = System.IO.Directory.GetFiles(folder, "*.cs");
            return (Compile(files) != null);
        }

        public static void CreateAIInstance(string className, string aiType) {
            if (script != null) {
                if (NPCAIType.ContainsKey(aiType) == false) {
                    Type type = script.GetType(className);
                    IAICore instance = (IAICore)script.CreateInstance(className);
                    AITypes.Add(className, type);
                    AIInstances.Add(className, instance);
                    NPCAIType.Add(aiType, className);
                    //InvokeSub(className, "Init", Globals.mNetScript, Globals.mObjFactory, Globals.mIOTools, Globals.mDebug);
                }
            }
        }

        public static void Initialize() {
            AITypes = new ListPair<string, Type>();
            AIInstances = new ListPair<string, IAICore>();
            NPCAIType = new ListPair<string, string>();
        }

        public static object InvokeFunction(string functionName, params object[] args) {
            try {
                return type.InvokeMember(functionName,
                    System.Reflection.BindingFlags.Public |
                    System.Reflection.BindingFlags.InvokeMethod |
                    System.Reflection.BindingFlags.Static,
                    null, null, args);
            } catch (Exception ex) {
                TriggerError(ex);
                return null;
            }
        }

        public static void InvokeSub(string subName, params object[] args) {
            try {
                type.InvokeMember(subName,
                    System.Reflection.BindingFlags.Public |
                    System.Reflection.BindingFlags.InvokeMethod |
                    System.Reflection.BindingFlags.Static,
                    null, null, args);
            } catch (Exception ex) {
                TriggerError(ex);
            }
        }

        public static void InvokeSubSimple(string subName, object[] args) {
            try {
                type.InvokeMember(subName,
                    System.Reflection.BindingFlags.Public |
                    System.Reflection.BindingFlags.InvokeMethod |
                    System.Reflection.BindingFlags.Static,
                    null, null, args);
            } catch (Exception ex) {
                TriggerError(ex);
            }
        }

        public static void PopulateAIList() {
            AITypes.Clear();
            AIInstances.Clear();
            NPCAIType.Clear();
            Type ti = typeof(IAICore);
            if (script != null) {
                foreach (Type t in script.GetTypes()) {
                    if (ti.IsAssignableFrom(t) && t.IsPublic) {
                        CreateAIInstance(t.FullName, t.Name);
                    }
                }
            }
        }

        public static void Reload() {
            InvokeSub("BeforeScriptReload");
            CompileScript(scriptFolder, false);
            PopulateAIList();
            InvokeSub("AfterScriptReload");
        }

        internal static void CreateInstance(string className) {
            if (script != null) {
                type = script.GetType(className);
            }
        }

        private static Assembly Compile(string[] files) {
            errors = new List<Diagnostic>();

            var currentDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            var assemblies = ((string)AppContext.GetData("TRUSTED_PLATFORM_ASSEMBLIES")).Split(';');

            var references = new List<MetadataReference>();
            foreach (var referenceAssembly in assemblies) {
                references.Add(MetadataReference.CreateFromFile(referenceAssembly));
            }

            var syntaxTrees = new List<SyntaxTree>();
            foreach (var file in files) {
                using (var fileStream = new FileStream(file, FileMode.Open)) {
                    var sourceText = SourceText.From(fileStream);

                    syntaxTrees.Add(SyntaxFactory.ParseSyntaxTree(sourceText));
                }
            }
            string fileName = "CompiledScript.dll";

            var compilation = CSharpCompilation.Create(fileName)
              .WithOptions(new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary))
              .AddReferences(references)
              .AddSyntaxTrees(syntaxTrees);

            string path = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            var compilationResult = compilation.Emit(path);

            foreach (var diagnostic in compilationResult.Diagnostics) {
                errors.Add(diagnostic);
            }

            if (compilationResult.Success) {
                return System.Runtime.Loader.AssemblyLoadContext.Default.LoadFromAssemblyPath(path);
            } else {
                return null;
            }
        }

        private static void InvokeAISub(string className, string subName, params object[] args) {
            try {
                AITypes.GetValue(className).InvokeMember(subName,
                    System.Reflection.BindingFlags.Public |
                    System.Reflection.BindingFlags.InvokeMethod |
                    System.Reflection.BindingFlags.Static,
                    null, AIInstances.GetValue(className), args);
            } catch (Exception) {
                //TriggerError(ex);
            }
        }

        private static void TriggerError(Exception ex) {
            try {
                type.InvokeMember("OnScriptError",
                    System.Reflection.BindingFlags.Public |
                    System.Reflection.BindingFlags.InvokeMethod |
                    System.Reflection.BindingFlags.Static,
                    null, null, new object[] { ex });
            } catch {
            }
        }

        #endregion Methods
    }
}