using Il2CppTMPro;
using Il2CppVampireSurvivors.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace VSMF.UI
{
	public class Button
	{
		public static GameObject CreateNormal (string text = "", ButtonType type = ButtonType.Normal, float width = 150, float height = 100)
		{
			// Create main GameObject
			GameObject obj = new GameObject("NewButton");
			obj.transform.localScale = new Vector3(1, 1, 1);

			// Create TextMeshPro
			GameObject obj_text = Text.Create(text);
			obj_text.transform.localScale = new Vector3(1, 1, 1);
			Object.SetParent(obj, obj_text);
			TextMeshProUGUI tmp = obj_text.GetComponent<TextMeshProUGUI>();
			tmp.text = text;

			// Add navigator
			GameObject button_navigator = Resources.FindObjectsOfTypeAll<GameObject>().Where((obj) => obj.name == "ButtonNavigator").First();
			UnityEngine.Object.Instantiate(button_navigator, obj.transform);

			// Create image
			Image obj_image = obj.AddComponent<Image>();
			obj_image.type = Image.Type.Sliced;
			obj_image.rectTransform.sizeDelta = new Vector2(width, height);
			obj_image.pixelsPerUnitMultiplier = 0.3f;

			// Add sprite
			string sprite_selector = GetSpriteName(type);
			Sprite obj_sprite = Resources.FindObjectsOfTypeAll<Sprite>().Where((obj) => obj.name == sprite_selector).First();
			obj_image.overrideSprite = obj_sprite;

			// Add additional components
			obj.AddComponent<UnityEngine.UI.Button>();
			SelectableUI selectableUI = obj.AddComponent<SelectableUI>();
			selectableUI.selectionType = SelectableUI.SelectableType.BUTTON;
			obj.AddComponent<UIButtonAudio>();

			return obj;
		}

		static string GetSpriteName (ButtonType type)
		{
			if (type == ButtonType.Confirm) return "button_c5_normal";
			else if (type == ButtonType.Danger) return "button_c8_normal";
			else return "button_c9_normal"; // ButtonType.Normal
		}
	}

	public enum ButtonType
	{
		Normal,
		Confirm,
		Danger
	}
}
