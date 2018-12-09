using UnityEngine;
using System.Collections.Generic;

public class Toolbox : MonoBehaviour {

	private Dictionary <string, UnityEngine.Component> tools = new Dictionary<string, UnityEngine.Component>();

	public static Toolbox Instance {
		get { return GetInstance(); }
		set { _instance = value; }
	}

	private static Toolbox _instance;

	private static Toolbox GetInstance () {
		if (Toolbox._instance == null) {
			var go = new GameObject("Toolbox");
			DontDestroyOnLoad(go);
			Toolbox._instance = go.AddComponent<Toolbox>();
		}
		return Toolbox._instance;
	}

	void Awake () {
        this.AddTool<GameInput>("GameInput");
        this.AddTool<GameManager>("GameManager");
    }

    public ObjType GetTool <ObjType> (string objName) where ObjType : Component {
		return tools[objName] as ObjType;
	}

	public ObjType AddTool <ObjType> (string objName) where ObjType : Component {
		var tool = new GameObject (objName);
		tool.transform.SetParent(this.transform);
		ObjType obj = tool.AddComponent<ObjType>();
		this.tools.Add(objName, obj);
		return obj;
	}
}
