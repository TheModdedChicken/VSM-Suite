using Il2CppTMPro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace VSMF.UI
{
	public class Text
	{
		public static GameObject Create(string name = "Text (TMP)", string text = "", float size = 20f)
		{
			GameObject go = new GameObject(name);
			go.transform.localScale = new Vector3(1, 1, 1);

			TextMeshProUGUI tmp = go.AddComponent<TextMeshProUGUI>();
			tmp.font = Font.GetDefault();
			tmp.text = text;
			tmp.fontSize = size;
			tmp.fontSizeMax = size;
			tmp.alignment = TextAlignmentOptions.Baseline;

			return go;
		}
	}
}
