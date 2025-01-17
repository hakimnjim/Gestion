using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using TMPro;

namespace Global.Editor
{
    public class UIScriptGenerator : EditorWindow
    {
        private string moduleName = "NewModule";
        private string structName = "NewModuleStruct";
        private string screenName = "NewScreen";
        private string stateClassName = "NewModuleState";
        private string controllerClassName = "NewModuleController";
        private string spawnerPath = "Assets/Global/StateMachine/StateMachineRunner.cs";
        private string controllerPath = "Assets/Global/UI/ScreenUIController.cs";

        private List<VariableConfig> variables = new List<VariableConfig>();

        [MenuItem("Tools/UI Script Generator")]
        public static void ShowWindow()
        {
            GetWindow<UIScriptGenerator>("UI Script Generator");
        }

        private void OnGUI()
        {
            GUILayout.Label("UI Script Generator", EditorStyles.boldLabel);

            // Module Info
            moduleName = EditorGUILayout.TextField("Module Name", moduleName);
            structName = EditorGUILayout.TextField("Struct Name", structName);
            stateClassName = EditorGUILayout.TextField("State Class Name", stateClassName);
            controllerClassName = EditorGUILayout.TextField("Controller Class Name", controllerClassName);
            screenName = EditorGUILayout.TextField("Screen Name", screenName);

            GUILayout.Space(10);

            // Dynamic Variables
            GUILayout.Label("Configure Variables", EditorStyles.boldLabel);

            for (int i = 0; i < variables.Count; i++)
            {
                GUILayout.BeginHorizontal();

                variables[i].name = EditorGUILayout.TextField("Name", variables[i].name);
                variables[i].type = (VariableType)EditorGUILayout.EnumPopup("Type", variables[i].type);
                variables[i].uiRepresentation = (UIRepresentation)EditorGUILayout.EnumPopup("UI Type", variables[i].uiRepresentation);

                if (GUILayout.Button("-", GUILayout.Width(20)))
                {
                    variables.RemoveAt(i);
                    break;
                }

                GUILayout.EndHorizontal();
            }

            if (GUILayout.Button("+ Add Variable"))
            {
                variables.Add(new VariableConfig { name = "NewVariable", type = VariableType.String, uiRepresentation = UIRepresentation.Text });
            }

            GUILayout.Space(20);

            // Generate Button
            if (GUILayout.Button("Generate Scripts"))
            {
                GenerateScripts();
                UpdateSpawner();
                UpdateScreenUIController();
            }
        }

        private void GenerateScripts()
        {
            string scriptsFolder = Application.dataPath + "/Global";

            if (!System.IO.Directory.Exists(scriptsFolder))
                System.IO.Directory.CreateDirectory(scriptsFolder);

            // Generate State Script
            string stateScript = GenerateStateScript();
            System.IO.File.WriteAllText($"{scriptsFolder}/StateMachine/General/{stateClassName}.cs", stateScript);

            // Generate Controller Script
            string controllerScript = GenerateControllerScript();
            System.IO.File.WriteAllText($"{scriptsFolder}/Screens/{controllerClassName}.cs", controllerScript);

            // Generate Struct Script
            string structScript = GenerateStructScript();
            System.IO.File.WriteAllText($"{scriptsFolder}/Screens/{structName}.cs", structScript);

            GenerateReport(scriptsFolder);

            AssetDatabase.Refresh();
            Debug.Log("Scripts Generated Successfully!");
        }

        private void GenerateReport(string folderPath)
        {
            // Path to the report file
            string reportPath = $"{folderPath}/ScriptGenerationReport.txt";

            // Build report content
            string reportContent = $"Script Generation Report\n";
            reportContent += $"Generated at: {System.DateTime.Now}\n";
            reportContent += $"------------------------------------\n";
            reportContent += $"Module Name: {moduleName}\n";
            reportContent += $"Struct Name: {structName}\n";
            reportContent += $"State Class Name: {stateClassName}\n";
            reportContent += $"Controller Class Name: {controllerClassName}\n";
            reportContent += $"Screen Name: {screenName}\n\n";

            reportContent += "Variables:\n";

            foreach (var variable in variables)
            {
                reportContent += $"- {variable.name} ({GetTypeString(variable.type)}), UI: {variable.uiRepresentation}\n";
            }

            reportContent += $"\nScripts Generated:\n";
            reportContent += $"- {folderPath}/StateMachine/General/{stateClassName}.cs\n";
            reportContent += $"- {folderPath}/Screens/{controllerClassName}.cs\n";
            reportContent += $"- {folderPath}/Screens/{structName}.cs\n";

            // Write the report to a text file
            File.WriteAllText(reportPath, reportContent);
            Debug.Log($"Report generated at {reportPath}");
        }

        private void UpdateSpawner()
        {
            if (!File.Exists(spawnerPath))
            {
                Debug.LogError($"Spawner file not found at {spawnerPath}");
                return;
            }
            string spawnerContent = File.ReadAllText(spawnerPath);
            int classEndIndex = spawnerContent.LastIndexOf("/**/");
            if (classEndIndex > 0)
            {
                spawnerContent = spawnerContent.Insert(classEndIndex - 1, $", {screenName}");
                File.WriteAllText(spawnerPath, spawnerContent);
            }
            else
            {
                Debug.LogError("Null index of");
            }
            /*if (!spawnerContent.Contains(screenName))
            {
                string newScreen = $@"
        screens.Add(new ScreenView 
        {{
            screenType = ScreenType.{screenName},
            screenController = null, 
            isUnique = true
        }});";
                spawnerContent = spawnerContent.Replace("// Add new screens here", newScreen + "\n        // Add new screens here");
                File.WriteAllText(spawnerPath, spawnerContent);
                Debug.Log($"Updated UISpawner with {screenName}");
            }*/
        }

        private void UpdateScreenUIController()
        {
            if (!File.Exists(controllerPath))
            {
                Debug.LogError($"ScreenUIController file not found at {controllerPath}");
                return;
            }

            string controllerContent = File.ReadAllText(controllerPath);
            if (!controllerContent.Contains($"void Init({moduleName}Struct"))
            {
                string newInitMethod = $@"

    public virtual void Init({moduleName}Struct data)
    {{
        Init();
        // Initialize {moduleName} data
    }}
";

                int classEndIndex = controllerContent.LastIndexOf("//**");
                if (classEndIndex > 0)
                {
                    controllerContent = controllerContent.Insert(classEndIndex - 1, newInitMethod);
                    File.WriteAllText(controllerPath, controllerContent);
                    Debug.Log($"ScreenUIController updated with Init method for {moduleName}Struct.");
                }
            }
            else
            {
                Debug.LogWarning($"Init method for {moduleName}Struct already exists in ScreenUIController.");
            }
        }

        private string GenerateStateScript()
        {
            return $@"
using Global.ScreenUIControllers;
using Global.StateMachine;
using System.Collections.Generic;
using UnityEngine;

namespace Global.StateMachine.States
{{
    public class {stateClassName} : State
    {{
        public override void Enter(GlobalStateMachine host)
        {{
            base.Enter(host);

            var controller = (CreateModuleController)GlobalEventManager.OnSpawnScreen(ScreenType.{screenName}, null);
            controller.Init(new {structName}
            {{
                {GenerateStructInit()}
            }});
        }}

        public override void Exit()
        {{
            base.Exit();
            GlobalEventManager.OnDestroyScreenController(ScreenType.{screenName}, null);
        }}
    }}
}}";
        }

        private string GenerateControllerScript()
        {
            string controllerFields = "";
            string initCode = "";

            foreach (var variable in variables)
            {
                // Dynamically declare UI elements and initialize them in the controller
                if (variable.uiRepresentation == UIRepresentation.Text)
                {
                    controllerFields += $"\n        [SerializeField] private TextMeshProUGUI {variable.name}Input;";
                    initCode += $"\n            {variable.name}Input.text = config.{variable.name};";
                }
                else if (variable.uiRepresentation == UIRepresentation.Dropdown)
                {
                    controllerFields += $"\n        [SerializeField] private TMP_Dropdown {variable.name}Dropdown;";
                    //initCode += $"\n            // Set dropdown values for {variable.name}Dropdown based on config.{variable.name};";
                }
                else if (variable.uiRepresentation == UIRepresentation.Toggle)
                {
                    if (GetTypeString(variable.type) == "List<string>")
                    {
                        controllerFields += $"\n        [SerializeField] private CheckBoxController {variable.name}Prefab;";
                        controllerFields += $"\n        [SerializeField] private Transform {variable.name}ParentContent;";
                    }
                    else
                    {
                        controllerFields += $"\n        [SerializeField] private Toggle {variable.name}Toggle;";
                        //initCode += $"\n            {variable.name}Toggle.isOn = config.{variable.name};";
                    }

                }
                else if (variable.uiRepresentation == UIRepresentation.InputField)
                {
                    if (GetTypeString(variable.type) == "List<string>")
                    {
                        controllerFields += $"\n        [SerializeField] private InputFieldController {variable.name}Prefab;";
                        controllerFields += $"\n        [SerializeField] private Transform {variable.name}ParentContent;";
                    }
                    else
                    {
                        controllerFields += $"\n        [SerializeField] private InputField {variable.name}Input;";
                        //initCode += $"\n            {variable.name}Input.text = config.{variable.name};";
                    }

                }
                else if (variable.uiRepresentation == UIRepresentation.Button)
                {
                    if (GetTypeString(variable.type) == "List<string>")
                    {
                        controllerFields += $"\n        [SerializeField] private ActionButtonController {variable.name}Prefab;";
                        controllerFields += $"\n        [SerializeField] private Transform {variable.name}ParentContent;";
                    }
                    else
                    {
                        controllerFields += $"\n        [SerializeField] private Button {variable.name}Input;";
                        //initCode += $"\n            {variable.name}Input.text = config.{variable.name};";
                    }
                }
            }

            return $@"
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Global.ScreenUIControllers
{{
    public class {controllerClassName} : ScreenUIController
    {{
        {controllerFields}

        public override void Init({structName} config)
        {{
            base.Init(config);
            {initCode}
        }}
    }}
}}";
        }

        private string GenerateStructScript()
        {
            string structFields = "";
            foreach (var variable in variables)
            {
                if (variable.uiRepresentation == UIRepresentation.InputField)
                {
                    structFields += $"\n        public Action<string> OnValueChange{variable.name};";
                    if (GetTypeString(variable.type) != "null")
                    {
                        structFields += $"\n        public {GetTypeString(variable.type)} {variable.name};";
                    }
                }
                else if (variable.uiRepresentation == UIRepresentation.Button)
                {
                    structFields += $"\n        public Action OnClick{variable.name};";
                    if (GetTypeString(variable.type) != "null")
                    {
                        structFields += $"\n        public {GetTypeString(variable.type)} {variable.name};";
                    }
                }
                else if (variable.uiRepresentation == UIRepresentation.Toggle)
                {
                    structFields += $"\n        public {GetTypeString(variable.type)} {variable.name};";
                    structFields += $"\n        public Action<string> OnToggleChange{variable.name};";
                }
                else
                {
                    structFields += $"\n        public {GetTypeString(variable.type)} {variable.name};";
                }
            }

            return $@"
using System;
using System.Collections.Generic;

namespace Global.ScreenUIControllers
{{
    public struct {structName}
    {{
        {structFields}
    }}
}}";
        }

        private string GenerateStructInit()
        {
            /*string init = "";
            foreach (var variable in variables)
            {
                init += $"\n                {variable.name} = default,";
            }
            return init.TrimEnd(',');*/
            return "";
        }

        private string GetTypeString(VariableType type)
        {
            switch (type)
            {
                case VariableType.String: return "string";
                case VariableType.Int: return "int";
                case VariableType.Float: return "float";
                case VariableType.Bool: return "bool";
                case VariableType.ListOfString: return "List<string>";
                case VariableType.Null: return "null";
                default: return "string";
            }
        }

        private enum VariableType
        {
            Null,
            String,
            Int,
            Float,
            Bool,
            ListOfString
        }

        private enum UIRepresentation
        {
            Text,
            Dropdown,
            Toggle,
            InputField,
            Button
        }

        private class VariableConfig
        {
            public string name;
            public VariableType type;
            public UIRepresentation uiRepresentation;
        }
    }
}
