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

using System;
using System.Collections.Generic;
using System.Text;

using System.Reflection;

namespace Server.Scripting.Editor
{
    public class EditorClassCollection
    {
        List<EditorClass> classes;

        public EditorClassCollection() {
            classes = new List<EditorClass>();
        }

        public List<EditorClass> Classes {
            get { return classes; }
        }

        public EditorClass FindByName(string name) {
            for (int i = 0; i < classes.Count; i++) {
                if (classes[i].Name == name) {
                    return classes[i];
                }
            }
            return null;
        }

        public void LoadClasses() {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            Type[] types;
            for (int i = 0; i < assemblies.Length; i++) {
                if (assemblies[i].FullName.Contains("Server")) {
                    types = assemblies[i].GetTypes();
                    for (int b = 0; b < types.Length; b++) {
                        if (types[b].IsPublic) {
                            LoadClassMethods(types[b]);
                        }
                    }
                }
            }
            classes.Sort(delegate(EditorClass c1, EditorClass c2) { return c1.Name.CompareTo(c2.Name); });
        }

        private void LoadClassMethods(Type type) {
            EditorClass editorClass = new EditorClass();
            editorClass.Name = type.FullName;
            foreach (MethodInfo method in type.GetMethods()) {
                if (method.IsPublic && !method.Name.StartsWith("add_") && !method.Name.StartsWith("remove_")) {
                    string methodName = method.Name.TrimStart("get_".ToCharArray()).TrimStart("set_".ToCharArray());
                    EditorMethod editorMethod;
                    int slot = editorClass.FindMethodByName(methodName);
                    if (slot > -1) {
                        editorMethod = editorClass.Methods[slot];
                    } else {
                        editorMethod = new EditorMethod();
                        editorMethod.Name = methodName;
                    }
                    editorMethod.Static = method.IsStatic;
                    if (slot == -1) {
                        if (method.Name.StartsWith("get_")) {
                            editorMethod.Type = "[prop-get]";
                        } else if (method.Name.StartsWith("set_")) {
                            editorMethod.Type = "[prop-set]";
                        }
                    } else {
                        if (method.Name.StartsWith("get_")) {
                            if (editorMethod.Type == "[prop-get]") {
                                editorMethod.Type = "[prop-get]";
                            } else if (editorMethod.Type == "[prop-set]") {
                                editorMethod.Type = "[prop-get/set]";
                            }
                        } else if (method.Name.StartsWith("set_")) {
                            if (editorMethod.Type == "[prop-set]") {
                                editorMethod.Type = "[prop-set]";
                            } else if (editorMethod.Type == "[prop-get]") {
                                editorMethod.Type = "[prop-get/set]";
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(editorMethod.Type) && editorMethod.Type.StartsWith("[prop")) {
                        if (editorMethod.Params.Count == 0) {
                            ParamSet param = new ParamSet();
                            param.ParamString = "No parameters";
                            editorMethod.Params.Add(param);
                        }
                    } else {
                        LoadMethodParameters(method, editorMethod);
                    }
                    if (slot == -1) {
                        editorClass.Methods.Add(editorMethod);
                    }
                }
            }
            editorClass.Methods.Sort(delegate(EditorMethod m1, EditorMethod m2) { return m1.Name.CompareTo(m2.Name); });
            classes.Add(editorClass);
        }

        private void LoadMethodParameters(MethodInfo method, EditorMethod editorMethod) {
            ParameterInfo[] Myarray = method.GetParameters();
            ParamSet param = new ParamSet();
            param.Parameters = Myarray;
            if (Myarray.Length > 0) {
                param.ParamString = "0: " + Myarray[0].Name + " (" + Myarray[0].ParameterType.ToString() + ")";
                for (int i = 1; i <= Myarray.Length - 1; i++) {
                    param.ParamString = param.ParamString + ", " + Myarray[i].Position.ToString() + ": " + Myarray[i].Name + " (" + Myarray[i].ParameterType.ToString() + ")";
                }
            } else {
                param.ParamString = "No parameters";
            }
            if (method.ReturnType.ToString() != "System.Void") {
                param.ReturnVal = "Returns: " + method.ReturnType.ToString();
            }
            editorMethod.Params.Add(param);
        }
    }
}
